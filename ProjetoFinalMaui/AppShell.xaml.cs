using ProjetoFinalMaui.Views;

namespace ProjetoFinalMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(Login), typeof(Login));
        Routing.RegisterRoute(nameof(Register), typeof(Register));
        
        LoadUserName();
    }

    private async void LoadUserName()
    {
        var userName = await SecureStorage.Default.GetAsync("usuario_nome");
        if (!string.IsNullOrEmpty(userName))
        {
            UserNameLabel.Text = $"Bem-vindo, {userName}!";
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await SecureStorage.Default.SetAsync("usuario_id", string.Empty);
        await SecureStorage.Default.SetAsync("usuario_nome", string.Empty);
        
        Application.Current.MainPage = new NavigationPage(new Login());
    }
}
