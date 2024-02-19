using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Noticia> Noticia { get; set; }

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

            #region Noticia

            modelBuilder.Entity<Noticia>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Noticia>()
                .Property(x => x.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Noticia>()
               .Property(x => x.Descricao)
               .HasMaxLength(500)
               .IsRequired();

            modelBuilder.Entity<Noticia>()
              .Property(x => x.Chapeu)
              .HasMaxLength(20)
              .IsRequired();

            modelBuilder.Entity<Noticia>()
             .Property(x => x.DataPublicacao)
             .HasColumnType("date")
             .IsRequired();

            modelBuilder.Entity<Noticia>()
             .Property(x => x.Autor)
             .HasMaxLength(50)
             .IsRequired();

            #endregion

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
