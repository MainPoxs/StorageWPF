using System.Windows;

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly AppDbContext _context;
        public EditWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }
    }
}
