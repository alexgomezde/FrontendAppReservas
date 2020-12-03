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
    public class AsientoManager
    {
        const string URL = "http://localhost:49220/api/asiento/";
        const string URLIngresar = "http://localhost:49220/api/asiento/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Asiento>> ObtenerAsientos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Asiento>>(resultado);
        }

        public async Task<IEnumerable<Asiento>> ObtenerAsiento(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Asiento>>(resultado);
        }
        public async Task<Asiento> Ingresar(Asiento asiento, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(asiento), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Asiento>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Asiento> Actualizar(Asiento asiento, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(asiento), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Asiento>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}