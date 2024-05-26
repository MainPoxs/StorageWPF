using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Storage
{
    public interface IFileService
    {        
        ObservableCollection<Product> OpenFile(string filename);
        void SaveFile
            (string filename, ObservableCollection<Product> products);
    }
    public class XMLFileService : IFileService
    {
        public ObservableCollection<Product> OpenFile(string filename)
        {
            var products = new ObservableCollection<Product>();
            var xmlSerialize = new XmlSerializer
                (typeof(ObservableCollection<Product>));

            using (var fs = File.Open(filename, FileMode.Open))
                products = xmlSerialize.Deserialize(fs) as
                    ObservableCollection<Product>; //Десериализация
            return products;
        }
        public void SaveFile
            (string filename, ObservableCollection<Product> products)
        {
            var xmlSerialize = new XmlSerializer
                (typeof(ObservableCollection<Product>));

            using (var fs = File.Open(filename, FileMode.Create))
                xmlSerialize.Serialize(fs, products); //Сериализация
        }        
    }
}
