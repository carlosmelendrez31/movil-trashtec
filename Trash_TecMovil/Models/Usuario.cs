using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Trash_TecMovil.Models;

public class Usuario
{
    private string _email;
    private string _contrasena;

    public string nombreusuario;
    public string email
    {
        get => _email;
        set
        {
            if (_email == value) return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string contrasena
    {
        get => _contrasena;
        set
        {
            if (_contrasena == value) return;
            _contrasena = value;
            OnPropertyChanged(nameof(contrasena));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
