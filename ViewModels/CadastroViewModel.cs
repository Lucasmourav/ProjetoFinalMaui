using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjetoFinalMauiReal.Models;
using ProjetoFinalMauiReal.Services;

namespace ProjetoFinalMauiReal.ViewModels
{
    public partial class CadastroViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        [ObservableProperty]
        private string nome = string.Empty;

        [ObservableProperty]
        private DateTime dataNascimento = DateTime.Today.AddYears(-18);

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string senha = string.Empty;

        public CadastroViewModel(DatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task CadastrarAsync()
        {
            var usuario = new Usuario
            {
                Nome = Nome?.Trim() ?? string.Empty,
                DataNascimento = DataNascimento,
                Email = Email?.Trim() ?? string.Empty,
                Senha = Senha ?? string.Empty
            };

            await _db.InitializeAsync();
            var res = await _db.CadastrarUsuarioAsync(usuario);
            if (!res.Ok)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", res.Error, "OK");
                return;
            }

            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Cadastro realizado! Faça login.", "OK");
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
