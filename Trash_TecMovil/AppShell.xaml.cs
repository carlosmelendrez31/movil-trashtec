namespace Trash_TecMovil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CargarNombreUsuario();
        }

        private void CargarNombreUsuario()
        {
            string nombreUsuario = Preferences.Get("NombreUsuario", "Usuario");
            var userToolbar = this.FindByName<ToolbarItem>("UserToolbar");

            if (userToolbar != null)
            {
                userToolbar.Text = nombreUsuario;
            }
        }
    }
}
