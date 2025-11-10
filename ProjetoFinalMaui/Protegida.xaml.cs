using System.Threading.Tasks; // Importa funcionalidades para trabalhar com tarefas ass�ncronas

namespace MauiAppLogin; // Define o namespace do projeto

public partial class Protegida : ContentPage // Declara a classe Protegida que herda de ContentPage (p�gina visual do MAUI)
{
	public Protegida() // Construtor da p�gina Protegida
	{
		InitializeComponent(); // Inicializa os componentes visuais definidos no XAML

		string? usuario_logado = null; // Declara vari�vel para armazenar o nome do usu�rio logado

		Task.Run(async () => // Executa uma tarefa ass�ncrona em segundo plano
		{
			usuario_logado = await SecureStorage.Default.GetAsync("usuario_logado"); // Recupera o nome do usu�rio logado do armazenamento seguro
			lbl_boasvindas.Text = $"bem vindo (a) {usuario_logado}"; // Atualiza o texto do label de boas-vindas com o nome do usu�rio
		});
	}

    private async void Button_Clicked(object sender, EventArgs e) // Evento disparado ao clicar no bot�o de sair
    {
		bool confirmacao = await DisplayAlert( // Exibe um alerta de confirma��o para o usu�rio
			"tem certeza?", "sair do app", "sim", "nao");

		if (confirmacao) { // Se o usu�rio confirmar a sa�da
			SecureStorage.Default.Remove("usuario_logado"); // Remove o usu�rio logado do armazenamento seguro
			App.Current.MainPage = new Login(); // Redireciona para a p�gina de login
		}
    }
}