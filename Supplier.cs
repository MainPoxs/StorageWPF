using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public class Supplier : INotifyPropertyChanged
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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanget(nameof(Name));
            }
        }
        public virtual ICollection<Product>AllProducts {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanget([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
