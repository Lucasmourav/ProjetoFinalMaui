using System.Net.Http.Json;
using ProjetoFinalMaui.Models;
using ProjetoFinalMaui.Services;

namespace ProjetoFinalMaui.Views;

public partial class WeatherPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "SUA_CHAVE_API_AQUI"; // Você precisará de uma chave API do OpenWeatherMap

    public WeatherPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    }

    private async void OnConsultarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_apiKey) || _apiKey.Contains("SUA_CHAVE_API_AQUI", StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Configuração necessária", "Defina sua chave da API OpenWeatherMap em WeatherPage.xaml.cs (variável _apiKey) antes de consultar.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(CidadeEntry.Text))
        {
            MessageLabel.Text = "Por favor, digite o nome da cidade";
            return;
        }

        try
        {
            var cidade = CidadeEntry.Text;
            var response = await _httpClient.GetAsync($"weather?q={cidade}&appid={_apiKey}&units=metric&lang=pt_br");
            
            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                
                if (weatherData != null)
                {
                    ResultadoFrame.IsVisible = true;
                    CidadeLabel.Text = weatherData.Name;
                    TemperaturaLabel.Text = $"{Math.Round(weatherData.Main.Temp)}°C";
                    CondicaoLabel.Text = weatherData.Weather[0].Description;

                    // Salvar no histórico
                    var usuarioId = await SecureStorage.Default.GetAsync("usuario_id");
                    if (int.TryParse(usuarioId, out int id))
                    {
                        var historico = new HistoricoConsulta
                        {
                            Cidade = cidade,
                            DataConsulta = DateTime.Now,
                            Temperatura = weatherData.Main.Temp,
                            Condicao = weatherData.Weather[0].Description,
                            UsuarioId = id
                        };

                        await _databaseService.SaveHistoricoAsync(historico);
                    }

                    MessageLabel.Text = string.Empty;
                }
            }
            else
            {
                MessageLabel.Text = "Cidade não encontrada";
                ResultadoFrame.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            MessageLabel.Text = "Erro ao consultar previsão do tempo: " + ex.Message;
            ResultadoFrame.IsVisible = false;
        }
    }
}

public class WeatherResponse
{
    public string Name { get; set; }
    public MainInfo Main { get; set; }
    public Weather[] Weather { get; set; }
}

public class MainInfo
{
    public double Temp { get; set; }
}

public class Weather
{
    public string Description { get; set; }
}