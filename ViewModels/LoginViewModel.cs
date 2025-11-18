using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjetoFinalMauiReal.Services;

namespace ProjetoFinalMauiReal.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string senha = string.Empty;

        public LoginViewModel(DatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task EntrarAsync()
        {
            await _db.InitializeAsync();
            var user = await _db.AutenticarAsync(Email?.Trim() ?? string.Empty, Senha ?? string.Empty);
            if (user == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Atenção", "Credenciais inválidas.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("//consulta");
        }

        [RelayCommand]
        private Task IrParaCadastroAsync()
        {
            return Shell.Current.GoToAsync("cadastro");
        }
    }
}
