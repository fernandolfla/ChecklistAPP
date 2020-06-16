using Acr.UserDialogs;
using ChecklistAPP.Models;
using ChecklistAPP.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalDetail : ContentPage
    {
        public PrincipalDetail()
        {
            InitializeComponent();

        }

        private void btnSair_Clicked(object sender, EventArgs e)
        {
            AppSettings.Token = null;
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}