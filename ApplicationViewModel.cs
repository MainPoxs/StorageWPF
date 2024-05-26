using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Storage
{
   
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private IFileService fileService;
        private IDialogService dialogService;
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

        private string currency;
        public string Currency
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.Currency;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.Currency = value;
                OnPropertyChanget(nameof(Currency));
            }
        }
        private string unit;
        public string Unit
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.Unit;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();

                myProduct.Unit = value;
                OnPropertyChanget(nameof(Unit));
            }
        }

        private ObservableCollection<string> currencyList;
        public ObservableCollection<string> CurrencyList
        {
            get => currencyList;
            set
            {
                currencyList = value;
                OnPropertyChanget(nameof(CurrencyList));
            }
        }
        private ObservableCollection<string> unitList;
        public ObservableCollection<string> UnitList
        {
            get => unitList;
            set
            {
                unitList = value;
                OnPropertyChanget(nameof(UnitList));
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
        
        public ApplicationViewModel()
        {
            fileService = new XMLFileService();
            //dialogService = new DefaultDialogService();
            products = new ObservableCollection<Product>();
            myProduct = new Product();
            currencyList = new ObservableCollection<string> { "руб", "USD" };
            unitList = new ObservableCollection<string> { "шт", "кг", "л", "упак" };            

            if (File.Exists(path))
                LoadFile();
        }       
        public void SaveListOfFile() =>
            fileService.SaveFile(path, products);
   
        public void LoadFile() =>
            products = fileService.OpenFile(path);       

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
        private RelayCommand saveAsCommand;
        public RelayCommand SaveAsCommand
        {
            get
            {
                return saveAsCommand ??
                   (saveAsCommand = new RelayCommand(obj =>
                   {
                       dialogService = new DefaultDialogService();
                       dialogService.SaveFileDialog();                    
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
        private RelayCommand previewCommand;
        public RelayCommand PreviewCommand
        {
            get
            {
                return previewCommand ??
                   (previewCommand = new RelayCommand(obj =>
                   {
                       PreviewWindow previewWindow = new PreviewWindow();
                       previewWindow.Show();
                   }
                   ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }       
    }
}
