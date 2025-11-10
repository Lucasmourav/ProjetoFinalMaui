using SQLite;

namespace ProjetoFinalMaui.Models
{
    public class HistoricoConsulta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Cidade { get; set; }

        public DateTime DataConsulta { get; set; }

        public double Temperatura { get; set; }

        public string Condicao { get; set; }

        public int UsuarioId { get; set; }
    }
}