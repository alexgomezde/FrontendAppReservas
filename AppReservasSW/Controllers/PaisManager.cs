using AppReservasSW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppReservasSW.Controllers
{

    public class PaisManager
    {
        const string URL = "http://localhost:49220/api/pais/";
        const string URLIngresar = "http://localhost:49220/api/pais/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public async Task<IEnumerable<Pais>> ObtenerPaises(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Pais>>(resultado);
        }

        public async Task<IEnumerable<Pais>> ObtenerTipoPais(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Pais>>(resultado);
        }


        public async Task<Pais> Ingresar(Pais pais, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Pais>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Pais> Actualizar(Pais pais, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Pais>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }






    }
}