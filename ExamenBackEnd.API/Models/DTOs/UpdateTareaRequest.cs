using System.ComponentModel.DataAnnotations;

namespace ExamenBackEnd.API.Models.DTOs
{
    //Manejo del DTO(objeto parcial) para la actualizacion de una tarea
    public class UpdateTareaRequest
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(100, ErrorMessage = "El título no puede exceder de 100 letras.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder de 500 letras.")]
        public string Descripcion { get; set; }

        public bool TareaCompletada { get; set; }
    }
}