using ProjetoFinalMaui.Models;
using ProjetoFinalMaui.Services;

namespace ProjetoFinalMaui.Views;

public partial class Register : ContentPage
{
    private readonly DatabaseService _databaseService;

    public Register()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = new Usuario
            {
                Nome = NomeEntry.Text,
                DataNascimento = DataNascimentoPicker.Date,
                Email = EmailEntry.Text,
                Senha = SenhaEntry.Text
            };

            // Verificar se o email já existe
            var existingUser = await _databaseService.GetUsuarioAsync(usuario.Email);
            if (existingUser != null)
            {
                MessageLabel.Text = "Este email já está cadastrado";
                return;
            }

            await _databaseService.SaveUsuarioAsync(usuario);
            await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");
            await Navigation.PopAsync(); // Volta para a tela de login
        }
        catch (Exception ex)
        {
            MessageLabel.Text = "Erro ao cadastrar: " + ex.Message;
        }
    }
}