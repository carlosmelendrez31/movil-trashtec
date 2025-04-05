using Trash_TecMovil.Models;

namespace Trash_TecMovil.View;

public partial class Login : ContentPage
{

    private Usuario usuariologin;

    public Login()
    {
        InitializeComponent();
        // Asigna el ViewModel al BindingContext
        
        BindingContext = new LoginViewModel(new HttpClient());
        usuariologin = new Usuario(); // Asegúrate de que el objeto Usuario esté inicializado
    }
    
    

    private async void Button_Clicked_1(object sender, EventArgs e)
    {

        Console.WriteLine("➡ Botón de inicio de sesión presionado.");

        var viewModel = (LoginViewModel)BindingContext;

        // Validación de los campos antes de enviar
        if (string.IsNullOrEmpty(viewModel.Usuario.email) || string.IsNullOrEmpty(viewModel.Usuario.contrasena))
        {
            Console.WriteLine("❌ Error: Los campos de email o contraseña están vacíos.");
            await DisplayAlert("Error", "Por favor, ingresa tu correo y contraseña.", "OK");
            return;
        }

        Console.WriteLine($"➡ Intentando iniciar sesión con: {viewModel.Usuario.email}");

        bool loginExitoso = await viewModel.IniciarSesion(viewModel.Usuario.email, viewModel.Usuario.contrasena);

        if (loginExitoso)
        {
            Application.Current.MainPage = new AppShell();

            // Espera un poquitito pa’ que se monte el Shell
            await Task.Delay(100);

            // Y ahora sí navegas
            await Shell.Current.GoToAsync("//principal");
        }
        else
        {
            await DisplayAlert("Error", "Correo o contraseña incorrectos.", "OK");
        }
    }

}

