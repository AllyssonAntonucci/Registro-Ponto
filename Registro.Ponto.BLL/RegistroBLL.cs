using Registro.Ponto.DAL;

namespace Registro.Ponto.BLL
{
    public interface IRegistroBLL
    {
        Task<bool> LoginUsuarioBLL(string username, string password);
        Task<bool> RegistrarPontoBLL(string username, DateTime horaAtual);
    }
    public class RegistroBLL : IRegistroBLL
    {
        private readonly IRegistroDAL _registroDAL;
        private readonly ICadastroBLL _cadastroBLL;
        public RegistroBLL(IRegistroDAL registroDAL, ICadastroBLL cadastroBLL)
        {
            _registroDAL = registroDAL;
            _cadastroBLL = cadastroBLL;
        }

        #region Login do usuário
        public async Task<bool> LoginUsuarioBLL(string username, string password)
        {
            string usuarioSalt = await _registroDAL.RetornarSaltUsuarioDAL(username);

            string hashedPassword = _cadastroBLL.HashAndSaltPassword(password, usuarioSalt);

            return await _registroDAL.LoginUsuarioDAL(username, hashedPassword);   
        }
        #endregion

        #region Registrar ponto
        public async Task<bool> RegistrarPontoBLL(string username, DateTime horaAtual)
        {
            bool registroEntradaExistente = await _registroDAL.VerificarSeExisteRegistroDeEntradaDAL(username);
            
            if(registroEntradaExistente == true) // Se o registro de entrada existir, registramos o ponto de saída
            {
                return await _registroDAL.RegistrarPontoSaidaDAL(username, horaAtual);    
            }
            else // Caso contrário, registramos o ponto de entrada
            {
                return await _registroDAL.RegistrarPontoEntradaDAL(username, horaAtual);
            }
        }
        #endregion
    }
}
