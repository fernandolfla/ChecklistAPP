using Acr.UserDialogs;
using ChecklistAPP.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckChaves : ContentPage
    {
        private List<Chave> _chaves;
        private List<Chave> chaves_destaArea;
        private Check _check;
        private List<Check_Chaves> check_chaves;

        public CheckChaves(Check check, List<Chave> chaves)
        {
            InitializeComponent();
            //Variaveis da tela anterior
            _chaves = chaves;
            _check = check;
            //Inicializa a lista de chaves desta área
            chaves_destaArea = new List<Chave>();

            int p = 0;
            foreach(var c in _chaves)
            {
                if(c.AreaId == check.AreaId)
                {
                    c.posicao = p;
                    chaves_destaArea.Add(c);
                    p++;
                }
            }

            //Inicializa a lista
            check_chaves = new List<Check_Chaves>();

            //Carregar os itens na list
            //if (chaves_destaArea.Count > 0)
           // {
                listChaves.ItemsSource = chaves_destaArea;

                //Carrego todos os Switchs como falso pois eles iniciam falsos
                foreach (var chave in chaves_destaArea)
                {
                    check_chaves.Add(new Check_Chaves { Selecionado = false, Chave = chave });
                }
            //}
            //else
            //{
            //    check_chaves.Add(new Check_Chaves { Selecionado = false, Chave = new Chave { Nome = " Chave da Viatura" } });
            //}

            //Nome da Área que o tático selecionou
            labelArea.Text = _check.AreaId.ToString();
        }


        private void MyToggledEventHandler(object sender, ToggledEventArgs e)
        {
            if (sender is View v && v.BindingContext is Chave chave)
            {
                check_chaves[chave.posicao].Selecionado = e.Value;
            }
        }

        private async void btnProximo_Clicked(object sender, EventArgs e)
        {
            var Dialog = UserDialogs.Instance.Loading("Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();

            ConvertChaves();
            _check.Chaves = check_chaves;

            Navigation.InsertPageBefore(new CheckFotos(_check), this);
            await Navigation.PopAsync();
            //await Navigation.PushAsync(new CheckFotos(_check));

            Dialog.Dispose();
        }

        private void ConvertChaves()
        {
            foreach(var c in check_chaves)
            {
                c.ChaveId = c.Chave.Id;
                c.Chave = null;
            }
        }
    }
}