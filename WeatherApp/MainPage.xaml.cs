using Microsoft.Maui.Controls;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async async OnGetWeatherClicked(object sender, EventArgs e)
        {
            string cityName = cityEntry.Text;
            if (string.IsNullOrWhiteSpace(cityName))
            {
                await DisplayAlert("Error", "Please enter a city name", "OK");
                return;
            }
            string weatherData = await GetWeatherData(cityName);
            weatherLabel.Text = weatherData;
        }
        private async Task<string> GetWeatherData(string cityName)
        {
            try
            {
                string url = ;//IDK

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        WeatherInfo weatherInfo = JsonSerializer.Deserialize<WeatherInfo>(json);

                        string weatherData = $"Temperature: {weatherInfo.Main.Temp}°C, " +
                                             $"Weather: {weatherInfo.Weather[0].Description}, " +
                                             $"Humidity: {weatherInfo.Main.Humidity}%";
                        return weatherData;
                    }
                    else
                    {
                        return "Error: Unable to get wether data. Try again later.";
                    }
                }

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
