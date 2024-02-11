using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows;
using System.Collections.Generic;
using System.Text.Json;

 namespace Storage
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Product myProduct;
        public Product MyProduct
        {
            get => myProduct;
            set
            {              
                myProduct = value;
                OnPropertyChanget(nameof(MyProduct));               
            }
        }
        public string NameProduct
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.NameProduct;
            }
        
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.NameProduct = value;
                OnPropertyChanget(nameof(NameProduct));              
            }
        }
        public string Description
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.Description;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.Description = value;
                OnPropertyChanget(nameof(Description));
            }
        }

        public decimal Price
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.Price;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.Price = value;
                OnPropertyChanget(nameof(Price));
            }
        }
        public int Quantity
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.Quantity;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.Quantity = value;
                OnPropertyChanget(nameof(Quantity));
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products       
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanget(nameof(Products));  
            }
        }

        readonly string path = "./ListProduct.xml";
        public XmlSerializer xmlSerialize;
        
        public ApplicationViewModel()
        {
            products = new ObservableCollection<Product>();
            myProduct = new Product();
            xmlSerialize = new XmlSerializer
                (typeof(ObservableCollection<Product>));

            if (File.Exists(path))
                LoadFile();
        }       
        public void SaveListOfFile()
        {
            using (var fs = File.Open(path, FileMode.Create))
                xmlSerialize.Serialize(fs, Products); //Сериализация
        }
        public void LoadFile()
        {
            using (var fs = File.Open(path, FileMode.Open))
                Products = xmlSerialize.Deserialize(fs) as
                    ObservableCollection<Product>; //Десериализация
        }
        
        private RelayCommand createProduct;
        public RelayCommand CreateProduct
        {
            get
            {
                return createProduct ??
                    (createProduct = new RelayCommand(obj =>
                    {                                             
                        myProduct = new Product();
                        CreateWindow productWindow = new CreateWindow();
                        productWindow.Show();                       
                    }));
            }             
        }
        private RelayCommand showDescription;
        public RelayCommand ShowDescription
        {
            get
            {
                return showDescription ??
                    (showDescription = new RelayCommand(obj =>
                    {                        
                        DescriptionWindow showDescription = new DescriptionWindow();                        
                        showDescription.Show();                       
                    }));
            }
        }

        private RelayCommand showList;
        public RelayCommand ShowList
        {
            get
            {
                return showList ??
                    (showList = new RelayCommand(obj =>
                    {
                        ShowList showList = new ShowList();
                        showList.Show();
                    }));
            }
        }
        public int SelectIndex = -1;

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                   (addCommand = new RelayCommand(obj =>
                   {
                       if (myProduct == null)
                           myProduct = new Product();

                       Products.Add(myProduct);
                       SaveListOfFile();

                       myProduct = new Product();

                       if (obj is Window window)                                     
                           window.Close();
                   }
                   ));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                   (editCommand = new RelayCommand(obj =>
                   {                       
                       EditWindow editWindow = new EditWindow();
                       editWindow.Show();                   
                   }
                   ));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                   (saveCommand = new RelayCommand(obj =>
                   {    
                       SaveListOfFile();
                       if (obj is Window window)
                           window.Close();
                   }
                   ));
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                   (removeCommand = new RelayCommand(obj =>
                   {                       
                       Product product = obj as Product;
                       if(product != null)
                           Products.Remove(product);                      
                   },
                   (obj) => Products.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }       
    }
}
