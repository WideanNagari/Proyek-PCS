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

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for Menu_Report.xaml
    /// </summary>
    public partial class Menu_Report : Window
    {
        Menu w_menu;
        public Menu_Report(Menu wm)
        {
            InitializeComponent();
            w_menu = wm;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w_menu.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Btn_report_penjualan_Click(object sender, RoutedEventArgs e)
        {
            ReportPenjualan rj = new ReportPenjualan(w_menu);
            this.Close();
            rj.Show();
        }

        private void Btn_report_pembelian_Click(object sender, RoutedEventArgs e)
        {
            ReportPembelian rb = new ReportPembelian(w_menu);
            this.Close();
            rb.Show();
        }

        private void Btn_report_member_Click(object sender, RoutedEventArgs e)
        {
            ReportMembership rm = new ReportMembership(w_menu);
            this.Close();
            rm.Show();
        }
    }
}
