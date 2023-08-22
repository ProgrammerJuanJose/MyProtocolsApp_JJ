using MyProtocols_Juan.ViewModels;
using MyProtocols_Juan.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyProtocols_Juan
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
