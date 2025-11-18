using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalMauiReal.ViewModels;

namespace ProjetoFinalMauiReal.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = App.Current!.Services.GetRequiredService<LoginViewModel>();
    }
}
