using Acr.UserDialogs;
using ChecklistAPP.Models;
using ChecklistAPP.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checklist : ContentPage
    {
        private List<Veiculo> veiculos;
        private List<Area> areas;
       
        private List<Item> itens;
        public Checklist()
        {
            InitializeComponent();
            Inicializa();

        }

        private async void Inicializa()
        {
            var Dialog = UserDialogs.Instance.Loading("Logando... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();

             veiculos = await ApiChecklist.BuscarVeiculos(AppSettings.Token);
            areas = await ApiChecklist.BuscarAreas(AppSettings.Token);
            //chaves = await ApiChecklist.BuscarChaves(AppSettings.Token); Tratar chaves na proxima view
            itens = await ApiChecklist.BuscarItens(AppSettings.Token);
            //listaVeiculos.ItemsSource = veiculos;
            picVeiculos.ItemsSource = veiculos;
            picAreas.ItemsSource = areas;
            listItens.ItemsSource = itens;



            Dialog.Dispose();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckChaves());
        }
    }
}