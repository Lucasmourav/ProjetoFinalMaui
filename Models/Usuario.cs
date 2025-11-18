using System;
using System.Text.RegularExpressions;
 using SQLite;

namespace ProjetoFinalMauiReal.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public (bool IsValid, string Error) Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                return (false, "Nome é obrigatório.");

            if (DataNascimento == default || DataNascimento > DateTime.Today)
                return (false, "Data de nascimento inválida.");

            if (string.IsNullOrWhiteSpace(Email))
                return (false, "Email é obrigatório.");

            // validação básica de email
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(Email))
                return (false, "Email inválido.");

            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 6)
                return (false, "Senha deve ter pelo menos 6 caracteres.");

            return (true, string.Empty);
        }
    }
}
