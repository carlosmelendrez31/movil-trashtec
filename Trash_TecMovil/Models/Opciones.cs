using System.ComponentModel;

namespace Trash_TecMovil.Models;

public class Opciones : INotifyPropertyChanged
{
    private int _fillLevel;

    public int FillLevel
    {
        get => _fillLevel;
        set
        {
            _fillLevel = value;
            OnPropertyChanged(nameof(FillLevel));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}