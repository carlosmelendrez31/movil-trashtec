namespace Trash_TecMovil.Models;

public class Bote
{
    public string Nombre { get; set; }
    public int NivelLlenado { get; set; }

    // Propiedad calculada para determinar el color según el nivel de llenado
    public Color ColorBote
    {
        get
        {
            if (NivelLlenado >= 75)
                return Colors.Red;
            if (NivelLlenado >= 50)
                return Colors.Orange;
            return Colors.Green;
        }
    }
}