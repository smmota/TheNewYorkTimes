using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Data
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Usuario

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Nome)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Senha)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Perfil)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
             .Property(x => x.Ativo)
             .HasColumnType("bit")
             .IsRequired();

            #endregion

        }
    }
}
