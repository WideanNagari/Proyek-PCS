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
    /// Interaction logic for Nota.xaml
    /// </summary>
    public partial class Nota : Window
    {
        TransJual trans1;
        TransBeli trans2;
        TransJualMember trans3;
        ReportJual rj;
        ReportBeli rb;
        ReportMember rm;
        string nomorNota;
        public Nota(string nama, TransJual t, string nota)
        {
            InitializeComponent();
            judul.Content = nama;
            trans1 = t;
            nomorNota = nota;
        }
        public Nota(string nama, TransBeli t, string nota)
        {
            InitializeComponent();
            judul.Content = nama;
            trans2 = t;
            nomorNota = nota;
        }
        public Nota(string nama, TransJualMember t, string nota)
        {
            InitializeComponent();
            judul.Content = nama;
            trans3 = t;
            nomorNota = nota;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if(trans1!=null) trans1.Show();
            else if(trans2!=null) trans2.Show();
            else if(trans3!=null) trans3.Show();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Report\\" + nomorNota + ".pdf";
            if (rj != null)
            {
                rj.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
            }
            else if (rb != null)
            {
                rb.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
            }
            else if (rm != null)
            {
                rm.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select to_char(sysdate,'DD/MM/YYYY') from dual";
            conn.Close();
            conn.Open();
            DatePicker date = new DatePicker();
            date.Text = cmd.ExecuteScalar().ToString();
            conn.Close();
            if (trans1 != null)
            {
                rj = new ReportJual();
                rj.SetDatabaseLogon("widean", "219116863", "widean", "");
                rj.SetParameterValue("nota", nomorNota);
                rj.SetParameterValue("tglAwal", date.SelectedDate);
                rj.SetParameterValue("tglAkhir", date.SelectedDate);
                rj.SetParameterValue("customer", "0");
                rj.SetParameterValue("karyawan", "0");
                rj.SetParameterValue("promo", "0");
                rj.SetParameterValue("subtotal", "0");
                rj.SetParameterValue("subs", "Greater");
                cReport.ViewerCore.ReportSource = rj;
            }
            else if (trans2 != null)
            {
                rb = new ReportBeli();
                rb.SetDatabaseLogon("widean", "219116863", "widean", "");
                rb.SetParameterValue("nota", nomorNota);
                rb.SetParameterValue("tglAwal", date.SelectedDate);
                rb.SetParameterValue("tglAkhir", date.SelectedDate);
                rb.SetParameterValue("supplier", "0");
                rb.SetParameterValue("karyawan", "0");
                rb.SetParameterValue("subtotal", "0");
                rb.SetParameterValue("subs", "Greater");
                cReport.ViewerCore.ReportSource = rb;
            }

            else if (trans3 != null)
            {
                rm = new ReportMember();
                rm.SetDatabaseLogon("widean", "219116863", "widean", "");
                rm.SetParameterValue("nota", nomorNota);
                rm.SetParameterValue("tglAwal", date.SelectedDate);
                rm.SetParameterValue("tglAkhir", date.SelectedDate);
                rm.SetParameterValue("customer", "0");
                rm.SetParameterValue("karyawan", "0");
                rm.SetParameterValue("subtotal", "0");
                rm.SetParameterValue("subs", "Greater");
                rm.SetParameterValue("member", "0");
                rm.SetParameterValue("status", "1");
                cReport.ViewerCore.ReportSource = rm;
            }
        }
    }
}
