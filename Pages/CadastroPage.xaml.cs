using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalMauiReal.ViewModels;

namespace ProjetoFinalMauiReal.Pages;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
        BindingContext = App.Current!.Services.GetRequiredService<CadastroViewModel>();
    }
}
