using Microsoft.EntityFrameworkCore;
using Registro.Ponto.Model;

namespace Registro.Ponto.DAL
{
    public interface IRegistroDAL
    {
        Task<bool> LoginUsuarioDAL(string username, string password);
        Task<bool> VerificarSeExisteRegistroDeEntradaDAL(string username);
        Task<bool> RegistrarPontoEntradaDAL(string username, DateTime horaAtual);
        Task<bool> RegistrarPontoSaidaDAL(string username, DateTime horaAtual);
        Task<string> RetornarSaltUsuarioDAL(string username);
    }
    public class RegistroDAL : IRegistroDAL
    {
        private readonly Context _context;
        public RegistroDAL(Context ctx)
        {
            _context = ctx;
        }

        #region Login do usuário
        public async Task<bool> LoginUsuarioDAL(string username, string hashedPassword)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(o => o.Username == username);

            if (usuario != null) // Se o usuário existir, vamos verificar se as senhas conferem
            {
                bool passwordMatch = hashedPassword.Equals(usuario.Password);

                return passwordMatch;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Verificar se o usuário bateu o ponto de entrada
        public async Task<bool> VerificarSeExisteRegistroDeEntradaDAL(string username)
        {
            bool registroEntrada = await _context.Registros.AnyAsync(o => o.Username == username && o.HoraSaida == null); // E se dois usuarios tiverem o mesmo username?

            return registroEntrada;
        }
        #endregion

        #region Registrar ponto de Entrada
        public async Task<bool> RegistrarPontoEntradaDAL(string username, DateTime horaAtual)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(o => o.Username == username);

            if(usuario != null)
            {
                RegistroPonto novoRegistro = new RegistroPonto
                {
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Username = usuario.Username,
                    HoraEntrada = horaAtual,
                    UsuarioId = usuario.Id
                };

                _context.Registros.Add(novoRegistro);
                _context.SaveChanges();
            }

            return true;
        }
        #endregion

        #region Registrar ponto de Saída
        public async Task<bool> RegistrarPontoSaidaDAL(string username, DateTime horaAtual)
        {
            var registro = await _context.Registros.FirstOrDefaultAsync(o => o.Username == username && o.HoraSaida == null);

            if (registro != null)
            {
                registro.HoraSaida = horaAtual;
                _context.SaveChanges();
            }

            return false;
        }
        #endregion

        #region Retornar o Salt do usuário
        public async Task<string> RetornarSaltUsuarioDAL(string username)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(o => o.Username == username);

            if (usuario != null)
            {
                string usuarioSalt = usuario.Salt;

                return usuarioSalt;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
