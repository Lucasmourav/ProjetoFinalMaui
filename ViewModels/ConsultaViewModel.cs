using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjetoFinalMauiReal.Models;
using ProjetoFinalMauiReal.Services;

namespace ProjetoFinalMauiReal.ViewModels
{
    public partial class ConsultaViewModel : ObservableObject
    {
        private readonly WeatherService _weather;
        private readonly DatabaseService _db;

        [ObservableProperty]
        private string cidade = string.Empty;

        [ObservableProperty]
        private string resultado = string.Empty;

        public ConsultaViewModel(WeatherService weather, DatabaseService db)
        {
            _weather = weather;
            _db = db;
        }

        [RelayCommand]
        private async Task ConsultarAsync()
        {
            if (string.IsNullOrWhiteSpace(Cidade))
            {
                await Application.Current!.MainPage!.DisplayAlert("Atenção", "Informe a cidade.", "OK");
                return;
            }

            var res = await _weather.ObterTempoAgoraAsync(Cidade.Trim());
            if (res == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível obter a previsão.", "OK");
                return;
            }

            Resultado = res.Resumo;
            await _db.InitializeAsync();
            await _db.RegistrarConsultaAsync(new ConsultaClima
            {
                Cidade = Cidade.Trim(),
                ResultadoResumo = res.Resumo,
                DataConsultaUtc = DateTime.UtcNow
            });
        }
    }
}
