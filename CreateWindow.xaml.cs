using System.Windows;

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow1.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        private readonly AppDbContext _context;
        public CreateWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }
    }
}
