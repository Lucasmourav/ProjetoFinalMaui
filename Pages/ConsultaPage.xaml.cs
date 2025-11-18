using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalMauiReal.ViewModels;

namespace ProjetoFinalMauiReal.Pages;

public partial class ConsultaPage : ContentPage
{
    public ConsultaPage()
    {
        InitializeComponent();
        BindingContext = App.Current!.Services.GetRequiredService<ConsultaViewModel>();
    }

    private async void OnHistoricoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//historico");
    }
}
