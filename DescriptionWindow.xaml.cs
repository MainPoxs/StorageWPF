﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для ShowRecipi.xaml
    /// </summary>
    public partial class DescriptionWindow : Window
    {
        private readonly AppDbContext _context;
        public DescriptionWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }
    }
}
