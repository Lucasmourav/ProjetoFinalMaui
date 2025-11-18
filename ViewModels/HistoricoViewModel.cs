using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjetoFinalMauiReal.Models;
using ProjetoFinalMauiReal.Services;

namespace ProjetoFinalMauiReal.ViewModels
{
    public partial class HistoricoViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        [ObservableProperty]
        private DateTime inicio = DateTime.Today.AddDays(-7);

        [ObservableProperty]
        private DateTime fim = DateTime.Today;

        public ObservableCollection<ConsultaClima> Itens { get; } = new();

        public HistoricoViewModel(DatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task CarregarAsync()
        {
            await _db.InitializeAsync();
            var dados = await _db.ObterConsultasAsync(Inicio, Fim);
            Itens.Clear();
            foreach (var d in dados)
                Itens.Add(d);
        }
    }
}
