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
    /// Interaction logic for ReportPembelian.xaml
    /// </summary>
    public partial class ReportPembelian : Window
    {
        OracleConnection conn;
        public ReportPembelian()
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

        private void Btn_report_penjualan_Click(object sender, RoutedEventArgs e)
        {
            ReportPenjualan rj = new ReportPenjualan();
            this.Close();
            rj.Show();
        }

        private void Btn_report_member_Click(object sender, RoutedEventArgs e)
        {
            ReportMembership rb = new ReportMembership();
            this.Close();
            rb.Show();
        }

        private void Btn_report_Click(object sender, RoutedEventArgs e)
        {
            Menu_Report mr = new Menu_Report();
            this.Close();
            mr.Show();
        }

        DataTable dtsupplier, dtkaryawan;
        List<string> listx;
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
            supplier.SelectedIndex = 0;
            karyawan.SelectedIndex = 0;
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
            else if (dari.SelectedDate > sampai.SelectedDate) MessageBox.Show("Tanggal awal tidak boleh melebihi tanggal akhir!");
            else
            {
                int nominal = 0;
                if (!subtotal.Text.Equals("")) nominal = Convert.ToInt32(subtotal.Text);
                ReportBeli rpt = new ReportBeli();
                rpt.SetDatabaseLogon(MainWindow.source, MainWindow.pass, MainWindow.userId, "");
                rpt.SetParameterValue("nota", "0");
                rpt.SetParameterValue("tglAwal", dari.SelectedDate);
                rpt.SetParameterValue("tglAkhir", sampai.SelectedDate);
                rpt.SetParameterValue("supplier", supplier.SelectedValue);
                rpt.SetParameterValue("karyawan", karyawan.SelectedValue);
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

            da = new OracleDataAdapter("select nama_supplier as \"nama\", id_supplier as id from supplier order by 2", conn);
            dtsupplier = new DataTable();
            da.Fill(dtsupplier);

            DataRow newRow = dtsupplier.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtsupplier.Rows.InsertAt(newRow, 0);

            supplier.ItemsSource = dtsupplier.DefaultView;
            supplier.DisplayMemberPath = dtsupplier.Columns["nama"].ToString();
            supplier.SelectedValuePath = "ID";

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

            reset();
        }
    }
}
