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

namespace Bai9_NguyenTracHung_660_Exe2
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

        private void Button_Click_Them(object sender, RoutedEventArgs e)
        {
            string result = "";
            result = tb_fullname.Text + "-" + cb_gender.Text + "-" + cb_phongban.Text;
            lb_List.Items.Add(result);
        }

        private void Button_Click_Xoa(object sender, RoutedEventArgs e)
        {
            lb_List.Items.Remove(lb_List.SelectedItem);
        }
    }
}
