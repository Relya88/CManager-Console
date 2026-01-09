using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CManager.Core.Application.Interfaces;
using CManager.Core.Application.Services;
using CManager.Core.Infrastructure.Repos;


namespace CManager.Presentation.GuiApp
{

    public partial class MainWindow : Window
    {
        private readonly ICustomerService _customerService;

        public MainWindow()
        {
            InitializeComponent();

            _customerService = new CustomerService(new CustomerRepo());
        }

        private void ManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomersList.Items.Clear();
            
            var customers = _customerService.GetCustomers();

            foreach (var customer in customers) // tog hjälp av chatgpt för att loopen skulle hantera flera kunder och visa dem en och en i gui
            {
                CustomersList.Items.Add(
                    $"{customer.FirstName} {customer.LastName} - {customer.Email}"
                );
            }
        }
    }
}

