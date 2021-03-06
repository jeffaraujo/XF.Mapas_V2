﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using XF.Mapas.App_Code;

namespace XF.Mapas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapaPinCustomView : ContentPage
	{

        string desativado = "Indisponível";
        public int Precisao { get; set; } = (int)GeolocationAccuracy.Medium;
        public string[] Accuracies => Enum.GetNames(typeof(GeolocationAccuracy));
        CancellationTokenSource cts;

        public Location _location { get; set; }


        private async void GetLocalizacao()
        {
            try
            {
                var requisicao = new GeolocationRequest((GeolocationAccuracy)Precisao);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(requisicao, cts.Token);
                _location = location;
            }
            catch (Exception ex)
            {
               await DisplayAlert("Info", "Se fodeu", "OK");
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
        }

        public MapaPinCustomView()
        {
            InitializeComponent();
            
            

            var pin = new PinCustomizado
            {
                Type = PinType.Place,
                Position = new Position(-23.573783, -46.623438),
                Label = "Fiap",
                Address = "Av. Lins de Vasconcelos, 1264, Aclimação",
                Id = "Fiap",
                Localizacao = "https://www.fiap.com.br/"
            };
            meuMapa.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(-23.573783, -46.623438), Distance.FromMiles(1.0)));

            meuMapa.PinCustomizados = new List<PinCustomizado> { pin };
            meuMapa.Pins.Add(pin);
        }



    }
}