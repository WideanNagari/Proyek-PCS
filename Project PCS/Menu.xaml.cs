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

        private void Alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik m = new MasterAlatMusik();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen m = new MasterProdusen();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis m = new MasterJenis();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo m = new MasterPromo();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer m = new MasterCustomer();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan m = new MasterKaryawan();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }
    }
}
