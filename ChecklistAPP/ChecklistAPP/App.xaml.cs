﻿using ChecklistAPP.Views;
using Xamarin.Forms;

namespace ChecklistAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();



            //MainPage = new MainPage();

            MainPage = new NavigationPage(new Login());
            //MainPage = new Login();

            #if DEBUG
            HotReloader.Current.Run(this);
            #endif
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
