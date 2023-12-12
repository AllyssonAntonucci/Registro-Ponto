# Registro de Ponto

Este projeto foi construido utilizando uma arquitetura em três camadas:

* UI (User Interface ou Interface do Usuário): Parte do sistema que interage diretamente com o usuário;

* BLL (Business Logic Layer ou Camada de Lógica de Negócios): É a camada intermediária entre as camadas UI e DAL. Basicamente é a parte do sistema que contém a lógica de negócios, sendo responsável por orquestrar a lógica do projeto e encaminhar as solicitações para a camada de acesso a dados (DAL);

* DAL (Data Access Layer ou Camada de acesso a dados) : É a camada responsável por interagir com o armazenamento de dados, seja ele um banco de dados, serviço web ou qualquer outro meio de persistência de dados.

O seguinte diagrama representa a interação entre as camadas UI, BLL e DAL:

<div align="center">
  <img src="https://github.com/AllyssonAntonucci/Registro-Ponto/assets/125825975/c18b6be3-7feb-4c41-bf0d-f9365d5930d8" alt="Diagrama de camadas">
</div>

Além disso, este projeto utiliza o conceito de "hash and salt" para garantir a segurança das senhas armazenadas, onde na senha fornecida pelo usuário durante o cadastro é adicionada uma string aleatória única ("salt") e após isso, essa combinação de senha com "salt" passa pelo processo de hashing, que converte dados de tamanho variável em um valor de tamanho fixo, geralmente uma sequência alfanumérica. Somente após essa conversão, o "hash" resultante é armazenado no banco de dados. É interessante ressaltar a importância dessa combinação de "hash" e "salt", uma vez que o uso do "salt" garante que mesmo que dois usuários tenham a mesma senha, seus hashes de senha serão diferentes devido ao "salt" único. Além de ajudar a prevenir ataques de dicionário e ataques de tabela arco-íris, tornando as senhas mais seguras.

---

## Como funciona o projeto

Basicamente o projeto é dividido em duas páginas, a página de registro de ponto e a página de cadastro. Para registrar o ponto é necessário ter feito o cadastro.

### Cadastro:

O usuário precisa se cadastrar na página de cadastro para então poder registrar o seu ponto. Para o cadastro é necessário o nome, sobrenome, nome de usuário e senha:

<div align="center">
  <img src="https://github.com/AllyssonAntonucci/Registro-Ponto/assets/125825975/9ce723e9-b4e8-4253-87c3-676d83a063a1" alt="Tela de cadastro">
</div>

### Registrando o ponto:

Após se cadastrar, o usuário pode registrar o seu ponto na seguinte página, para o registro é necessário o nome de usuário e senha:

<div align="center">
  <img src="https://github.com/AllyssonAntonucci/Registro-Ponto/assets/125825975/a0302721-b9bb-4aef-b40b-e7c07044c7df" alt="Tela de registro de ponto">
</div>

Ao registrar o ponto de entrada, a seguinte mensagem é exibida:

<div align="center">
  <img src="https://github.com/AllyssonAntonucci/Registro-Ponto/assets/125825975/3ebf6b4e-35a0-438b-b492-359583f07a50" alt="Mensagem de registro de entrada">
</div>

Já ao registrar o ponto de saída, a mensagem exibida é:

<div align="center">
  <img src="https://github.com/AllyssonAntonucci/Registro-Ponto/assets/125825975/c3f366dd-8d53-48ba-9db3-750539a348f8" alt="Mensagem de registro de saída">
</div>

---

## Como rodar o projeto:

### Dependências necessárias:

* [*Visual Studio*](https://visualstudio.microsoft.com/pt-br/)
* [*Docker*](https://www.docker.com/products/docker-desktop/)

### Ferramenta opcional
* Uma ferramenta de gerenciamento de banco de dados, como o [*SQL Management Studio*](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) ou similares para consultar e interagir com os dados no banco de dados.

### Configuração do Ambiente:

1. Instale o Visual Studio e o Docker.

2. Execute o seguinte comando no terminal (Prompt de comando ou PowerShell) para criar um contêiner do SQL Server via Docker:

     ```shell
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
     ```

    Certifique-se de escolher uma senha forte e substituí-la no campo "yourStrong(!)Password".

3. Clone este repositório usando o seguinte comando no terminal (Prompt de comando ou PowerShell):

    ```shell
    git clone https://github.com/AllyssonAntonucci/Registro-Ponto.git
    ```

4. Antes de compilar o projeto no Visual Studio, abra o arquivo Program.cs localizado em Registro.Ponto.WebSite e substitua a linha de código:

    ```csharp
    builder.Configuration.GetConnectionString("ConexaoBancoSQL")
    ```

    pela sua string de conexão do banco SQL.

5. Pronto, o projeto está pronto para uso!

---

Para mais informações ou dúvidas, sinta-se à vontade para entrar em contato comigo:
* Email: allysson.antonucci.dev@gmail.com
