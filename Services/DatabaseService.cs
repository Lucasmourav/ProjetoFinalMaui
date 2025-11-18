using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjetoFinalMauiReal.Models;
using SQLite;
using Microsoft.Maui.Storage;

namespace ProjetoFinalMauiReal.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _conn;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "projeto_final_maui_real.db3");
            _conn = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            await _conn.CreateTableAsync<Usuario>();
            await _conn.CreateTableAsync<ConsultaClima>();
        }

        public async Task<(bool Ok, string Error)> CadastrarUsuarioAsync(Usuario usuario)
        {
            var v = usuario.Validate();
            if (!v.IsValid)
                return (false, v.Error);

            var existente = await _conn.Table<Usuario>().Where(u => u.Email == usuario.Email).FirstOrDefaultAsync();
            if (existente != null)
                return (false, "Email já cadastrado.");

            await _conn.InsertAsync(usuario);
            return (true, string.Empty);
        }

        public Task<Usuario?> AutenticarAsync(string email, string senha)
        {
            return _conn.Table<Usuario>().Where(u => u.Email == email && u.Senha == senha).FirstOrDefaultAsync();
        }

        public Task<int> RegistrarConsultaAsync(ConsultaClima consulta)
        {
            return _conn.InsertAsync(consulta);
        }

        public Task<List<ConsultaClima>> ObterConsultasAsync(DateTime? inicio, DateTime? fim)
        {
            var query = _conn.Table<ConsultaClima>().OrderByDescending(c => c.DataConsultaUtc);
            return query.ToListAsync().ContinueWith(t =>
            {
                var dados = t.Result.AsEnumerable();
                if (inicio.HasValue)
                    dados = dados.Where(c => c.DataConsultaLocal.Date >= inicio.Value.Date);
                if (fim.HasValue)
                    dados = dados.Where(c => c.DataConsultaLocal.Date <= fim.Value.Date);
                return dados.ToList();
            });
        }
    }
}
