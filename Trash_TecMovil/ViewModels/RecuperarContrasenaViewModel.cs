using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Trash_TecMovil.Models;

namespace Trash_TecMovil.ViewModels;

public class RecuperarContrasenaViewModel : INotifyPropertyChanged
{
    private RecuperarContrasena _recuperarContrasena;

    public RecuperarContrasena RecuperarContrasena
    {
        get => _recuperarContrasena;
        set
        {
            _recuperarContrasena = value;
            OnPropertyChanged();
        }
    }

    public ICommand RecuperarCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public RecuperarContrasenaViewModel()
    {
        RecuperarContrasena = new RecuperarContrasena();
        RecuperarCommand = new Command(OnRecuperar);
    }

    private async void OnRecuperar()
    {
        // Aquí puedes agregar la lógica para recuperar la contraseña
        if (!string.IsNullOrEmpty(RecuperarContrasena.Email) &&
            !string.IsNullOrEmpty(RecuperarContrasena.CodigoVerificacion))
        {
            // Lógica para recuperar la contraseña
            await Application.Current.MainPage.DisplayAlert("Éxito", "Se ha enviado un enlace para recuperar tu contraseña.", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}