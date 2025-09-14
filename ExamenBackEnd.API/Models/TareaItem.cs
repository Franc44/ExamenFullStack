using System.ComponentModel.DataAnnotations;

namespace ExamenBackEnd.API.Models
{
    //Creamos el modelo(Ttabla principal) con validaciones del CRUD
    public class TareaItem
    {
        public int Id { get; set; }

        //Validaciones automaticas en DataAnnotations para solo validar el state del objeto
        [Required(ErrorMessage = "El título de la tarea es requerido.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder de 100 letras.")]
        public string Titulo { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder de 500 letras.")]
        public string Descripcion { get; set; }

        public bool TareaCompletada { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; }

    }
}