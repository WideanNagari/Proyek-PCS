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
using Oracle.DataAccess.Client;
using System.Data;

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for ReportPenjualan.xaml
    /// </summary>
    public partial class ReportPenjualan : Window
    {
        Menu w_menu;
        OracleConnection conn;
        public ReportPenjualan(Menu wm)
        {
            InitializeComponent();
            conn = MainWindow.conn;
            w_menu = wm;
        }

        List<string> listx;
        DataTable dtcustomer, dtkaryawan, dtpromo;

        private void Button_Click(object sender, RoutedEventArgs e) { this.Close(); w_menu.Show(); }
        private string katabaru(string kata)
        {
            string kata2 = "";
            if (kata.Length > 0)
            {
                if (kata[kata.Length - 1] != '1' && kata[kata.Length - 1] != '2' && kata[kata.Length - 1] != '3' && kata[kata.Length - 1] != '4' && kata[kata.Length - 1] != '5'
                    && kata[kata.Length - 1] != '6' && kata[kata.Length - 1] != '7' && kata[kata.Length - 1] != '8' && kata[kata.Length - 1] != '9' && kata[kata.Length - 1] != '0')
                {
                    for (int i = 0; i < kata.Length - 1; i++)
                    {
                        kata2 += kata[i];
                    }
                }
                else
                {
                    for (int i = 0; i < kata.Length; i++)
                    {
                        kata2 += kata[i];
                    }
                }
            }
            return kata2;
        }

        private void Subtotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            string kata2 = katabaru(subtotal.Text);
            subtotal.Text = kata2;
            subtotal.SelectionStart = subtotal.Text.Length;
        }
        private void reset()
        {
            customer.SelectedIndex = 0;
            karyawan.SelectedIndex = 0;
            promo.SelectedIndex = 0;
            subs.SelectedIndex = 0;
            subtotal.Text = "";
            dari.Text = "";
            sampai.Text = "";
        }

        private void Tampil_Click(object sender, RoutedEventArgs e)
        {
            if (dari.SelectedDate == null)
            {
                MessageBox.Show("Pilih tanggal awal terlebih dahulu!");
            }
            else if (sampai.SelectedDate == null)
            {
                MessageBox.Show("Pilih tanggal akhir terlebih dahulu!");
            }
            else if (dari.SelectedDate>sampai.SelectedDate) MessageBox.Show("Tanggal awal tidak boleh melebihi tanggal akhir!");
            else
            {
                int nominal = 0;
                if (!subtotal.Text.Equals("")) nominal = Convert.ToInt32(subtotal.Text);
                ReportJual rpt = new ReportJual();
                rpt.SetDatabaseLogon("widean", "219116863", "widean", "");
                rpt.SetParameterValue("nota", "0");
                rpt.SetParameterValue("tglAwal", dari.SelectedDate);
                rpt.SetParameterValue("tglAkhir", sampai.SelectedDate);
                rpt.SetParameterValue("customer", customer.SelectedValue);
                rpt.SetParameterValue("karyawan", karyawan.SelectedValue);
                rpt.SetParameterValue("promo", promo.SelectedValue);
                rpt.SetParameterValue("subtotal", nominal);
                rpt.SetParameterValue("subs", subs.SelectedValue);
                cReport.ViewerCore.ReportSource = rpt;
            }
        }

        OracleDataAdapter da;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cReport.Owner = Window.GetWindow(this);

            listx = new List<string>();
            listx.Add("Greater");
            listx.Add("Lesser");
            subs.ItemsSource = null;
            subs.ItemsSource = listx;

            da = new OracleDataAdapter("select nama_customer as \"nama\", id_customer as id from customer order by 2", conn);
            dtcustomer = new DataTable();
            da.Fill(dtcustomer);

            DataRow newRow = dtcustomer.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtcustomer.Rows.InsertAt(newRow, 0);

            customer.ItemsSource = dtcustomer.DefaultView;
            customer.DisplayMemberPath = dtcustomer.Columns["nama"].ToString();
            customer.SelectedValuePath = "ID";

            da = new OracleDataAdapter("select nama_karyawan as \"nama\", id_karyawan as id from karyawan order by 2", conn);
            dtkaryawan = new DataTable();
            da.Fill(dtkaryawan);

            newRow = dtkaryawan.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtkaryawan.Rows.InsertAt(newRow, 0);

            karyawan.ItemsSource = dtkaryawan.DefaultView;
            karyawan.DisplayMemberPath = dtkaryawan.Columns["nama"].ToString();
            karyawan.SelectedValuePath = "ID";

            da = new OracleDataAdapter("select nama_promo as \"nama\", kode_promo as id from promo order by 2", conn);
            dtpromo = new DataTable();
            da.Fill(dtpromo);

            newRow = dtpromo.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtpromo.Rows.InsertAt(newRow, 0);

            promo.ItemsSource = dtpromo.DefaultView;
            promo.DisplayMemberPath = dtpromo.Columns["nama"].ToString();
            promo.SelectedValuePath = "ID";

            reset();
        }
    }
}
