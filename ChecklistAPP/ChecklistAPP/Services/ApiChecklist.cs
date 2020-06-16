using ChecklistAPP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistAPP.Services
{
    public static class ApiChecklist
    {
        public static HttpResponseMessage ResponseMessage = new HttpResponseMessage();

        public static async Task<List<Area>> BuscarAreas(Token token)
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
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/area/listar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            List<Area> result = JsonConvert.DeserializeObject<List<Area>>(jsonResult);

            return result;

        }

        public static async Task<List<Chave>> BuscarChaves(Token token)
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
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/chave/listar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            List<Chave> result = JsonConvert.DeserializeObject<List<Chave>>(jsonResult);

            return result;

        }

        public static async Task<List<Item>> BuscarItens(Token token)
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
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/item/listar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            List<Item> result = JsonConvert.DeserializeObject<List<Item>>(jsonResult);

            return result;

        }

        public static async Task<List<Veiculo>> BuscarVeiculos(Token token)
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
            resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/veiculo/listar", content);
        }
        catch (Exception ex)
        {
            ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
            ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
        }

        if (!resposta.IsSuccessStatusCode)
            return null;

        var jsonResult = await resposta.Content.ReadAsStringAsync();
        List<Veiculo> result = JsonConvert.DeserializeObject<List<Veiculo>>(jsonResult);

        return result;

        }

        public static async Task<Resposta> Salvar(Token token, Check check)
        {
            if (token == null)
                if (!token.logado)
                    return null;


            //Corrigir erros de serialização de lista dentro de objetos
            //JsonConvert.SerializeObject(check, Formatting.None,
            //            new JsonSerializerSettings()
            //            {
            //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //                ContractResolver = new CamelCasePropertyNamesContractResolver()
            //            });

           


            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(check);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario.Id.ToString());
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/check/salvar", content);
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
