using Microsoft.AspNetCore.Mvc;
using Registro.Ponto.BLL;
using Registro.Ponto.WebSite.Models;
using System.Diagnostics;

namespace Registro.Ponto.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICadastroBLL _cadastroBLL;
        private readonly IRegistroBLL _registroBLL;


        public HomeController(ILogger<HomeController> logger, ICadastroBLL cadastroBLL, IRegistroBLL registroBLL)
        {
            _logger = logger;
            _cadastroBLL = cadastroBLL;
            _registroBLL = registroBLL;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(string nome, string sobrenome, string username, string password, string confirmPassword)
        {
            #region Verifica se as senhas coincidem
            if (password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "As senhas não coincidem.");
            }
            #endregion

            #region Converte os três primeiros parâmetros para uppercase
            string nomeUpperCase = nome.ToUpper();
            string sobrenomeUpperCase = sobrenome.ToUpper();
            string usernameUpperCase = username.ToUpper();
            #endregion

            try
            {
                if (ModelState.IsValid)
                {
                    bool sucessoCadastro = await _cadastroBLL.CadastrarUsuarioBLL(nomeUpperCase, sobrenomeUpperCase, usernameUpperCase, password);

                    if (sucessoCadastro == true)
                    {
                        @TempData["mensagemCadastroSucesso"] = "Usuário cadastrado com sucesso";
                        _logger.LogInformation("Sucesso ao cadastrar usuário!");
                        return View();
                    }
                    else
                    {
                        TempData["mensagemCadastroFalha"] = "Falha ao cadastrar, o usuário já existe.";
                        _logger.LogInformation("Falha ao cadastrar, o usuário já existe.");
                        return View();
                    }
                }
                else
                {
                    // Para não limpar os inputs após as senhas não serem iguais
                    TempData["nome"] = nomeUpperCase; 
                    TempData["sobrenome"] = sobrenomeUpperCase;
                    TempData["username"] = usernameUpperCase;
                    return View();
                }
            }
            catch (Exception ex)
            {
                @TempData["mensagemErroCadastro"] = $"Falha ao cadastrar usuário: {ex.InnerException.Message}";
                _logger.LogInformation($"Erro ao cadastrar usuário: {ex.InnerException.Message}");
                return RedirectToAction("Error");
            }
        }

        public IActionResult RegistroPonto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistroPonto(string username, string password)
        {
            string usernameUpperCase = username.ToUpper();

            try
            {
                bool sucessoLogin = await _registroBLL.LoginUsuarioBLL(usernameUpperCase, password);
                DateTime horaAtual = DateTime.Now;

                if (sucessoLogin == true)
                {
                    var registroEntrada = await _registroBLL.RegistrarPontoBLL(usernameUpperCase, horaAtual);
                    if(registroEntrada == true)
                    {
                        @TempData["mensagemRegistro"] = $"Ponto de entrada registrado com sucesso! Você entrou às {horaAtual.ToString("HH:mm:ss")}.";
                    }
                    else
                    {
                        @TempData["mensagemRegistro"] = $"Ponto de saída registrado com sucesso! Você saiu às {horaAtual.ToString("HH:mm:ss")}.";
                    }
                    
                    _logger.LogInformation("Sucesso ao registrar ponto!");
                    return View();
                }
                else
                {
                    @TempData["mensagemFalhaRegistro"] = "Falha ao registrar ponto! Nome de usuário ou senha incorretos.";
                    _logger.LogInformation("Falha ao registrar ponto! Nome de usuário ou senha incorretos.");
                    return View();
                }
            }
            catch(Exception ex)
            {
                @TempData["mensagemErroRegistro"] = $"Erro ao registrar ponto: {ex.InnerException.Message}";
                _logger.LogInformation($"Erro de Registro: {ex.InnerException.Message}");
                return RedirectToAction("Error");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
