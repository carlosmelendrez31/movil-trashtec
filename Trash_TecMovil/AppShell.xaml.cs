using Trash_TecMovil.View;
namespace Trash_TecMovil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CargarNombreUsuario();
        }
        private async void CargarNombreUsuario()
        {
            var nombre = await SecureStorage.GetAsync("nombreusuario") ?? "Usuario";
            NombreUsuarioLabel.Text = $"Hola, {nombre} 👋";
        }


        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("AuthToken");
            SecureStorage.Remove("nombreusuario");

            // Volver a Login
            Application.Current.MainPage = new NavigationPage(new View.Login());
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {

            SecureStorage.Remove("AuthToken");
            SecureStorage.Remove("nombreusuario");

            // Volver a Login
            Application.Current.MainPage = new NavigationPage(new View.Login());
        }
    }
}
