using LaCucina.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaCucina
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoryPage());
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();
            //await RefreshData();
            /* //.test. 
            var recipe = new Recipe()
            {
                Name="test name",
                Category = CategoryDataType.Soups
            };
            await App.LocalDB.SaveItem(recipe);

            var recpies = await App.LocalDB.GetRecpies();

            await DisplayAlert("ok", $"Liczba rek w db {recpies.Count}", "ok");
            */
        }



    }
}
