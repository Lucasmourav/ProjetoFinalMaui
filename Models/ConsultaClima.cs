using System;
 using SQLite;

namespace ProjetoFinalMauiReal.Models
{
    public class ConsultaClima
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public string ResultadoResumo { get; set; } = string.Empty;
        public DateTime DataConsultaUtc { get; set; }

        public DateTime DataConsultaLocal => DataConsultaUtc.ToLocalTime();
    }
}
