using ChecklistAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ChecklistAPP.Services
{
    public static class ApiProblemas
    {
        public static HttpResponseMessage ResponseMessage = new HttpResponseMessage();

        public static async Task<List<Problema>> Buscar(Token token)
        {
            if (token == null)
                if (!token.logado)
                    return null;

            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject("");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario.Id.ToString());
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/problema/listar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            List<Problema> result = JsonConvert.DeserializeObject<List<Problema>>(jsonResult);

            return result;

        }

        
        public static async Task<Resposta> Salvar(Token token, Problema problema)
        {
            if (token == null)
                if (!token.logado)
                    return null;

            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(problema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario.Id.ToString());
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/problema/salvar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            Resposta result = JsonConvert.DeserializeObject<Resposta>(jsonResult);

            return result;

        }

        public static async Task<Resposta> Atualizar(Token token, Problema problema)
        {
            if (token == null)
                if (!token.logado)
                    return null;

            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(problema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario.Id.ToString());
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/problema/atualizar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            Resposta result = JsonConvert.DeserializeObject<Resposta>(jsonResult);

            return result;

        }


    }
}
