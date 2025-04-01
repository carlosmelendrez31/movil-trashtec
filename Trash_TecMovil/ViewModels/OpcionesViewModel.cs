using System.ComponentModel;
using Trash_TecMovil.Models;

namespace TrashTec_MVVM_5B.ViewModels;

public class OpcionesViewModel : INotifyPropertyChanged
{
    private Opciones _opciones;
    private bool _isOpen;

    public Opciones Opciones
    {
        get => _opciones;
        set
        {
            _opciones = value;
            OnPropertyChanged(nameof(Opciones));
        }
    }

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            OnPropertyChanged(nameof(IsOpen));
            OnPropertyChanged(nameof(OpenButtonText));
        }
    }

    public string OpenButtonText => IsOpen ? "Cerrar" : "Abrir";

    public Command OpenCommand { get; }
    public Command DeleteCommand { get; }
    public Command EditCommand { get; }

    public OpcionesViewModel()
    {
        Opciones = new Opciones { FillLevel = 50 };
        IsOpen = false;

        OpenCommand = new Command(() => ToggleTrashCan());
        DeleteCommand = new Command(() => DeleteTrashCan());
        EditCommand = new Command(() => EditTrashCan());
    }

    private void ToggleTrashCan()
    {
        IsOpen = !IsOpen;
    }

    private void DeleteTrashCan()
    {
        // Lógica para eliminar el bote
    }

    private void EditTrashCan()
    {
        // Lógica para editar el bote
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
