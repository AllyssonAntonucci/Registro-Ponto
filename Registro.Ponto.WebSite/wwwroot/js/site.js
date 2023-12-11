/*--------------------Tempo de exibição da mensagemCadastro-----------------------------*/ 
setTimeout(function () {
    document.getElementById('mensagem').style.display = 'none';
}, 5000); // Tempo em milissegundos = 5 segundos)


/*--------------------Aviso de registro de ponto-----------------------------*/ 
const avisoRegistroContainer = document.querySelector('.registroPonto-container.active'); // Seleciona a div com a classe
const fecharBtn = document.getElementById('btnFechar'); // Seleciona pelo Id

const fecharAvisoSucessoRegistro = () => { avisoRegistroContainer.classList.remove('active') }
