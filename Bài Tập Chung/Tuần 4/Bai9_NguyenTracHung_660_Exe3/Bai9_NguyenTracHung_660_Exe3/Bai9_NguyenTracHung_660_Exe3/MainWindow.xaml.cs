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

namespace Bai9_NguyenTracHung_660_Exe3
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

        private void btn_add(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text.CompareTo("")==0)
            {
                MessageBox.Show("Name doesn't Exist");
            }
            
            else
            {
                lb_list.Items.Add("Họ Tên: " + tb_name.Text);
                lb_list.Items.Add("Giới Tính: " + (radNam.IsChecked == true ? "Nam" : "Nữ"));
                lb_list.Items.Add("Trình trạng hôn nhân: " + (radMarried.IsChecked == true ? "Đã kết hôn" : "Chưa kết hôn"));
                lb_list.Items.Add("Sở thích: " + (cb_climb.IsChecked == true ? "Leo núi" : " ") + " " +
                    (cb_swim.IsChecked == true ? "Bơi" : " ") + " " +
                    (cb_soccer.IsChecked == true ? "Đá bóng" : " ") + " " +
                    (cb_music.IsChecked == true ? "Nghe nhạc" : " "));
            }
        }

        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
