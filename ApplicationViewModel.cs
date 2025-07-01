using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows;
using System.Collections.Generic;
using System.Text.Json;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;

namespace Storage
{
   
    public class ApplicationViewModel : INotifyPropertyChanged, IComparer<Product>
    {
        private AppDbContext _context;
        
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

        private DateTime dateProduction;
        public DateTime DateProduction
        {
            get
            {
                if (myProduct == null)
                    myProduct = new Product();
                return myProduct.DateProduction;
            }
            set
            {
                if (myProduct == null)
                    myProduct = new Product();
                myProduct.DateProduction = value;
                OnPropertyChanget(nameof(DateProduction));
            }
        }
        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get => myProduct.ExpirationDate;
            set
            {
                myProduct.ExpirationDate = value;
                OnPropertyChanget(nameof(ExpirationDate));
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

        private int supplierId;
        public int SupplierId
        {
            get
            {
                if (supplier_ == null)
                    supplier_ = new Supplier();
                return supplier_.Id;
            }

            set
            {
                if (supplier_ == null)
                    supplier_ = new Supplier();

                supplier_.Id = value;
                OnPropertyChanget(nameof(SupplierId));
            }
        }
        private Supplier supplier_;
        public Supplier Supplier_
        {
            get => supplier_;
            set
            {
                supplier_ = value;
                OnPropertyChanget(nameof(Supplier_));
            }
        }
        private string supplierName;
        public string SupplierName
        {
            get
            {
                if (Supplier_ == null)
                    Supplier_ = new Supplier();
                return Supplier_.Name;
            }

            set
            {
                if (Supplier_ == null)
                    Supplier_ = new Supplier();

                Supplier_.Name = value;
                OnPropertyChanget(nameof(SupplierName));
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

        private ObservableCollection<Supplier> suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get => suppliers;
            set
            {
                suppliers = value;
                OnPropertyChanget(nameof(Suppliers));
            }
        }    
       

        public ApplicationViewModel()
        {
            _context = new AppDbContext();
                   
            products = new ObservableCollection<Product>();
            Suppliers = new ObservableCollection<Supplier>();

            Supplier_ = new Supplier();
            myProduct = new Product();
            dateProduction = new DateTime();
            expirationDate = new DateTime();

            currencyList = new ObservableCollection<string> { "руб", "USD" };
            unitList = new ObservableCollection<string> { "шт", "кг", "л", "упак" };

            LoadProductsAsync();

            DateProduction = DateTime.SpecifyKind(DateProduction, DateTimeKind.Utc);
            ExpirationDate = DateTime.SpecifyKind(ExpirationDate, DateTimeKind.Utc);
        }       

        private async Task LoadProductsAsync()
        {      
           
            var pro = await _context.ProductsAll.ToListAsync();
            Products = new ObservableCollection<Product>(pro);

            var sup = await _context.SuppliersAll.ToListAsync();
            Suppliers = new ObservableCollection<Supplier>(sup);
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

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                   (addCommand = new RelayCommand(async obj =>
                   {                       
                       if (myProduct == null)
                           myProduct = new Product();       
                     
                       DateProduction = DateTime.SpecifyKind(DateProduction, DateTimeKind.Utc);
                       ExpirationDate = DateTime.SpecifyKind(ExpirationDate, DateTimeKind.Utc); 

                       myProduct.SupplierId = supplier_.Id;
                       await _context.ProductsAll.AddAsync(myProduct);
                   
                       await _context.SaveChangesAsync();
                       await LoadProductsAsync();                   

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
                   (saveCommand = new RelayCommand(async obj =>
                   {
                       DateProduction = DateTime.SpecifyKind(DateProduction, DateTimeKind.Utc);
                       ExpirationDate = DateTime.SpecifyKind(ExpirationDate, DateTimeKind.Utc);

                       await _context.SaveChangesAsync();
                       await LoadProductsAsync();

                       if (obj is Window window)
                           window.Close();
                   }
                   ));
            }
        }
        private RelayCommand saveAsCommand;    //Сохранение в PDF на рабочий стол
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
        private RelayCommand removeCommand;        //Удалить товар
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                   (removeCommand = new RelayCommand(async obj =>
                   {                       
                       myProduct = obj as Product;
                       if(myProduct != null)
                          _context.ProductsAll.Remove(myProduct);
                       await _context.SaveChangesAsync();
                      
                      await LoadProductsAsync();                                           

                   },
                   (obj) => Products.Count > 0));
            }
        }
        private RelayCommand deleteCommand;  //Списать просроченные товары
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                   (deleteCommand = new RelayCommand(async obj =>
                   {
                       Product product = new Product()
                       { ExpirationDate = DateTime.Now };

                       for (int i = 0; i < Products.Count; i++)
                       {
                           if (Compare(Products[i], product) < 0)
                           {                               
                               _context.ProductsAll.Remove(Products[i]);                               
                           }
                       }
                       await _context.SaveChangesAsync();

                       await LoadProductsAsync();
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
        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                return selectCommand ??
                   (selectCommand = new RelayCommand(obj =>
                   {
                       DeleteProduct deleteProduct = new DeleteProduct();
                       deleteProduct.Show();                       
                   }
                   ));
            }
        }
        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                   (filterCommand = new RelayCommand(obj =>
                   {
                       var pr = new List<Product>(Products);                      
                       pr.Sort(new ApplicationViewModel());                       
                       Products = new ObservableCollection<Product>(pr);                       
                   }                       
                   )) ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public int Compare(Product x, Product y)
        {
            return x.ExpirationDate.CompareTo(y.ExpirationDate);
        }
    }
}
