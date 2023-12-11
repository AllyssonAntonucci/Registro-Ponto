using Registro.Ponto.DAL;
using System.Security.Cryptography;
using System.Text;

namespace Registro.Ponto.BLL
{
    public interface ICadastroBLL
    {
        Task<bool> CadastrarUsuarioBLL(string nome, string sobrenome, string username, string password);
        string HashAndSaltPassword(string password, string salt);  
    }
    public class CadastroBLL : ICadastroBLL
    {
        private readonly ICadastroDAL _cadastroDAL;
        
        public CadastroBLL(ICadastroDAL cadastro)
        {
            _cadastroDAL = cadastro;
        }

        #region Cadastrar Usuario
        public async Task<bool> CadastrarUsuarioBLL(string nome, string sobrenome, string username, string password)
        {
            string salt = GerarSalt();
            string hashedPW = HashAndSaltPassword(password, salt);

            bool usuarioCadastrado = await _cadastroDAL.UsuarioJaCadastradoDAL(username); 

            if(usuarioCadastrado == false) // Se o username não existir no banco de dados, cadastre o usuário
            {
                return await _cadastroDAL.CadastrarUsuarioDAL(nome, sobrenome, username, hashedPW, salt);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Criar Hash and Salt Password
        public string HashAndSaltPassword(string password, string salt)
        {
            SHA256 hash = SHA256.Create();

            var passwordWithSaltBytes = Encoding.UTF8.GetBytes(salt + password);
            var hashedSaltPassword = hash.ComputeHash(passwordWithSaltBytes);

            return Convert.ToHexString(hashedSaltPassword);
        }
        #endregion

        #region Criar Salt
        private static string GerarSalt()
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        #endregion
    }
}
