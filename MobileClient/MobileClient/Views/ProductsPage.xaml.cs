using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileClient.Models;
using System.Net.Http;

namespace MobileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductsPage : ContentPage
	{
        private const string Url = "http://localhost:56440/api/product/all";
        private readonly HttpClient client = new HttpClient();
        //private ObservableCollection<>
        

        public ProductsPage()
        {
            InitializeComponent();
        }

        private void OnAdd(object sender, EventArgs e)
        {

        }

        private void OnUpdate(object sender, EventArgs e)
        {

        }

        private void OnDelete(object sender, EventArgs e)
        {

        }
    }
}