﻿using Acr.UserDialogs;
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
    public partial class CheckFotos : ContentPage
    {
		private Check _check;
		private List<Check_Foto> fotos;
		private Resposta resposta;
		public CheckFotos(Check check)
        {
            InitializeComponent();
			//Variaveis da tela anterior
			_check = check;

			//Incia a lista de fotos
			fotos = new List<Check_Foto>();

		}

        private async void TakeFotoButton_OnClicked(object sender, EventArgs e)
        {
			await GetCameraPhotoAsync();
        }


		private async Task GetCameraPhotoAsync()
		{
			var media = CrossMedia.Current;

			var file = await media.TakePhotoAsync(new StoreCameraMediaOptions
			{
				//SaveToAlbum = true,
				PhotoSize = PhotoSize.Small,
				CompressionQuality = 90,
				Name = "juricheck" + DateTime.Now.ToString()
			});

			try
			{   // Retorna o caminho da imagem.	
				ImageView.Source = file.Path; 						  
				//Converte a foto em byte para guaradar no banco
				byte[] img = File.ReadAllBytes(file.Path);
				string strimg = Convert.ToBase64String(img);
				//Adiciona na lista de checks a foto
				fotos.Add(new Check_Foto { FotoString = strimg });
				//ImageSource.FromStream(() => new MemoryStream(Foto_em_byte));   //byte  para imagem
			}
			catch { DependencyService.Get<IMessage>().LongAlert("Foto não carregada, tente novamente"); }
		}

		private async Task GetPhotoAsync() // Pegar foto existente desabilitado por hora
		{
			var media = CrossMedia.Current;

			var file = await media.PickPhotoAsync();

			ImageView.Source = file.Path; // Retorna o caminho da imagem.
		}

		private async void PickImagemButton_OnClicked(object sender, EventArgs e)
		{
			await GetPhotoAsync();
		}

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
			var Dialog = UserDialogs.Instance.Loading("Enviando... Aguarde", null, null, true, MaskType.Black);
			Dialog.Show();

			if (fotos != null)
            {
				_check.Fotos = fotos;
				//Envia via Json para o servidor
				resposta = await ApiChecklist.Salvar(AppSettings.Token,_check);

				if(resposta != null)
					if(resposta.Ok)
                    {
						DependencyService.Get<IMessage>().LongAlert("Checklist Salvo com Sucesso");
						btnEnviar.IsEnabled = false;
						btnFoto.IsEnabled = false;
						await Navigation.PopToRootAsync();



					}
					else DependencyService.Get<IMessage>().LongAlert("Erro ao salvar");
			}


			Dialog.Dispose();

		
		}
    }
}