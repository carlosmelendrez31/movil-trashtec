namespace Trash_TecMovil.Models;


public class EliminarBote
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Descripcion { get; set; }
    public bool Activo { get; set; }
}