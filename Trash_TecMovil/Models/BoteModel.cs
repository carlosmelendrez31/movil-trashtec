using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trash_TecMovil.Models
{
    using System.ComponentModel;

    public class BoteModel : INotifyPropertyChanged
    {
        public string Nombre { get; set; } = "";
        public string Tipo { get; set; } = "";
        public int Llenado { get; set; }

        public string LlenadoColor => Llenado switch
        {
            <= 50 => "Green",
            <= 80 => "Yellow",
            _ => "Red"
        };

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
