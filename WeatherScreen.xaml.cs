using WeatherApplication.Services;

namespace WeatherApplication;

public partial class WeatherScreen : ContentPage
{
    public List<Models.List> WeatherList;
    public WeatherScreen()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await ApiService.GetWeather(39.1381, -84.5294);
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;

        labelCity.Text = result.city.name;
        labelWeatherdesc.Text = result.list[0].weather[0].description;
        LabelTemperature.Text = result.list[0].main.temperature + "°C";
        LabelHumidity.Text = result.list[0].main.humidity + "°%";
        Labelwind.Text = result.list[0].wind.speed + "°km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;



    }
}
