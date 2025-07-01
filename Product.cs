using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Storage
{    
    public class Product : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanget(nameof(Id));
            }
        }

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

        private DateTime dateProduction;
        public DateTime DateProduction
        {
            get => dateProduction;
            set
            {
                dateProduction = value;
                OnPropertyChanget(nameof(DateProduction));
            }
        }

        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get => expirationDate;
            set
            {
                expirationDate = value;
                OnPropertyChanget(nameof(ExpirationDate));
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

        private string currency;
        public string Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanget(nameof(Currency));
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

        private string unit;
        public string Unit
        {
            get => unit;
            set
            {
                unit = value;
                OnPropertyChanget(nameof(Unit));
            }
        }

        private int supplierId;
        public int SupplierId
        {
            get => supplierId;
            set
            {
                supplierId = value;
                OnPropertyChanget(nameof(SupplierId));
            }
        }

        public virtual Supplier Supplier { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
    }
}
