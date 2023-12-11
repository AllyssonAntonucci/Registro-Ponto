using Microsoft.EntityFrameworkCore;
using Registro.Ponto.Model;

namespace Registro.Ponto.DAL
{
    public interface ICadastroDAL
    {
        Task<bool> UsuarioJaCadastradoDAL(string username);
        Task<bool> CadastrarUsuarioDAL(string nome, string sobrenome, string username, string hashedPW, string salt);
    }

    public class CadastroDAL : ICadastroDAL
    {
        private readonly Context _context;
        public CadastroDAL(Context ctx)
        {
            _context = ctx;
        }

        #region Verificar se o usuário já está cadastrado
        public async Task<bool> UsuarioJaCadastradoDAL(string username)
        {
            bool usuarioCadastrado = await _context.Usuarios.AnyAsync(o => o.Username == username);

            return usuarioCadastrado;
        }
        #endregion

        #region Cadastrar usuário
        public async Task<bool> CadastrarUsuarioDAL(string nome, string sobrenome, string username, string hashedPW, string salt)
        {
            bool usuarioCadastrado = await _context.Usuarios.AnyAsync(o => o.Username == username);
            
            if(usuarioCadastrado == false)
            {
                User novoUsuario = new User
                {
                    Nome = nome,
                    Sobrenome = sobrenome,
                    Username = username,
                    Password = hashedPW,
                    Salt = salt
                };

                _context.Usuarios.Add(novoUsuario);
                _context.SaveChanges();
            }

            return true;
        }
        #endregion
    }
}
