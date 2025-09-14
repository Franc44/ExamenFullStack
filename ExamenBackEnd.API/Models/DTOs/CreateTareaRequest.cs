using System.ComponentModel.DataAnnotations;

namespace ExamenBackEnd.API.Models.DTOs
{
    //Manejo del DTO(objeto parcial) para la creación de una tarea
    public class CreateTareaRequest
    {
        //Validaciones automaticas en DataAnnotations para solo validar el state del objeto
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(100, ErrorMessage = "El título no puede exceder de 100 letras.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder de 500 letras.")]
        public string Descripcion { get; set; }
    }
}