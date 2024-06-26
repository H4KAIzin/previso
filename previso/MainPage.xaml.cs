﻿﻿using System.Text.Json;

namespace previso;

public partial class MainPage : ContentPage
{
	Resposta resposta;
	const string URL = "https://api.hgbrasil.com/weather?woeid&key=eeb4ae8c";
	
	public MainPage(){
		InitializeComponent();
		AtualizaTempo();
	}

	async void AtualizaTempo(){
		try{
			var HttpClient = new HttpClient();
			var Response = await HttpClient.GetAsync(URL);

			if(Response.IsSuccessStatusCode){
				var Content = await Response.Content.ReadAsStringAsync();
				resposta = JsonSerializer.Deserialize<Resposta>(Content);
			}
		}

		catch(Exception e){
			System.Diagnostics.Debug.WriteLine("//ERROR");
		}

		PreencherTela();
	}
	

	void PreencherTela(){

		listafcast.ItemsSource = resposta.results.forecast;

		LabelTemperatura.Text = resposta.results.temp + "ºC".ToString();
		LabelHumidade.Text = resposta.results.humidity.ToString();
		
		LabelForca.Text = resposta.results.wind_speedy.ToString();
		LabelChuva.Text = resposta.results.rain.ToString();
		
		LabelTempo.Text = resposta.results.description;
		LabelDirecao.Text = resposta.results.wind_cardinal;
		LabelFase.Text = resposta.results.moon_phase;
		LabelHora.Text = "Lido as = " + resposta.results.time;
		LabelAmanhecer.Text = resposta.results.sunrise;
		LabelAnoitecer.Text = resposta.results.sunset;

		int temp = 31;
		int sunrise = 0620;
		int sunset = 1850;
		int humidity = 25;

		string date = "11/11/2005";
		string time = "00:00";
		string description = "Tempo nublado";
		string currently = "dia";
		string city = "Apucarana";
		string wind_cardinal = "NE";
		string moon_phase = "Minguante";
		string condition = "";

		double cloudiness = 30;
		double rain = 11;	
		double wind_speedy = 5;

		if(resposta.results.currently == "dia"){
			if(resposta.results.rain > 10)
			Fundo.Source = "diachuv.jpg";
		
			else if(resposta.results.cloudiness > 20)
				Fundo.Source = "dianumb.jpg";

			else
				Fundo.Source = "dialimp.jpg";
		}
		
		else {
			
			if(resposta.results.rain > 10)
			Fundo.Source = "noitechuv.jpg";
		
			else if(resposta.results.cloudiness > 20)
				Fundo.Source = "noitenumb.jpg";

			else
				Fundo.Source = "noitelimp.jpg";
		}
		
	}

}