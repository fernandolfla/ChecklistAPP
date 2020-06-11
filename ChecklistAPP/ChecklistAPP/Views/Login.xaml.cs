using ChecklistAPP.Models;
using ChecklistAPP.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {
                if (Logar(txtUsuario.Text.ToLower(), txtSenha.Text) != null)
                {
                    //Entrando no sistema
                }
            }else DependencyService.Get<IMessage>().LongAlert("Digite algo em usuário e senha para logar");

        }

        private async Task<Resposta> Logar(string usuario, string senha)
        {
            // using Acr.UserDialogs; Cria as dialog e tost 
            var Dialog = UserDialogs.Instance.Loading("Logando... Aguarde", null, null, true, MaskType.Black);
            Dialog.Show();
                var token = await ApiLogin.Login(usuario, senha);
                if (token != null)
                {
                    if (!string.IsNullOrEmpty(token.access_token) && token.Mensagem.Equals("ok"))
                    {
                        AppSettings.Token = token;
                        var resposta = new Resposta();
                        resposta.Ok = true;
                        //await Navigation.PushModalAsync(new Principal()); // Assim da para voltar
                        Navigation.InsertPageBefore(new Principal(), this);
                        //await Navigation.PushModalAsync(new Principal());
                        //Navigation.RemovePage(this);
                        await Navigation.PopAsync();
                        Dialog.Dispose();
                        return resposta;
                    }
                }
            Dialog.Dispose();
            DependencyService.Get<IMessage>().LongAlert("Usuário ou senha inválido, tente novamente!");
            return null;
        }
    }
}