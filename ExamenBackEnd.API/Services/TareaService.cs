using Microsoft.EntityFrameworkCore;
using ExamenBackEnd.API.Data;
using ExamenBackEnd.API.Models;
using ExamenBackEnd.API.Models.DTOs;

namespace ExamenBackEnd.API.Services
{
    public class TareaService : ITareaService
    {
        private readonly ExamenBackEndDbContext _context;

        public TareaService(ExamenBackEndDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TareaResponse>> GetTodoTareas()
        {
            var todos = await _context.TareaItems
                .OrderByDescending(t => t.FechaCreacion)
                .ToListAsync();

            return todos.Select(MapToResponse);
        }

        public async Task<TareaResponse?> GetTareaById(int id)
        {
            var todo = await _context.TareaItems.FindAsync(id);
            return todo != null ? MapToResponse(todo) : null;
        }

        public async Task<TareaResponse> CreateTarea(CreateTareaRequest request)
        {
            var todo = new TareaItem
            {
                Titulo = request.Titulo.Trim(),
                Descripcion = request.Descripcion?.Trim(),
                TareaCompletada = false,
                FechaCreacion = DateTime.UtcNow
            };

            _context.TareaItems.Add(todo);
            await _context.SaveChangesAsync();

            return MapToResponse(todo);
        }

        public async Task<TareaResponse?> UpdateTarea(int id, UpdateTareaRequest request)
        {
            var todo = await _context.TareaItems.FindAsync(id);
            if (todo == null)
                return null;

            todo.Titulo = request.Titulo.Trim();
            todo.Descripcion = request.Descripcion?.Trim();
            todo.TareaCompletada = request.TareaCompletada;
            todo.FechaCreacion = DateTime.UtcNow;

            _context.TareaItems.Update(todo);
            await _context.SaveChangesAsync();

            return MapToResponse(todo);
        }

        public async Task<bool> DeleteTarea(int id)
        {
            var todo = await _context.TareaItems.FindAsync(id);
            if (todo == null)
                return false;

            _context.TareaItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
        
        private static TareaResponse MapToResponse(TareaItem todo)
        {
            return new TareaResponse
            {
                Id = todo.Id,
                Titulo = todo.Titulo,
                Descripcion = todo.Descripcion,
                TareaCompletada = todo.TareaCompletada,
                FechaCreacion = todo.FechaCreacion,
                FechaActualizacion = todo.FechaActualizacion
            };
        }
    }
}