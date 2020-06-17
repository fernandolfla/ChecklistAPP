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
    public partial class Manutencao : ContentPage
    {
        private List<Veiculo> veiculos;
        private List<Fornecedor> fornecedores;
        private Models.Manutencao manutencao;
        private Resposta resposta;
        public Manutencao()
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
            manutencao = null;
            manutencao = new Models.Manutencao();
            txtKm.Text = "";
            txtServico.Text = "";
            txtValor.Text = "";
            manutencao.UsuarioId = AppSettings.Token.Usuario.Id;
            manutencao.FilialId = AppSettings.Token.Usuario.FilialId;
            manutencao.Data = DateTime.Now;
            Dialog.Dispose();


        }

        private bool Veiculo_Selecionado()
        {
            if (picVeiculos.SelectedIndex >= 0)
            {
                manutencao.VeiculoId = veiculos[picVeiculos.SelectedIndex].Id;
                return true;
            }
            return false;

        }

        private bool Posto_Selecionado()
        {
            if (picFornecedor.SelectedIndex >= 0)
            {
                manutencao.FornecedorId = fornecedores[picFornecedor.SelectedIndex].Id;
                return true;
            }
            return false;

        }

        private bool Texto_KM()
        {
            if (!string.IsNullOrEmpty(txtKm.Text))
            {
                try { manutencao.Km = Convert.ToInt32(txtKm.Text); } catch { manutencao.Km = 0; }
                return true;
            }
            return false;
        }
        private bool Texto_Valor()
        {
            if (!string.IsNullOrEmpty(txtValor.Text))
            {
                try { manutencao.Valor = Convert.ToDouble(txtValor.Text); } catch { manutencao.Valor = 0; }
                return true;
            }
            return false;
        }
        private bool Texto_Servico()
        {
            if (!string.IsNullOrEmpty(txtServico.Text))
                if(txtServico.Text.Length > 3)
                {
                    manutencao.Servico = txtServico.Text;
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
            manutencao.Foto_string = strimg;
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
                    if (Texto_KM() && Texto_Servico() && Texto_Valor())
                    {
                        var Dialog = UserDialogs.Instance.Loading("Enviando... Aguarde", null, null, true, MaskType.Black);
                        Dialog.Show();

                        resposta = await ApiFornecedor.Manutencao(AppSettings.Token, manutencao);

                        Dialog.Dispose();
                        if (resposta != null)
                            if (resposta.Ok)
                            {
                                DependencyService.Get<IMessage>().LongAlert("Manutenção Salva com Sucesso");
                                Inicializa();
                            }
                            else DependencyService.Get<IMessage>().LongAlert("Erro ao salvar");
                    }
                    else DependencyService.Get<IMessage>().LongAlert("Você precisa Digitar o KM inicial, o valor e o litro");
                }
                else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar uma Oficina");
            }
            else DependencyService.Get<IMessage>().LongAlert("Você precisa selecionar um Veículo antes de continuar");
            
        }
    }
}