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
using System.Windows.Shapes;
using Bai12_Hung660_P1.Models;
namespace Bai12_Hung660_P1
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public static string id;
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QLBanHangContext db = new QLBanHangContext();
            var query = from account in db.Users
                        select new
                        {
                            account.TenDn,
                            account.MatKhau
                        };
            var txtId = idLogin.Text;
            var pw = pwLogin.Text;
            foreach (var item in query)
            {
                if (item.TenDn.CompareTo(txtId)==0 && item.MatKhau.CompareTo(pw) == 0)
                {
                    MainWindow main = new MainWindow();
                    id = item.TenDn;
                    main.Show();
                    break;
                }
                else
                {
                    MessageBox.Show("Không tồn tại tài khoản này trong cơ sở dữ liệu", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
            }
        }
    }
}
