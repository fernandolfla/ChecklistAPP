using Acr.UserDialogs;
using ChecklistAPP.Models;
using ChecklistAPP.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Problemas : ContentPage
    {

        private List<Veiculo> veiculos;
        private Models.Problema problema;
        private Resposta resposta;
        public Problemas()
        {
            InitializeComponent();
            Inicializa();

        }
        private async void Inicializa()
        {
            var Dialog = UserDialogs.Instance.Loading("Abrindo... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();
            veiculos = await ApiChecklist.BuscarVeiculos(AppSettings.Token);
            //Carrega os Pics do layout de veículos e Áreas
            picVeiculos.ItemsSource = veiculos;
            problema = null;
            problema = new Models.Problema();
            txtProblema.Text = "";
            problema.UsuarioId = AppSettings.Token.Usuario.Id;
            problema.Data = DateTime.Now;
            ImageView.Source = null;
            Dialog.Dispose();


        }

        private bool Veiculo_Selecionado()
        {
            if (picVeiculos.SelectedIndex >= 0)
            {
                problema.VeiculoId = veiculos[picVeiculos.SelectedIndex].Id;
                return true;
            }
            return false;

        }

        private bool Texto_Problema()
        {
            if (!string.IsNullOrEmpty(txtProblema.Text))
                if(txtProblema.Text.Length > 3)
                {
                    problema.Descricao = txtProblema.Text;
                    return true;
                }
            return false;
        }

        private async Task GetCameraPhotoAsync()
        {
            var media = CrossMedia.Current;

            var file = await media.TakePhotoAsync(new StoreCameraMediaOptions
            {
                //SaveToAlbum = true,
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 90,
                Name = "juricheck_PRO" + DateTime.Now.ToString()
            });
            ImageView.Source = file.Path;
            byte[] img = File.ReadAllBytes(file.Path);
            string strimg = Convert.ToBase64String(img);
            problema.Foto_string = strimg;
            DependencyService.Get<IMessage>().LongAlert("Foto do Problema Adicionado");
        }

        private async void btnFotoProblema_Clicked(object sender, EventArgs e)
        {
            await GetCameraPhotoAsync();
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            if (Veiculo_Selecionado())
            {
                if (Texto_Problema())
                {
                    var Dialog = UserDialogs.Instance.Loading("Enviando... Aguarde", null, null, true, MaskType.Black);
                    Dialog.Show();
                    resposta = await ApiProblemas.Salvar(AppSettings.Token, problema);
                    Dialog.Dispose();
                    if (resposta != null)
                        if (resposta.Ok)
                        {
                            DependencyService.Get<IMessage>().LongAlert("Problema Salvo com Sucesso");
                            Inicializa();
                        }
                  else DependencyService.Get<IMessage>().LongAlert("Erro ao salvar");
                }
                else DependencyService.Get<IMessage>().LongAlert("Você precisa Digitar um problema");
            }
            else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar um Veículo antes de continuar");
        }

       
    }
}
