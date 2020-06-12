using Acr.UserDialogs;
using ChecklistAPP.Models;
using ChecklistAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckChaves : ContentPage
    {
        private List<Chave> chaves;
        public CheckChaves()
        {
            InitializeComponent();
            Inicializa();
        }

        private async void Inicializa()
        {
            var Dialog = UserDialogs.Instance.Loading("Logando... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();

          
            chaves = await ApiChecklist.BuscarChaves(AppSettings.Token); 
            
            listChaves.ItemsSource = chaves;



            Dialog.Dispose();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckFotos());
        }
    }
}