using ExamenBackEnd.API.Models;
using ExamenBackEnd.API.Models.DTOs;

namespace ExamenBackEnd.API.Services
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaResponse>> GetTodoTareas();
        Task<TareaResponse?> GetTareaById(int id);
        Task<TareaResponse> CreateTarea(CreateTareaRequest request);
        Task<TareaResponse?> UpdateTarea(int id, UpdateTareaRequest request);
        Task<bool> DeleteTarea(int id);
    }
}