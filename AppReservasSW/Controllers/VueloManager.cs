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
    public class VueloManager
    {
        const string URL = "http://localhost:49220/api/vuelo/";
        const string URLIngresar = "http://localhost:49220/api/vuelo/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Vuelo>> ObtenerVuelos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Vuelo>>(resultado);
        }

        public async Task<IEnumerable<Vuelo>> ObtenerVuelo(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Vuelo>>(resultado);
        }
        public async Task<Vuelo> Ingresar(Vuelo vuelo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Vuelo>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Vuelo> Actualizar(Vuelo vuelo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Vuelo>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}