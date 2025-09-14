using Microsoft.AspNetCore.Mvc;
using ExamenBackEnd.API.Models.DTOs;
using ExamenBackEnd.API.Services;

namespace ExamenBackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _TareaService;
        private readonly ILogger<TareaController> _logger;

        public TareaController(ITareaService TareaService, ILogger<TareaController> logger)
        {
            _TareaService = TareaService;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los registros de la tabla de tarea
        /// </summary>
        /// <returns>Lista de los items de Tarea</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TareaResponse>>>> GetAllTareas()
        {
            try
            {
                var Tareas = await _TareaService.GetTodoTareas();
                return Ok(ApiResponse<IEnumerable<TareaResponse>>.SuccessResult(Tareas, "Tareas obtenidas exitosamente."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al obtener las tareas.");
                return StatusCode(500, ApiResponse<IEnumerable<TareaResponse>>.ErrorResult("Internal server error"));
            }
        }

        /// <summary>
        /// Obtener un item de Tarea por ID
        /// </summary>
        /// <param name="id">Tarea item ID</param>
        /// <returns>Tarea item</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TareaResponse>>> GetTareaById(int id)
        {
            try
            {
                var Tarea = await _TareaService.GetTareaById(id);
                if (Tarea == null)
                {
                    return NotFound(ApiResponse<TareaResponse>.ErrorResult($"Registro de Tarea con ID {id} no encontrado"));
                }

                return Ok(ApiResponse<TareaResponse>.SuccessResult(Tarea, "Tarea obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al obtener la tarea con ID {Id}", id);
                return StatusCode(500, ApiResponse<TareaResponse>.ErrorResult("Internal server error"));
            }
        }

        /// <summary>
        /// Creacion de un nuevo registro de Tarea
        /// </summary>
        /// <param name="request">Objeto de tarea</param>
        /// <returns>Iterm de tarea creaao </returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TareaResponse>>> CreateTarea([FromBody] CreateTareaRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponse<TareaResponse>.ErrorResult("Error en validación de datos", errors));
                }

                var Tarea = await _TareaService.CreateTarea(request);
                return CreatedAtAction(nameof(GetTareaById), new { id = Tarea.Id },
                    ApiResponse<TareaResponse>.SuccessResult(Tarea, "Registro de Tarea creado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando la Tarea");
                return StatusCode(500, ApiResponse<TareaResponse>.ErrorResult("Internal server error"));
            }
        }

        /// <summary>
        /// Actualizacion de un registro de Tarea
        /// </summary>
        /// <param name="id">Tarea ID</param>
        /// <param name="request">Objeto de Tarea</param>
        /// <returns>Objeto de Tarea Actualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TareaResponse>>> UpdateTarea(int id, [FromBody] UpdateTareaRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return BadRequest(ApiResponse<TareaResponse>.ErrorResult("Error en validación de datos", errors));
                }

                var Tarea = await _TareaService.UpdateTarea(id, request);
                if (Tarea == null)
                {
                    return NotFound(ApiResponse<TareaResponse>.ErrorResult($"Registro de Tarea con ID {id} no encontrado."));
                }

                return Ok(ApiResponse<TareaResponse>.SuccessResult(Tarea, "Tarea actualizada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la Tarea con ID {Id}", id);
                return StatusCode(500, ApiResponse<TareaResponse>.ErrorResult("Internal server error"));
            }
        }

        /// <summary>
        /// Eliminar un registro de Tarea
        /// </summary>
        /// <param name="id">Tarea ID</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTarea(int id)
        {
            try
            {
                var deleted = await _TareaService.DeleteTarea(id);
                if (!deleted)
                {
                    return NotFound(ApiResponse<object>.ErrorResult($"Registro de Tarea con ID {id} no encontrado."));
                }

                return Ok(ApiResponse<object>.SuccessResult(null, "Tarea eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando la Tarea con  ID {Id}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResult("Internal server error"));
            }
        }
    }
}