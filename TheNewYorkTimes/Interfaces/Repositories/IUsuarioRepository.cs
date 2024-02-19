﻿using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> GetUsuarioByUserAndPassword(string userName, string password);

        Task<Usuario> GetUsuarioByLogin(string userName);
        Task<bool> VerificaSeUsuarioExiste(string userName);
    }
}
