using System.ComponentModel.DataAnnotations.Schema;

namespace Registro.Ponto.Model
{
    public class RegistroPonto
    {
        public int IdRegistro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; } = null;
        public int UsuarioId { get; set; }

        public virtual User ? User { get; set; } // Cada registro terá apenas um Correspondente na tabela Usuarios, por isso não é necessário o ICollection<>
    }
}
