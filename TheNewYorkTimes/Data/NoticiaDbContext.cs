using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Data
{
    public class NoticiaDbContext : DbContext
    {
        public NoticiaDbContext(DbContextOptions<NoticiaDbContext> options) : base(options) { }

        public DbSet<Noticia> Noticia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
    }
}
