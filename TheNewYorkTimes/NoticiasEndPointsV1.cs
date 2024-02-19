using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Data;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes
{
    public static class NoticiasEndPointsV1
    {
        public static RouteGroupBuilder MapNoticiasEndPointsV1(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", AddNoticia);
            //group.MapPut("/{id}", UpdateTodo);
            //group.MapDelete("/{id}", DeleteTodo);

            return group;
        }

        public static Created<Noticia> AddNoticia(Noticia noticia, NoticiaDbContext context)
        {
            context.Noticia.Add(noticia);
            context.SaveChanges();

            return TypedResults.Created($"/noticias/{noticia.Id}", noticia);
        }
        public static async Task<Results<Ok<Noticia>, NotFound>> GetById(int id, NoticiaDbContext context)
        {
            var noticia = await context.Noticia.FindAsync(id);

            if (noticia != null)
            {
                return TypedResults.Ok(noticia);
            }

            return TypedResults.NotFound();
        }

        public static async Task<Ok<Noticia[]>> GetAll(NoticiaDbContext context)
        {
            var noticias = await context.Noticia.ToArrayAsync();
            return TypedResults.Ok(noticias);
        }
    }
}
