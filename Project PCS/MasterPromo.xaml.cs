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
    /// Interaction logic for MasterPromo.xaml
    /// </summary>
    public partial class MasterPromo : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari, inserts;
        List<string> listx;
        public MasterPromo()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select kode_promo as \"Kode Promo\", nama_promo as \"Nama Promo\", besar_potongan as \"Besar Potongan\" " +
                "from promo order by 1",conn);

            dgvPromo.Columns.Clear();
            da.Fill(ds);
            DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
            Binding bind = new Binding("Besar Potongan") { StringFormat = "Rp. {0:N0}" };
            newC.Binding = bind;
            dgvPromo.ItemsSource = ds.DefaultView;
            dgvPromo.Columns.Insert(2, newC);
            dgvPromo.Columns.RemoveAt(3);
            conn.Close();
        }

        private void reset()
        {
            dgvPromo.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;

            rnama.IsChecked = false;
            rkode.IsChecked = false;
            rpotongan.IsChecked = false;

            keyword.Text = "";
            id.Text = "";
            nama.Text = "";
            nominal.Text = "";
            keyword.Text = "";

            id.IsReadOnly = false;
            inserts = 1;
        }

        private void DgvProdusen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvPromo.SelectedIndex != -1)
            {
                inserts = 0;
                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id.Text = ds.Rows[dgvPromo.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvPromo.SelectedIndex][1].ToString();
                nominal.Text = ds.Rows[dgvPromo.SelectedIndex][2].ToString();

                id.IsReadOnly = true;
            }
        }

        private void Resets_Click(object sender, RoutedEventArgs e)
        {
            reset();
            if (caricari == 1)
            {
                loadData();
                caricari = 0;
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            if (rnama.IsChecked == false && rkode.IsChecked == false && rpotongan.IsChecked == false)
                MessageBox.Show("Mohon Isi Kategori Filter Terlebih Dahulu!");
            else if (rpotongan.IsChecked == true && keyword.Text.Equals("")) MessageBox.Show("Mohon Isi Keyword Terlebih Dahulu!");
            else
            {
                try
                {
                    string where = "";
                    if (rnama.IsChecked == true) where = " upper(nama_promo) like '%" + keyword.Text.ToUpper() + "%'";
                    else if (rkode.IsChecked == true) where = " upper(kode_promo) like '%" + keyword.Text.ToUpper() + "%'";
                    else if (rpotongan.IsChecked == true)
                    {
                        if (potongan.SelectedIndex == 0) where = " besar_potongan >= " + keyword.Text;
                        else if (potongan.SelectedIndex == 1) where = " besar_potongan <= " + keyword.Text;
                    }

                    ds = new DataTable();
                    da = new OracleDataAdapter("select kode_promo as \"Kode Promo\", nama_promo as \"Nama Promo\", besar_potongan as \"Besar Potongan\" " +
                        "from promo where " + where +
                        " order by 1",conn);

                    dgvPromo.Columns.Clear();
                    da.Fill(ds);
                    DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
                    Binding bind = new Binding("Besar Potongan") { StringFormat = "Rp. {0:N0}" };
                    newC.Binding = bind;
                    dgvPromo.ItemsSource = ds.DefaultView;
                    dgvPromo.Columns.Insert(2, newC);
                    dgvPromo.Columns.RemoveAt(3);
                    conn.Close();
                    caricari = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            reset();
            caricari = 0;

            listx = new List<string>();
            listx.Add("Greater");
            listx.Add("Lesser");

            potongan.ItemsSource = null;
            potongan.ItemsSource = listx;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Promo!");
            else if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Promo!");
            else if (nominal.Text.Equals(""))  MessageBox.Show("Mohon Isi Field Potongan!");
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper())) ada = true;
                }
                if (ada) MessageBox.Show("Nama Promo Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("insert into promo values (:id,initcap(:nama),:potongan)", conn);
                        cmd.Parameters.Add(":id", id.Text);
                        cmd.Parameters.Add(":nama", nama.Text);
                        cmd.Parameters.Add(":potongan", Convert.ToInt32(nominal.Text));

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Promo Baru Berhasil Ditambahkan!");
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Promo!");
            else if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Promo!");
            else if (nominal.Text.Equals("")) MessageBox.Show("Mohon Isi Field Potongan!");
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper()) && !row[0].Equals(id.Text)) ada = true;
                }
                if (ada) MessageBox.Show("Nama Promo Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("update promo set nama_promo = :nama, besar_potongan = :besar where kode_promo = :id", conn);
                        cmd.Parameters.Add(":nama", nama.Text);
                        cmd.Parameters.Add(":besar", nominal.Text);
                        cmd.Parameters.Add(":id", id.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Promo Berhasil diUpdate!");
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPromo.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Promo Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from promo where kode_promo = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Promo Berhasil!");
            }
        }
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

        private void Nominal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = nominal.SelectionStart;
            string kata2 = katabaru(nominal.Text);
            nominal.Text = kata2;
            nominal.SelectionStart = temp;
        }

        private void Rkode_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rnama_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rpotongan_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = true;
            potongan.SelectedIndex = 0;
            keyword.Text = "";
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rpotongan.IsChecked == true)
            {
                int temp = keyword.SelectionStart;
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = temp;
            }
        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mc = new MasterCustomer();
            this.Close();
            mc.Show();
        }

        private void Btn_supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier ms = new MasterSupplier();
            this.Close();
            ms.Show();
        }

        private void Btn_karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan mk = new MasterKaryawan();
            this.Close();
            mk.Show();
        }

        private void Btn_alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik mam = new MasterAlatMusik();
            this.Close();
            mam.Show();
        }

        private void Btn_aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris ma = new MasterAksesoris();
            this.Close();
            ma.Show();
        }

        private void Btn_produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen mps = new MasterProdusen();
            this.Close();
            mps.Show();
        }

        private void Btn_jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis mj = new MasterJenis();
            this.Close();
            mj.Show();
        }

        private void Btn_member_Click(object sender, RoutedEventArgs e)
        {
            MasterMember mm = new MasterMember();
            this.Close();
            mm.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Menu_Master mm = new Menu_Master();
            mm.Show();
        }

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master();
            this.Close();
            mma.Show();
        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inserts == 1)
            {
                id.Text = id.Text.ToUpper();
                id.SelectionStart = id.Text.Length;
            }
        }
    }
}
