using System;
using System.IO;
using Microsoft.Win32;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.ObjectModel;

namespace Storage
{
    public interface IDialogService
    {       
        string FilePath { get; set; } //путь к выбранному файлу      
        void CreatePdf(); //создание PDF документа
        bool SaveFileDialog(); //сохранение файла в PDF
    }
    public class DefaultDialogService : IDialogService
    {
        private ApplicationViewModel app = new();
        private Document document;
        public string FilePath { get; set; } 
        
        private ObservableCollection<Product> pr;
        public ObservableCollection<Product> Pr
        {
            get => app.Products;
            set
            {
               pr = app.Products;
            }
        }

        public void CreatePdf()
        {
            //Создание PDF-документа
            document = new Document();

            //Добавление страницы    
            Page page = document.Pages.Add();           

            //Создание таблицы
            Table table = new Table();

            //Ширина столбцов таблицы
            table.ColumnWidths = "50 50 50";

            //Установка границ для ячеек таблицы
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.1F,
                Color.FromRgb(System.Drawing.Color.LightGray));

            //Цвет границы таблицы
            table.Border = new BorderInfo(BorderSide.All, 1F,
                Color.FromRgb(System.Drawing.Color.LightGray));

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 5f;
            marginInfo.Left = 5f;
            marginInfo.Right = 5f;
            marginInfo.Bottom = 5f;
            table.DefaultCellPadding = marginInfo;

            //Добавление строки в таблицу
            Row row = table.Rows.Add();

            //Добавление ячейки таблицы
            row.Cells.Add("№");
            row.Cells.Add("Наименование");
            row.Cells.Add("Описание");
            row.Cells.Add("Цена");
            row.Cells.Add("Валюта");
            row.Cells.Add("Количество");
            row.Cells.Add("Единица");

            for (int rowCount = 0; rowCount < Pr.Count; rowCount++)
            {
                //Добавление строки в таблицу
                row = table.Rows.Add();
                //Добавление ячейки таблицы
                row.Cells.Add((rowCount + 1).ToString());
                row.Cells.Add(Pr[rowCount].NameProduct);
                row.Cells.Add(Pr[rowCount].Description);
                row.Cells.Add(Pr[rowCount].Price.ToString());
                row.Cells.Add(Pr[rowCount].Currency);
                row.Cells.Add(Pr[rowCount].Quantity.ToString());
                row.Cells.Add(Pr[rowCount].Unit);
            }
            //Добавление таблицы на страницу
            page.Paragraphs.Add(table);            
        }

        public bool SaveFileDialog()
        {         
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF файл(*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                using var fs = new FileStream
                    (FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                {
                    CreatePdf();
                  
                    //Сохранение PDF-документа
                    document.Save(fs);
                }             
                return true;
            }
            return false;
        }       
    }
}
