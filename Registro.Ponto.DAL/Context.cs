using Microsoft.EntityFrameworkCore;
using Registro.Ponto.Model;

namespace Registro.Ponto.DAL
{
    public class Context : DbContext
    {
        public Context()
        {
            
        }
        public Context(DbContextOptions<Context> op) : base(op)
        {
            
        }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<RegistroPonto> Registros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para fazer com que o IdRegistro seja uma Primary Key:
            modelBuilder.Entity<RegistroPonto>().HasKey(c => c.IdRegistro);

            // Para fazer com que o UsuarioId(Tabela RegistroPonto) seja uma Foreign Key se relacionando com o Id da tabela Usuarios:
            modelBuilder.Entity<RegistroPonto>()
            .HasOne(r => r.User)
            .WithMany(f => f.Registros)
            .HasForeignKey(r => r.UsuarioId);
        }
    }
}
