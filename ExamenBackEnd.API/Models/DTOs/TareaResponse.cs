namespace ExamenBackEnd.API.Models.DTOs
{
    public class TareaResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; }
        public bool TareaCompletada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}