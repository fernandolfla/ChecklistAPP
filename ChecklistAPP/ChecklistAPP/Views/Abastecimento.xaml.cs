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
    public partial class Abastecimento : ContentPage
    {
        private List<Veiculo> veiculos;
        private List<Fornecedor> fornecedores;
        private Models.Abastecimento abastecer; 
        private Resposta resposta;
        public Abastecimento()
        {
            InitializeComponent();
            Inicializa();

           

        }



        private async void Inicializa()
        {
            var Dialog = UserDialogs.Instance.Loading("Abrindo... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();
            veiculos = await ApiChecklist.BuscarVeiculos(AppSettings.Token);
            fornecedores = await ApiFornecedor.Buscar(AppSettings.Token);
            //Carrega os Pics do layout de veículos e Áreas
            picVeiculos.ItemsSource = veiculos;
            picFornecedor.ItemsSource = fornecedores;
            abastecer = null;
            abastecer = new Models.Abastecimento();
            txtKm.Text = "";
            txtLitros.Text = "";
            txtValor.Text = "";
            abastecer.UsuarioId = AppSettings.Token.Usuario.Id;
            abastecer.FilialId = AppSettings.Token.Usuario.FilialId;
            abastecer.Data = DateTime.Now;
            Dialog.Dispose();
            

        }

        private bool Veiculo_Selecionado()
        {
            if (picVeiculos.SelectedIndex >= 0)
            {
               abastecer.VeiculoId = veiculos[picVeiculos.SelectedIndex].Id;
                return true;
            }
            return false;

        }

        private bool Posto_Selecionado()
        {
            if (picFornecedor.SelectedIndex >= 0)
            {
                abastecer.FornecedorId = fornecedores[picFornecedor.SelectedIndex].Id;
                return true;
            }
            return false;

        }

        private bool Texto_KM()
        {
            if (!string.IsNullOrEmpty(txtKm.Text))
            {
                try { abastecer.Km = Convert.ToInt32(txtKm.Text); } catch { abastecer.Km = 0; }
                return true;
            }
            return false;
        }
        private bool Texto_Valor()
        {
            if (!string.IsNullOrEmpty(txtValor.Text))
            {
                try { abastecer.Valor = Convert.ToDouble(txtValor.Text); } catch { abastecer.Valor = 0; }
                return true;
            }
            return false;
        }
        private bool Texto_Litros()
        {
            if (!string.IsNullOrEmpty(txtLitros.Text))
            {
                try { abastecer.Litros = Convert.ToDouble(txtLitros.Text); } catch { abastecer.Litros = 0; }
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
                Name = "juricheck_NF" + DateTime.Now.ToString()
            });

            byte[] img = File.ReadAllBytes(file.Path);
            string strimg = Convert.ToBase64String(img);
            abastecer.Foto_string = strimg;
            DependencyService.Get<IMessage>().LongAlert("Foto da Nota Fiscal Adicionada");
        }

        private async void btnForoNota_Clicked(object sender, EventArgs e)
        {
            await GetCameraPhotoAsync();
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            if (Veiculo_Selecionado())
            {
                if (Posto_Selecionado())
                {
                    if (Texto_KM() && Texto_Litros() && Texto_Valor())
                    {
                        var Dialog = UserDialogs.Instance.Loading("Enviando... Aguarde", null, null, true, MaskType.Black);
                        Dialog.Show();
                        resposta = await ApiFornecedor.Abastecer(AppSettings.Token, abastecer);
                        Dialog.Dispose();
                        if (resposta != null)
                            if (resposta.Ok)
                            {
                                DependencyService.Get<IMessage>().LongAlert("Abastecimento Salvo com Sucesso");
                                Inicializa();
                            }
                            else DependencyService.Get<IMessage>().LongAlert("Erro ao salvar");
                    }
                    else DependencyService.Get<IMessage>().LongAlert("Você precisa Digitar o KM inicial, o valor e o litro");
                }
                else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar um Posto");
            }
            else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar um Veículo antes de continuar");
        }
    }
}