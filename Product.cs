using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Storage
{    
    public class Product : INotifyPropertyChanged
    {       
        private string nameProduct;
        public string NameProduct
        {
            get => nameProduct;
            set
            {
                nameProduct = value;
                OnPropertyChanget(nameof(NameProduct));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanget(nameof(Description));
            }
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanget(nameof(Price));
            }
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanget(nameof(Quantity));
            }
        }       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
