using SQLite;
using ProjetoFinalMaui.Models;
using System.IO;
using Microsoft.Maui.Storage;

namespace ProjetoFinalMaui.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "weatherapp.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
            _database.CreateTableAsync<HistoricoConsulta>().Wait();
        }

        // Métodos para Usuario
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _database.Table<Usuario>().ToListAsync();
        }

        public async Task<Usuario> GetUsuarioAsync(string email)
        {
            return await _database.Table<Usuario>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            if (usuario.Id == 0)
                return await _database.InsertAsync(usuario);
            else
                return await _database.UpdateAsync(usuario);
        }

        // Métodos para HistoricoConsulta
        public async Task<List<HistoricoConsulta>> GetHistoricoAsync(int usuarioId)
        {
            return await _database.Table<HistoricoConsulta>()
                .Where(h => h.UsuarioId == usuarioId)
                .OrderByDescending(h => h.DataConsulta)
                .ToListAsync();
        }

        public async Task<List<HistoricoConsulta>> GetHistoricoByDateAsync(int usuarioId, DateTime inicio, DateTime fim)
        {
            return await _database.Table<HistoricoConsulta>()
                .Where(h => h.UsuarioId == usuarioId && h.DataConsulta >= inicio && h.DataConsulta <= fim)
                .OrderByDescending(h => h.DataConsulta)
                .ToListAsync();
        }

        public async Task<int> SaveHistoricoAsync(HistoricoConsulta historico)
        {
            return await _database.InsertAsync(historico);
        }
    }
}