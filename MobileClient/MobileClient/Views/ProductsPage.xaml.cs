using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Collections.ObjectModel;
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
        private void OnAdd(){}
        private void OnUpdate() { }
        private void OnDelete() { }
        //private void InitializeComponent()
        //{
        //    throw new NotImplementedException();
        //}
    }
}