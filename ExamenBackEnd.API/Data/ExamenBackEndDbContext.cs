using Microsoft.EntityFrameworkCore;
using ExamenBackEnd.API.Models;

namespace ExamenBackEnd.API.Data
{
    public class ExamenBackEndDbContext : DbContext
    {
        public ExamenBackEndDbContext(DbContextOptions<ExamenBackEndDbContext> options) : base(options)
        {
        }

        public DbSet<TareaItem> TareaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TodoItem entity
            modelBuilder.Entity<TareaItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.TareaCompletada).HasDefaultValue(false);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
