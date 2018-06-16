using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Godsend.Models;
using Xamarin.Forms;
namespace MobileClient.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadProductsCommand { get; set; }

        public ProductsViewModel()
        {
            Title = "Products";
            Products = new ObservableCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommmand());
        }

        async Task ExecuteLoadProductsCommmand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();                
                var products = await DataStore.GetProductsAsync(true);
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
