namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using System.Runtime.CompilerServices;

    public class ProductInformation : Information, INotifyPropertyChanged
    {
        public string Description { get; set; }

        private string title;

        [JsonProperty("title")]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // public IEnumerable<ISupplier> Suppliers { get; set; }
    }
}
