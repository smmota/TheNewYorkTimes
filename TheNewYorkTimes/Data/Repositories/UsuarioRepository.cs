﻿using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Interfaces.Repositories;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SqlContext _sqlContext;

        public UsuarioRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<Usuario> GetUsuarioByLogin(string userName)
        {
            var users = _sqlContext.Usuario;
            return await users.Where(x => x.Email.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByUserAndPassword(string userName, string password)
        {
            return await _sqlContext.Usuario.Where(x => x.Email.ToLower() == userName.ToLower() && x.Senha == password).FirstOrDefaultAsync();
        }

        public async Task<bool> VerificaSeUsuarioExiste(string userName)
        {
            var users = _sqlContext.Usuario;
            var user = await users.Where(x => x.Email.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

            return user != null ? true : false;
        }
    }
}
