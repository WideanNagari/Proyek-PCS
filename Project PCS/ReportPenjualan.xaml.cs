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
        OracleConnection conn;
        public ReportPenjualan()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Menu_Report mr = new Menu_Report();
            this.Close();
            mr.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Btn_report_pembelian_Click(object sender, RoutedEventArgs e)
        {
            ReportPembelian rb = new ReportPembelian();
            this.Close();
            rb.Show();
        }

        private void Btn_report_member_Click(object sender, RoutedEventArgs e)
        {
            ReportMembership rj = new ReportMembership();
            this.Close();
            rj.Show();
        }

        private void Btn_report_Click(object sender, RoutedEventArgs e)
        {
            Menu_Report mr = new Menu_Report();
            this.Close();
            mr.Show();
        }

        List<string> listx;
        DataTable dtcustomer, dtkaryawan, dtpromo;

        private string katabaru(string kata)
        {
            string kata2 = "";
            if (kata.Length > 0)
            {
                for (int i = 0; i < kata.Length; i++)
                {
                    if (Char.IsDigit(kata[i])) kata2 += kata[i];
                }
            }
            return kata2;
        }

        private void Subtotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = subtotal.SelectionStart;
            string kata2 = katabaru(subtotal.Text);
            subtotal.Text = kata2;
            subtotal.SelectionStart = temp;
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
                rpt.SetDatabaseLogon(MainWindow.source, MainWindow.pass, MainWindow.userId, "");
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
