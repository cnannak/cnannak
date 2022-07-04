using System.Windows;
using WpfAppTCS_2341269.ViewModels;

namespace WpfAppTCS_2341269
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Employee List";
            DataContext = new EmployeeViewModel();
        }
    }
}
