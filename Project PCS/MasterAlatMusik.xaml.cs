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
    /// Interaction logic for MasterAlatMusik.xaml
    /// </summary>
    public partial class MasterAlatMusik : Window
    {
        OracleConnection conn;
        DataTable ds, dtSource;
        OracleDataAdapter da, daSource;
        public MasterAlatMusik()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
        }
        private void loadData()
        {
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "select a.id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                "from alat_musik a, jenis_alat_musik j, produsen p " +
                "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen " +
                "order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dgvMusik.ItemsSource = ds.DefaultView;
            conn.Close();
            dgvMusik.Columns[1].Width = 100;
        }
    }
}
