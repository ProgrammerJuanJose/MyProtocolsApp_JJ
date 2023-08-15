using MyProtocols_Juan.Services;
using MyProtocols_Juan.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocols_Juan
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //Definimos la forma de apilar las páginas en la pantalla y cuál es la primera página en ser mostrada
            MainPage = new NavigationPage(new AppLoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
