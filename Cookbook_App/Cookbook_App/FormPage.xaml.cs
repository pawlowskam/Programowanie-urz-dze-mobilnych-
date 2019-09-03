using LaCucina.Model;
using Plugin.Media;
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
	public partial class FormPage : ContentPage
	{
        private Recipe _recipe;
        string pathToFile;
        private CategoryDataType formcategory;

        public FormPage (Recipe recipe = null, CategoryDataType formcategory = CategoryDataType.Pizza)
        {
            var scroll = new ScrollView();
            Content = scroll;

            InitializeComponent();
            this.formcategory = formcategory;
            //CategoryDataType categoryForm;
            image.Source = ImageSource.FromFile("Assets/no-img.jpg");

            if (recipe != null)
            {
                _recipe = recipe;
                entryName.Text = _recipe.Name;
                entryRate.Text = _recipe.Rate.ToString();
                //entryIngredient.Text = _recipe.Ingredient;
                entryRecipe_Text_Area.Text = _recipe.Recipe_Text_Area;
                this.formcategory = _recipe.Category;
                string _ingredients = recipe.Ingredient;
                string[] listIng = _ingredients.Split(';');
                foreach (var x in listIng)
                {
                    ingList.Children.Add(new Entry { Text = x });
                }
            }
        }
        private async Task AddNewRecipe()
        {
            string ingredients = string.Empty;
            
            foreach(var x in ingList.Children)
            {
                var entry = (Entry)x;
                ingredients += entry.Text + ";";
            }

            var recipe = new Recipe()
            {
                Name = entryName.Text,
                Rate = uint.Parse(entryRate.Text),
                Ingredient = ingredients,
                Recipe_Text_Area = entryRecipe_Text_Area.Text,
                FilePath = pathToFile,
                Category = formcategory,
            };

            if (_recipe != null)
            {
                recipe.ID = _recipe.ID;
            }

            await App.LocalDB.SaveItem(recipe);
            await DisplayAlert("Sukces", "Zapis powiódł się", "OK");
            await Navigation.PushAsync(new ListPage(formcategory));
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await DeleteRecipe();
        }

        private async Task DeleteRecipe()
        {
            if (_recipe != null)
            {
                await App.LocalDB.DeleteItem(_recipe);
                await DisplayAlert("Sukces", "Udało się usunąć przepis", "OK");
                await Navigation.PopAsync();
            }
        }

        private void Add_Next_Clicked(object sender, EventArgs e)
        {
            ingList.Children.Add(new Entry());
        }

        private async void BtnTakePho_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Brak aparatu", ":( Nie ma dostępnego aparatu.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg" //= recipe.Name
            });

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");
            pathToFile = file.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void Add_Recipe_Clicked(object sender, EventArgs e)
        {

            if (entryName.Text == null 
                || entryRecipe_Text_Area.Text == null 
                || entryRate.Text == null
                || oneIng.Text == null)
            {
                await DisplayAlert("Błąd", $"Proszę uzupełnić wszystkie pola", "Ok");
            }

            else if (!uint.TryParse(entryRate.Text, out uint x) || x > 5 || x < 0)
            {
                await DisplayAlert("Błąd", $"W polu Ocena proszę wprowadzić jedynie liczby całkowite od 0 do 5", "Ok");
            }
            
            else
                await AddNewRecipe();

        }
    }
}