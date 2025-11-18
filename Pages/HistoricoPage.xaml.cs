using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalMauiReal.ViewModels;

namespace ProjetoFinalMauiReal.Pages;

public partial class HistoricoPage : ContentPage
{
    private readonly HistoricoViewModel _vm;
    public HistoricoPage()
    {
        InitializeComponent();
        BindingContext = _vm = App.Current!.Services.GetRequiredService<HistoricoViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CarregarCommand.Execute(null);
    }
}
