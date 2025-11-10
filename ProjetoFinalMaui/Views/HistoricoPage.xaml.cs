using ProjetoFinalMaui.Services;

namespace ProjetoFinalMaui.Views;

public partial class HistoricoPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public HistoricoPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();

        // Inicializa os datepickers
        DataInicioPicker.Date = DateTime.Today.AddDays(-7);
        DataFimPicker.Date = DateTime.Today;

        // Carrega o histórico inicial
        CarregarHistorico();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarHistorico();
    }

    private async void CarregarHistorico()
    {
        try
        {
            var usuarioId = await SecureStorage.Default.GetAsync("usuario_id");
            if (int.TryParse(usuarioId, out int id))
            {
                var historico = await _databaseService.GetHistoricoByDateAsync(
                    id,
                    DataInicioPicker.Date,
                    DataFimPicker.Date.AddDays(1).AddSeconds(-1));

                HistoricoCollectionView.ItemsSource = historico;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Erro ao carregar histórico: " + ex.Message, "OK");
        }
    }

    private void OnFiltrarClicked(object sender, EventArgs e)
    {
        CarregarHistorico();
    }
}