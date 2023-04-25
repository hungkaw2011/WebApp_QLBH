using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
namespace Bai12_Hung660_P1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy");
            tb_date.Text = time.ToString();
            tb_idlogin.Text = WindowLogin.id.ToString();
        }

        private void btn_save(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_addProduct(object sender, RoutedEventArgs e)
        {

        }
    }
}
