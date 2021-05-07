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
    /// Interaction logic for Menu_Master.xaml
    /// </summary>
    public partial class Menu_Master : Window
    {
        public Menu_Master()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Btn_member_Click(object sender, RoutedEventArgs e)
        {
            MasterMember mm = new MasterMember();
            this.Hide();
            mm.ShowDialog();
            this.Show();

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo mpr = new MasterPromo();
            this.Hide();
            mpr.ShowDialog();
            this.Show();
        }

        private void Btn_jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis mj = new MasterJenis();
            this.Hide();
            mj.ShowDialog();
            this.Show();
        }

        private void Btn_produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen mpd = new MasterProdusen();
            this.Hide();
            mpd.ShowDialog();
            this.Show();
        }

        private void Btn_aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris ma = new MasterAksesoris();
            this.Hide();
            ma.ShowDialog();
            this.Show();
        }

        private void Btn_alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik mam = new MasterAlatMusik();
            this.Hide();
            mam.ShowDialog();
            this.Show();
        }

        private void Btn_karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan mk = new MasterKaryawan();
            this.Hide();
            mk.ShowDialog();
            this.Show();
        }

        private void Btn_supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier ms = new MasterSupplier();
            this.Hide();
            ms.ShowDialog();
            this.Show();
        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mc = new MasterCustomer();
            this.Hide();
            mc.ShowDialog();
            this.Show();
        }
    }
}
