﻿using Trash_TecMovil.View;

namespace Trash_TecMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());



        }
    }
}
