using System.Windows.Input;

namespace Trash_TecMovil.ViewModels
{

    public class EliminarBote : BindableObject
    {
        public ICommand CancelarCommand { get; }
        public ICommand AceptarCommand { get; }

        public Models.EliminarBote BoteAEliminar { get; set; }

        public EliminarBote()
        {
            BoteAEliminar = new Models.EliminarBote
            {
                Id = 1,
                Nombre = "Bote de Ejemplo",
                FechaCreacion = DateTime.Now,
                Descripcion = "Ejemplo de bote",
                Activo = true
            };

            CancelarCommand = new Command(OnCancelar);
            AceptarCommand = new Command(OnAceptar);
        }

        private async void OnCancelar()
        {
            await Shell.Current.GoToAsync(".."); // Navegar atrás
        }

        private async void OnAceptar()
        {
            bool eliminado = await Eliminar();

            if (eliminado)
            {
                await Shell.Current.DisplayAlert("Éxito", "Bote eliminado correctamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el bote", "OK");
            }
        }

        private Task<bool> Eliminar()
        {
            // Simula la eliminación real
            return Task.FromResult(true);
        }
    }
}