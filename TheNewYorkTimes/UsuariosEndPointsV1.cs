using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Data;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes
{
    public static class UsuariosEndPointsV1
    {
        public static RouteGroupBuilder MapUsuariosEndPointsV1(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);
            group.MapGet("/{id}", GetById);
            group.MapPost("/", AddUsuario);
            //group.MapPut("/{id}", UpdateTodo);
            //group.MapDelete("/{id}", DeleteTodo);

            return group;
        }

        public static Created<Usuario> AddUsuario(Usuario usuario, UsuarioDbContext context)
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();

            return TypedResults.Created($"/usuarios/{usuario.Id}", usuario);
        }

        public static async Task<Results<Ok<Usuario>, NotFound>> GetById(int id, UsuarioDbContext context)
        {
            var usuario = await context.Usuario.FindAsync(id);

            if (usuario != null)
            {
                return TypedResults.Ok(usuario);
            }

            return TypedResults.NotFound();
        }

        public static async Task<Ok<Usuario[]>> GetAll(UsuarioDbContext context)
        {
            var usuarios = await context.Usuario.ToArrayAsync();
            return TypedResults.Ok(usuarios);
        }

        public static async Task<Results<Ok<Usuario>, NotFound>> GetByEmail(string email, UsuarioDbContext context)
        {
            var usuario = await context.Usuario.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (usuario != null)
            {
                return TypedResults.Ok(usuario);
            }

            return TypedResults.NotFound();
        }

    }
}
