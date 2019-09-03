using LaCucina.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaCucina
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        private Recipe currentRecipe;

        public DetailPage (Recipe _recipe)
		{
            var scroll = new ScrollView();
            Content = scroll;

            currentRecipe = _recipe;
            InitializeComponent ();
            BindingContext = _recipe;

            string _ingredients = _recipe.Ingredient;
            string[] listIng = _ingredients.Split(';');

            foreach(var x in listIng)
            {
                StackIngList.Children.Add(new Label { Text = x });
                //ingList.Children.Add(new Label { Text = x });
                //ingList.Add(new Label { Text = x });
                //new Label { Text = x };
            }

            switch (_recipe.Rate)
            {
                case 0:
                    imgRate.Source = ImageSource.FromFile("Assets/r0.png");
                    break;
                case 1:
                    imgRate.Source = ImageSource.FromFile("Assets/r1.png");
                    break;
                case 2:
                    imgRate.Source = ImageSource.FromFile("Assets/r2.png");
                    break;
                case 3:
                    imgRate.Source = ImageSource.FromFile("Assets/r3.png");
                    break;
                case 4:
                    imgRate.Source = ImageSource.FromFile("Assets/r4.png");
                    break;
                default:
                    imgRate.Source = ImageSource.FromFile("Assets/r5.png");
                    break;
            }

            if(_recipe.FilePath != null)
                imgOfDish.Source = ImageSource.FromFile(_recipe.FilePath);
            else
                imgOfDish.Source = ImageSource.FromFile("Assets/no-img.jpg");

        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPage(currentRecipe));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Usuń", "Czy jesteś pewień, że chcesz usunąć ten przepis?", "Tak", "Nie");
            if (answer)
            {
                await App.LocalDB.DeleteItem(currentRecipe);
                await Navigation.PopAsync();
            }

        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            string text = currentRecipe.Ingredient + currentRecipe.Recipe_Text_Area;
            await Xamarin.Essentials.Share.RequestAsync(currentRecipe.Name, text);
        }

    }
}