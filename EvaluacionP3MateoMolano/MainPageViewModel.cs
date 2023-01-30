using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvaluacionP3MateoMolano
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private decimal _usdRate;

        public decimal UsdRate { get => _usdRate; set { _usdRate = value; OnProperyChanged(nameof(UsdRate));  } }
        private decimal _eurRate;
        public decimal EurRate { get => _eurRate; set { _eurRate = value; OnProperyChanged(nameof(EurRate)); } }
        private decimal _audRate;
        public decimal AudRate { get => _audRate; set { _audRate = value; OnProperyChanged(nameof(AudRate)); } }
        public ICommand ExchangeMateoMolanoCom { get; set; }
        public MainPageViewModel()
        {
            ExchangeMateoMolanoCom = new Command(async () => await ExchangeMateoMolano());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ExchangeMateoMolano()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("apiKey", "ao1X6w7J9DEw0FKUtY4yfxAr0vaJIbHv");
        var response = await httpClient.GetAsync("https://api.apilayer.com/fixer/latest?symbols=USD,EUR,AUD&base=CHE");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var rates = JsonSerializer.Deserialize<CurrentRates>(data, options);
                UsdRate = rates.Rates.Usd;
                EurRate = rates.Rates.Eur;
                AudRate = rates.Rates.Aud;
            }
    }
    }
    public class CurrentRates
    {
        public Rate Rates { get; set; }
    }
    public class Rate
    {
        public decimal Usd { get; set; }
        public decimal Eur { get; set; }
        public decimal Aud { get; set; }
    }
}
