using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class CheckFotos : ContentPage
    {
        public CheckFotos()
        {
            InitializeComponent();
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
				Name = "NomeDaSuaFoto"
			});
			ImageView.Source = file.Path; // Retorna o caminho da imagem.
		}

		private async Task GetPhotoAsync()
		{
			var media = CrossMedia.Current;

			var file = await media.PickPhotoAsync();

			ImageView.Source = file.Path; // Retorna o caminho da imagem.
		}

		private async void PickImagemButton_OnClicked(object sender, EventArgs e)
		{
			await GetPhotoAsync();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}