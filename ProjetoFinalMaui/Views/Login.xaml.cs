using ProjetoFinalMaui.Services;

namespace ProjetoFinalMaui.Views;

public partial class Login : ContentPage
{
    private readonly DatabaseService _databaseService;

    public Login()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = await _databaseService.GetUsuarioAsync(EmailEntry.Text);

            if (usuario != null && usuario.Senha == SenhaEntry.Text)
            {
                await SecureStorage.Default.SetAsync("usuario_id", usuario.Id.ToString());
                await SecureStorage.Default.SetAsync("usuario_nome", usuario.Nome);

                // Navegar para a página principal após o login
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                MessageLabel.Text = "Email ou senha incorretos";
            }
        }
        catch (Exception ex)
        {
            MessageLabel.Text = "Erro ao fazer login: " + ex.Message;
        }
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }
}