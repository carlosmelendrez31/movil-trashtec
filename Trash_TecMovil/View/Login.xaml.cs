using Trash_TecMovil.Models;

namespace Trash_TecMovil.View;

public partial class Login : ContentPage
{
    new Usuario usuariologin;
	public Login()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool loginExitoso = await((LoginViewModel)BindingContext).IniciarSesion(usuariologin.Email, usuariologin.Contrasena);

        if (loginExitoso)
        {
            await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            // 🔹 Redirigir a la pantalla de dispositivos
            await Navigation.PushAsync(new Principal());
        }
        else
        {
            await DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
        }
    }
}