using LaCucina.Data;
using LaCucina.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LaCucina
{
    public partial class App : Application
    {
        private static LocalDatabase localDb;

        public static LocalDatabase LocalDB
        {
            get
            {
                if (localDb == null)
                {
                    var fileHelper = DependencyService.Get<IFileHelper>();
                    var fullPath = fileHelper.GetLocalFilepath("app.database");
                    localDb = new LocalDatabase(fullPath);
                }

                return localDb;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }/*
        public static ImageSource GetImageByCategory(CategoryDataType cat)
        {
            switch (cat)
            {
                case CategoryDataType.Dessers:
                    return ImageSource.FromResource(string.Empty);
                case CategoryDataType.Soups:
                    return ImageSource.FormFile("Assets/bg.jpeg");
                default:
                    return ImageSource.FormFile("Assets/r0.png");
            }
        }*/
    }
}
