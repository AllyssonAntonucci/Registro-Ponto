namespace Registro.Ponto.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<RegistroPonto> ? Registros { get; set; } // Cada usuario pode ter mais de um registro de ponto, por isso o uso do ICollection<>

    }
}
