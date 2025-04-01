using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;
using Trash_TecMovil.Models;

namespace TrashTec_MVVM_5B.ViewModels;

public class EditNameViewModel : INotifyPropertyChanged
{
    private CdsModel _bote;

    public string Name
    {
        get => _bote.Name;
        set
        {
            _bote.Name = value;
            OnPropertyChanged();
        }
    }

    public ICommand AcceptCommand { get; }
    public ICommand CancelCommand { get; }

    public EditNameViewModel()
    {
        _bote = new CdsModel { Name = "Nombre actual" }; // Puedes inicializar con datos existentes
        AcceptCommand = new RelayCommand(Accept);
        CancelCommand = new RelayCommand(Cancel);
    }

    private void Accept()
    {
        Application.Current.MainPage.DisplayAlert("Éxito", $"Nombre actualizado a: {Name}", "OK");
    }

    private void Cancel()
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}