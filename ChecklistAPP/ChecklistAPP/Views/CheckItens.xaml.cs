using Acr.UserDialogs;
using ChecklistAPP.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckItens : ContentPage
    {
        private List<Item> _itens;
        private List<Chave> _chaves;
        private Check _check;
        private List<Check_Item> check_itens;


        public CheckItens(Check check, List<Item> itens, List<Chave> chaves)
        {
            InitializeComponent();
            //Variaveis da tela anterior
            _check = check;
            _itens = itens;
            _chaves = chaves;
            //Carregar os itens na list

            listItens.ItemsSource = _itens;

            //Inicializa a lista
            check_itens = new List<Check_Item>();
            //Carrego todos os Switchs como falso pois eles iniciam falsos
            foreach (var item in _itens) 
            {
                check_itens.Add(new Check_Item { Selecionado = false, Item = item });
            }

        }

      
        private async void btnProximo_Clicked(object sender, EventArgs e)
        {
            var Dialog = UserDialogs.Instance.Loading("Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();

            ConvertItens();
            _check.Items = check_itens;

            Navigation.InsertPageBefore(new CheckChaves(_check, _chaves), this);
            await Navigation.PopAsync();
            //await Navigation.PushAsync(new CheckChaves(_check, _chaves));

            Dialog.Dispose();
        }

        private void MyToggledEventHandler(object sender, ToggledEventArgs e)
        {
            if (sender is View v && v.BindingContext is Item item)
            {
                check_itens[item.posicao].Selecionado = e.Value;
            }

        }

        private void ConvertItens()
        {
            foreach(var item in check_itens)
            {
                item.ItemId = item.Item.Id;
                item.Item = null;
            }
        }


    }
}







