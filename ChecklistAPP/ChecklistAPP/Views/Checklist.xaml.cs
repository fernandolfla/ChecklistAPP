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
        //listas a buscar na API via JSON POST
        private List<Veiculo> veiculos;
        private List<Area> areas;
        private List<Item> itens;
        private List<Chave> chaves;
        private Check check;

        public Checklist()
        {
            InitializeComponent();
            Inicializa();

            //Mensagem da tela de checklist com o nome do usuário
            txtNomeUsuario.Text = "Olá, Bem Vindo  " + AppSettings.Token.Usuario.Nome;

            check = null;
            check = new Check();
            check.UsuarioId = AppSettings.Token.Usuario.Id;
            check.FilialId = AppSettings.Token.Usuario.FilialId;

        }

        private async void Inicializa()
        {
            //Mensagem de espera using Acr.UserDialogs
            var Dialog = UserDialogs.Instance.Loading("Logando... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();
            //Busca na API das listas de uma só vez para não ter mais load
            veiculos = await ApiChecklist.BuscarVeiculos(AppSettings.Token);
            areas = await ApiChecklist.BuscarAreas(AppSettings.Token);
            chaves = await ApiChecklist.BuscarChaves(AppSettings.Token); 
            itens = await ApiChecklist.BuscarItens(AppSettings.Token);
            //Carrega os Pics do layout de veículos e Áreas
            picVeiculos.ItemsSource = veiculos;
            picAreas.ItemsSource = areas;
            //Carrega a posição do item na lista
            int p = 0;
            foreach(var item in itens)
            {
                item.posicao = p;
                p++;
            }
            Dialog.Dispose();

        }

        private async void btnProximo_Clicked(object sender, EventArgs e)
        {
            //Recolher a placa e a área
            if(Veiculo_Selecionado())
            {
                if(Area_Selecionado())
                {
                    if (Texto_KM())
                    {
                        //chama a proxima tela com botão de voltar na barra superior
                        await Navigation.PushAsync(new CheckItens(check, itens, chaves));
                    }
                    else DependencyService.Get<IMessage>().LongAlert("Você precisa Digitar o KM inicial");
                }
                else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar uma Área antes de continuar");
            }
            else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar um Veículo antes de continuar");

        }
        //Busca o objeto dos picker
        private bool Veiculo_Selecionado()
        {
            if(picVeiculos.SelectedIndex >= 0)
            {
                check.VeiculoId= veiculos[picVeiculos.SelectedIndex].Id;
                return true;
            }
            return false;
            
        }
        private bool Area_Selecionado()
        {
            if (picAreas.SelectedIndex >= 0)
            {
                check.AreaId = areas[picAreas.SelectedIndex].Id;
                return true;
            }
            return false;
            
        }

        private bool Texto_KM()
        {
            if (!string.IsNullOrEmpty(txtKm.Text))
            {
                try { check.Kminicial = Convert.ToInt32(txtKm.Text); } catch { check.Kminicial = 0; }
                return true;
            }
            return false;
        }


    }
}