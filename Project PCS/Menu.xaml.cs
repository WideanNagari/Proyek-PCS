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
            MasterAlatMusik m = new MasterAlatMusik(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen m = new MasterProdusen(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis m = new MasterJenis(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo m = new MasterPromo(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer m = new MasterCustomer(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan m = new MasterKaryawan(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier m = new MasterSupplier(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            MasterMember m = new MasterMember(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris m = new MasterAksesoris(this);
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Transjual_Click(object sender, RoutedEventArgs e)
        {
            TransJual t = new TransJual();
            this.Hide();
            t.ShowDialog();
            this.Show();
        }

        private void Transbeli_Click(object sender, RoutedEventArgs e)
        {
            TransBeli t = new TransBeli();
            this.Hide();
            t.ShowDialog();
            this.Show();
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

        private void Reportjual_Click(object sender, RoutedEventArgs e)
        {
            ReportPenjualan r = new ReportPenjualan();
            this.Hide();
            r.ShowDialog();
            this.Show();
        }

        private void Reportbeli_Click(object sender, RoutedEventArgs e)
        {
            ReportPembelian r = new ReportPembelian();
            this.Hide();
            r.ShowDialog();
            this.Show();
        }

        private void Reportmember_Click(object sender, RoutedEventArgs e)
        {
            ReportMembership r = new ReportMembership();
            this.Hide();
            r.ShowDialog();
            this.Show();
        }

        private void Trans_member_Click(object sender, RoutedEventArgs e)
        {
            TransJualMember tm = new TransJualMember();
            this.Hide();
            tm.ShowDialog();
            this.Show();
        }
    }
}
