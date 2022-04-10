using ConsumindoAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumindoAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Informe o CEP para a consulta: ");
            string cepReceived = Console.ReadLine().ToString();

            // Defino a url base
            string baseUrl = $"https://viacep.com.br/ws/{cepReceived}/";
            // Recebo o parâmetro
            Console.WriteLine("Relizando consulta para o CEP: " + cepReceived);

            // Faço a instância do objeto HttpClient client, recebendo como BaseAddress uma instância de Uri com a minha url base
            HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            // Faço uma requisição assíncrona com GetAsync passando o endpoint e recebo os valores em response
            // Obs.: Como é uma requisição assíncrona, devo fazer uso do await e tornar meu método de void para 'async Task'
            var response = await client.GetAsync("json");
            // Pego o conteúdo, acessando a propriedade 'Content' e utilizando o método 'ReadAsStringAsync()' para fazer a leitura
            var content = await response.Content.ReadAsStringAsync();
            // Converto o Json para um objeto, fazendo uso da classe ApiCep como tipo
            // Para isso, precisei criar a classes de acordo com o retorno (ApiCep) 'https://quicktype.io/csharp'
            // E como vou receber informações de Cep, o tipo é um ApiCep (ApiCep)
            var info = JsonConvert.DeserializeObject<ApiCep>(content);

            Console.WriteLine($"\n" +
                $"Logradouro: {info.Logradouro}\n" +
                $"Complemento: {info.Complemento}\n" +
                $"Bairro: {info.Bairro}\n" +
                $"Localidade: {info.Localidade}\n" +
                $"UF: {info.Uf}\n" +
                $"IBGE: {info.Ibge}\n" +
                $"GIA: {info.Gia}\n" +
                $"DDD: {info.Ddd}\n" +
                $"SIAFI: {info.Siafi}\n");

            Console.ReadKey();
        }
    }
}

