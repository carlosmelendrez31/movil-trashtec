using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Microsoft.Win32;
using Trash_TecMovil.Models;
using Trash_TecMovil.View;

namespace Trash_TecMovil.ViewModels;

public class RegistroViewModel : INotifyPropertyChanged
{
    private Usuario _usuario = new Usuario();
    private string _passwordError;
    private string _emailError;

    public string Nombre
    {
        get => _usuario.nombreusuario;
        set
        {
            _usuario.nombreusuario = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _usuario.email;
        set
        {
            _usuario.email = value;
            OnPropertyChanged();
            ValidateEmail();
        }
    }

    public string Password
    {
        get => _usuario.contrasena;
        set
        {
            _usuario.contrasena = value;
            OnPropertyChanged();
            ValidatePassword();
        }
    }

    public string PasswordError
    {
        get => _passwordError;
        set
        {
            _passwordError = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPasswordErrorVisible));
        }
    }

    public string EmailError
    {
        get => _emailError;
        set
        {
            _emailError = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmailErrorVisible));
        }
    }

    public bool IsPasswordErrorVisible => !string.IsNullOrEmpty(PasswordError);
    public bool IsEmailErrorVisible => !string.IsNullOrEmpty(EmailError);

    private void ValidateEmail()
    {
        if (string.IsNullOrEmpty(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            EmailError = "Por favor, ingresa un correo electrónico válido.";
        }
        else
        {
            EmailError = string.Empty;
        }
    }

    private void ValidatePassword()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8 ||
            !Regex.IsMatch(Password, @"[A-Z]") ||
            !Regex.IsMatch(Password, @"[0-9]") ||
            !Regex.IsMatch(Password, @"[\W_]"))
        {
            PasswordError = "La contraseña debe tener al menos 8 caracteres, una mayúscula, un número y un carácter especial.";
        }
        else
        {
            PasswordError = string.Empty;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void OnLogin()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Login());
    }

}
