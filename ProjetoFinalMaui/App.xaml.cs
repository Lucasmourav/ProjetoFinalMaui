using ProjetoFinalMaui.Views;

namespace ProjetoFinalMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                var usuarioId = await SecureStorage.Default.GetAsync("usuario_id");
                if (string.IsNullOrEmpty(usuarioId))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = new NavigationPage(new Login());
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = new AppShell();
                    });
                }
            });
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 400;
            window.Height = 600;

            return window;
        }
    }
}
