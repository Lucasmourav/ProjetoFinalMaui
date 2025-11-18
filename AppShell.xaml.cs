namespace ProjetoFinalMauiReal;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("cadastro", typeof(Pages.CadastroPage));
    }
}
