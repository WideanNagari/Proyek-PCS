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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            Menu_Report mr = new Menu_Report(this);
            this.Hide();
            mr.Show();
        }

        private void BtnTransaksi_Click(object sender, RoutedEventArgs e)
        {
            Menu_Trans mt = new Menu_Trans(this);
            this.Hide();
            mt.Show();
        }

        private void BtnMaster_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mm = new Menu_Master(this);
            this.Hide();
            mm.Show();
        }
    }
}
