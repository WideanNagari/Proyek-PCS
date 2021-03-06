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
using Microsoft.Win32;
using System.IO;

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for MasterAksesoris.xaml
    /// </summary>
    public partial class MasterAksesoris : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        List<string> listx;
        string newPath;
        int caricari;

        public MasterAksesoris()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                "from aksesoris order by 1", conn);
            da.Fill(ds);
            dgvAksesoris.ItemsSource = ds.DefaultView;
            kolom();
        }
        private void reset()
        {
            dgvAksesoris.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            stok.IsEnabled = false;
            harga.IsEnabled = false;

            rnama.IsChecked = false;
            rketerangan.IsChecked = false;
            rstok.IsChecked = false;
            rharga.IsChecked = false;

            keyword.Text = "";
            id2.Text = "";
            nama2.Text = "";
            stok2.Text = "";
            harga2.Text = "";
            keterangan2.Text = "";
            source.Text = "";

            id2.IsReadOnly = true;
            source.IsReadOnly = true;
            Images.Source = null;
            
            stok.SelectedIndex = -1;
            harga.SelectedIndex = -1;
        }

        private string katabaru(string kata)
        {
            string kata2 = "";
            if (kata.Length > 0)
            {
                for (int i = 0; i < kata.Length; i++)
                {
                    if(Char.IsDigit(kata[i])) kata2 += kata[i];
                }
            }
            return kata2;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listx = new List<string>();
            listx.Add("Greater");
            listx.Add("Lesser");
            loadData();
            reset();

            newPath = System.AppDomain.CurrentDomain.BaseDirectory + "ImageList\\";

            stok.ItemsSource = null;
            stok.ItemsSource = listx;
            harga.ItemsSource = null;
            harga.ItemsSource = listx;

            caricari = 0;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            reset();
            if (caricari == 1)
            {
                loadData();
                caricari = 0;
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";
            if (open.ShowDialog() == true)
            {
                source.Text = open.FileName;
                Images.Source = new BitmapImage(new Uri(source.Text));
            }
        }

        private void Stok2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = stok2.SelectionStart;
            string kata2 = katabaru(stok2.Text);
            stok2.Text = kata2;
            stok2.SelectionStart = temp;
        }

        private void Harga2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = harga2.SelectionStart;
            string kata2 = katabaru(harga2.Text);
            harga2.Text = kata2;
            harga2.SelectionStart = temp;
        }

        private void Rnama_Checked(object sender, RoutedEventArgs e)
        {
            rstok.IsChecked = false;
            rharga.IsChecked = false;
            rketerangan.IsChecked = false;

            harga.IsEnabled = false;
            harga.SelectedIndex = -1;
            stok.IsEnabled = false;
            stok.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rketerangan_Checked(object sender, RoutedEventArgs e)
        {
            rstok.IsChecked = false;
            rnama.IsChecked = false;
            rharga.IsChecked = false;

            harga.IsEnabled = false;
            harga.SelectedIndex = -1;
            stok.IsEnabled = false;
            stok.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rstok_Checked(object sender, RoutedEventArgs e)
        {
            rharga.IsChecked = false;
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;

            harga.IsEnabled = false;
            harga.SelectedIndex = -1;
            stok.IsEnabled = true;
            stok.SelectedIndex = 0;
            keyword.Text = "";
        }

        private void Rharga_Checked(object sender, RoutedEventArgs e)
        {
            rstok.IsChecked = false;
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;

            harga.IsEnabled = true;
            harga.SelectedIndex = 0;
            stok.IsEnabled = false;
            stok.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void DgvAksesoris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvAksesoris.SelectedIndex != -1)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select Nama_File from aksesoris where id_aksesoris = :id";
                cmd.Parameters.Add(":id", ds.Rows[dgvAksesoris.SelectedIndex][0].ToString());
                conn.Close();
                conn.Open();
                string sources = cmd.ExecuteScalar().ToString();
                conn.Close();

                if (!sources.Equals("")) Images.Source = new BitmapImage(new Uri(newPath + sources));
                else Images.Source = null;

                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id2.Text = ds.Rows[dgvAksesoris.SelectedIndex][0].ToString();
                nama2.Text = ds.Rows[dgvAksesoris.SelectedIndex][1].ToString();
                stok2.Text = ds.Rows[dgvAksesoris.SelectedIndex][2].ToString();
                harga2.Text = ds.Rows[dgvAksesoris.SelectedIndex][3].ToString();
                keterangan2.Text = ds.Rows[dgvAksesoris.SelectedIndex][4].ToString();

                source.Text = "";
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || keterangan2.Equals(""))
            {
                MessageBox.Show("Mohon Isi Semua Field!");
            }
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama2.Text.ToUpper())) ada = true;
                }
                if (ada) MessageBox.Show("Nama Aksesoris Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        string[] file, ext;
                        string filename = "", ext2 = "";
                        if (!source.Text.Equals(""))
                        {
                            file = source.Text.Split('\\');
                            filename = file[file.Length - 1];
                            ext = filename.Split('.');
                            ext2 = "." + ext[1];
                        }

                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("insert into aksesoris values (:id,:nama,:stok,:harga,:keterangan,:source)", conn);
                        cmd.Parameters.Add(":id", "");
                        cmd.Parameters.Add(":nama", nama2.Text);
                        cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                        cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                        cmd.Parameters.Add(":keterangan", keterangan2.Text);
                        cmd.Parameters.Add(":source", "");

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (!source.Text.Equals(""))
                        {
                            cmd = new OracleCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "select id_aksesoris from aksesoris where nama_aksesoris = initcap('" + nama2.Text + "')";
                            conn.Close();
                            conn.Open();
                            string newName = cmd.ExecuteScalar().ToString();
                            conn.Close();
                            File.Copy(source.Text, System.IO.Path.Combine(newPath, newName + ext2), true);

                            cmd = new OracleCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "update aksesoris set nama_file = '" + newName + ext2 + "' where id_aksesoris = '" + newName + "'";
                            conn.Close();
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        loadData();
                        reset();
                        MessageBox.Show("Aksesoris Baru Berhasil Ditambahkan!");
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
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || keterangan2.Equals(""))
            {
                MessageBox.Show("Mohon Isi Semua Field!");
            }
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama2.Text.ToUpper()) && !row[0].Equals(id2.Text)) ada = true;
                }
                if (ada) MessageBox.Show("Nama Aksesoris Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        if (!source.Text.Equals(""))
                        {
                            string[] file = source.Text.Split('\\');
                            string filename = file[file.Length - 1];
                            string[] ext = filename.Split('.');
                            string ext2 = "." + ext[1];

                            string newName = id2.Text;
                            File.Copy(source.Text, System.IO.Path.Combine(newPath, newName + ext2), true);

                            conn.Close();
                            cmd = new OracleCommand("Update aksesoris set nama_aksesoris = :nama, " +
                                "stok = :stok, harga = :harga, keterangan = :keterangan, nama_file = :source" +
                                " where id_aksesoris = :id", conn);
                            cmd.Parameters.Add(":nama", nama2.Text);
                            cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                            cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                            cmd.Parameters.Add(":keterangan", keterangan2.Text);
                            cmd.Parameters.Add(":source", newName + ext2);
                            cmd.Parameters.Add("id", id2.Text);
                        }
                        else
                        {
                            conn.Close();
                            cmd = new OracleCommand("Update aksesoris set nama_aksesoris = :nama, " +
                                "stok = :stok, harga = :harga, keterangan = :keterangan" +
                                " where id_aksesoris = :id", conn);
                            cmd.Parameters.Add(":nama", nama2.Text);
                            cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                            cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                            cmd.Parameters.Add(":keterangan", keterangan2.Text);
                            cmd.Parameters.Add("id", id2.Text);
                        }

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Update Berhasil!");
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
            if (dgvAksesoris.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Aksesoris Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from aksesoris where id_aksesoris = '" + id2.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Aksesoris Berhasil!");
            }
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rstok.IsChecked == true || rharga.IsChecked == true)
            {
                int temp = keyword.SelectionStart;
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = temp;
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            if (rnama.IsChecked == false && rketerangan.IsChecked == false && rstok.IsChecked == false && rharga.IsChecked == false)
            {
                MessageBox.Show("Mohon Pilih Kategori Filter Terlebih Dahulu!");
            }
            else if ((rstok.IsChecked == true || rharga.IsChecked == true) && keyword.Text.Equals("")) MessageBox.Show("Mohon Isi Keyword Terlebih Dahulu!");
            else
            {
                string where = "";
                if (rnama.IsChecked == true) where = " upper(nama_aksesoris) like '%" + keyword.Text.ToUpper() + "%'";
                else if (rketerangan.IsChecked == true) where = " upper(keterangan) like '%" + keyword.Text.ToUpper() + "%'";
                else if (rstok.IsChecked == true)
                {
                    if (stok.SelectedIndex == 0) where = " stok >= " + keyword.Text;
                    else if (stok.SelectedIndex == 1) where = " stok <= " + keyword.Text;
                }
                else if (rharga.IsChecked == true)
                {
                    if (harga.SelectedIndex == 0) where = " harga >= " + keyword.Text;
                    else if (harga.SelectedIndex == 1) where = " harga <= " + keyword.Text;
                }
                
                try
                {
                    ds = new DataTable();
                    da = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                        "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                        "from aksesoris where " + where +
                        " order by 1",conn);
                    da.Fill(ds);
                    dgvAksesoris.ItemsSource = ds.DefaultView;
                    kolom();
                    caricari = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                caricari = 1;
            }

        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer ma = new MasterCustomer();
            this.Close();
            ma.Show();
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
            this.Close();
            mm.Show();

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Menu_Master mm = new Menu_Master();
            mm.Show();
        }

        private void Btn_promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo mpr = new MasterPromo();
            this.Close();
            mpr.Show();
        }

        private void Btn_jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis mj = new MasterJenis();
            this.Close();
            mj.Show();
        }

        private void Btn_produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen mpd = new MasterProdusen();
            this.Close();
            mpd.Show();
        }

        private void Btn_alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik mam = new MasterAlatMusik();
            this.Close();
            mam.Show();
        }

        private void Btn_karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan mk = new MasterKaryawan();
            this.Close();
            mk.Show();
        }

        private void Btn_supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier ms = new MasterSupplier();
            this.Close();
            ms.Show();
        }

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master();
            this.Close();
            mma.Show();
        }

        private void kolom()
        {
            dgvAksesoris.Columns[3].ClipboardContentBinding.StringFormat = "Rp. {0:N0}";
            dgvAksesoris.Columns[0].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[1].Width = new DataGridLength(2.5, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[2].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DgvAksesoris_Loaded(object sender, RoutedEventArgs e) { kolom(); }
    }
}
