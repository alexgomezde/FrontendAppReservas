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
    public class AeropuertoManager
    {
        const string URL = "http://localhost:49220/api/aeropuerto/";
        const string URLIngresar = "http://localhost:49220/api/aeropuerto/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Aeropuerto>> ObtenerAeropuertos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Aeropuerto>>(resultado);
        }

        public async Task<IEnumerable<Aeropuerto>> ObtenerAeropuerto(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Aeropuerto>>(resultado);
        }
        public async Task<Aeropuerto> Ingresar(Aeropuerto aeropuerto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(aeropuerto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Aeropuerto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Aeropuerto> Actualizar(Aeropuerto aeropuerto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(aeropuerto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Aeropuerto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}