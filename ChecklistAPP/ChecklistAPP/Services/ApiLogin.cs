using System;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChecklistAPP.Models;

namespace ChecklistAPP.Services
{
    public static class ApiLogin
    {
        public static HttpResponseMessage ResponseMessage = new HttpResponseMessage();

        public static async Task<Token> Login(string usuario, string senha)
        {
            var loginModel = new LoginModel()
            {
                Usuario = usuario,
                Senha = senha,
            };
            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/login/logar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }
            if (!resposta.IsSuccessStatusCode)
                return null;
            var jsonResult = await resposta.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult); // VERIFICAR COMO RECEBER 2 OBJETOS DISTINTOS
            return result;
        }



    }
}
