using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class CurrencyRate
{
    public string Code { get; set; }
    public string Codein { get; set; }
    public string Name { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    public string VarBid { get; set; }
    public string PctChange { get; set; }
    public string Bid { get; set; }
    public string Ask { get; set; }
    public string Timestamp { get; set; }
    public DateTime CreateDate { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        using (var client = new HttpClient())
        {
            Console.WriteLine("Entrez le code ISO de la devise que vous souhaitez comparer avec le Réal brésilien (par exemple, USD, EUR, BTC) :");
            string currencyCode = Console.ReadLine().ToUpper();
            string url = $"https://economia.awesomeapi.com.br/json/last/{currencyCode}-BRL";
            
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var rates = JsonConvert.DeserializeObject<Dictionary<string, CurrencyRate>>(json);

                if (rates.ContainsKey($"{currencyCode}BRL"))
                {
                    PrintCurrencyRate(rates[$"{currencyCode}BRL"]);
                }
                else
                {
                    Console.WriteLine("La devise spécifiée n'existe pas.");
                }
            }
            else
            {
                Console.WriteLine($"Échec de récupération des données. Code d'état : {response.StatusCode}");
            }
        }
    }

    static void PrintCurrencyRate(CurrencyRate rate)
    {
        Console.WriteLine($"Code: {rate.Code}");
        Console.WriteLine($"Nom: {rate.Name}");
        Console.WriteLine($"Haute: {rate.High}");
        Console.WriteLine($"Basse: {rate.Low}");
        Console.WriteLine($"Variation Bid: {rate.VarBid} %");
        Console.WriteLine($"Changement pourcentage: {rate.PctChange} %");
        Console.WriteLine($"Bid: {rate.Bid}");
        Console.WriteLine($"Ask: {rate.Ask}");
        Console.WriteLine($"Timestamp: {rate.Timestamp}");
        Console.WriteLine($"Date de création: {rate.CreateDate}");
        Console.WriteLine();
    }
}
