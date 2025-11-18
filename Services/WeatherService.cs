using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetoFinalMauiReal.Services
{
    public class WeatherResult
    {
        public string Resumo { get; set; } = string.Empty;
        public double? TemperaturaC { get; set; }
    }

    public class WeatherService
    {
        private readonly HttpClient _http;

        public WeatherService(HttpClient? httpClient = null)
        {
            _http = httpClient ?? new HttpClient();
        }

        public async Task<(double lat, double lon)?> GeocodificarAsync(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade)) return null;
            var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(cidade)}&count=1&language=pt&format=json";
            using var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            using var stream = await resp.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            var results = doc.RootElement.GetProperty("results");
            if (results.GetArrayLength() == 0) return null;
            var first = results[0];
            var lat = first.GetProperty("latitude").GetDouble();
            var lon = first.GetProperty("longitude").GetDouble();
            return (lat, lon);
        }

        public async Task<WeatherResult?> ObterTempoAgoraAsync(string cidade)
        {
            var coords = await GeocodificarAsync(cidade);
            if (coords == null) return null;
            var (lat, lon) = coords.Value;
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
            using var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            using var stream = await resp.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            if (!doc.RootElement.TryGetProperty("current_weather", out var cw)) return null;
            var temp = cw.GetProperty("temperature").GetDouble();
            var wind = cw.GetProperty("windspeed").GetDouble();
            var resumo = $"Temp: {temp:0.#}°C | Vento: {wind:0.#} km/h";
            return new WeatherResult { Resumo = resumo, TemperaturaC = temp };
        }
    }
}
