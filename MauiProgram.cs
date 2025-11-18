using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalMauiReal.Services;
using ProjetoFinalMauiReal.ViewModels;
using ProjetoFinalMauiReal.Pages;

namespace ProjetoFinalMauiReal;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
		builder.Services.AddSingleton<DatabaseService>();
		builder.Services.AddSingleton<WeatherService>();

		// ViewModels
		builder.Services.AddTransient<CadastroViewModel>();
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<ConsultaViewModel>();
		builder.Services.AddTransient<HistoricoViewModel>();

		// Pages
		builder.Services.AddTransient<CadastroPage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<ConsultaPage>();
		builder.Services.AddTransient<HistoricoPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
