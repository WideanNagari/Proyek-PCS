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
        int inserts;
        string newPath;
        int caricari;

        Menu w_utama;

        public MasterAksesoris(Menu wm)
        {
            InitializeComponent();
            conn = MainWindow.conn;
            w_utama = wm;
        }

        private void loadData()
        {
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();
            dgvAksesoris.Columns.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                "from aksesoris order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
            Binding bind = new Binding("Harga") { StringFormat = "Rp. {0:N0}" };
            newC.Binding = bind;
            dgvAksesoris.ItemsSource = ds.DefaultView;
            dgvAksesoris.Columns.Insert(3, newC);
            dgvAksesoris.Columns.RemoveAt(4);
            conn.Close();
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

            inserts = 1;
            stok.SelectedIndex = -1;
            harga.SelectedIndex = -1;
        }

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
            string kata2 = katabaru(stok2.Text);
            stok2.Text = kata2;
            stok2.SelectionStart = stok2.Text.Length;
        }

        private void Harga2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string kata2 = katabaru(harga2.Text);
            harga2.Text = kata2;
            harga2.SelectionStart = harga2.Text.Length;
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
                inserts = 0;
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || keterangan2.Equals(""))
            {
                MessageBox.Show("Mohon Isi Semua Field!");
            }
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
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = keyword.Text.Length;
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
                    OracleCommand cmd = new OracleCommand();
                    da = new OracleDataAdapter();
                    dgvAksesoris.Columns.Clear();
                    cmd.Connection = conn;
                    cmd.CommandText = "select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                        "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                        "from aksesoris where " + where +
                        " order by 1";
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
                    Binding bind = new Binding("Harga") { StringFormat = "Rp. {0:N0}" };
                    newC.Binding = bind;
                    dgvAksesoris.ItemsSource = ds.DefaultView;
                    dgvAksesoris.Columns.Insert(3, newC);
                    dgvAksesoris.Columns.RemoveAt(4);
                    conn.Close();
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
            MasterCustomer ma = new MasterCustomer(w_utama);
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
            MasterMember mm = new MasterMember(w_utama);
            this.Close();
            mm.Show();

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w_utama.Show();
        }

        private void Btn_promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo mpr = new MasterPromo(w_utama);
            this.Close();
            mpr.Show();
        }

        private void Btn_jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis mj = new MasterJenis(w_utama);
            this.Close();
            mj.Show();
        }

        private void Btn_produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen mpd = new MasterProdusen(w_utama);
            this.Close();
            mpd.Show();
        }

        private void Btn_alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik mam = new MasterAlatMusik(w_utama);
            this.Close();
            mam.Show();
        }

        private void Btn_karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan mk = new MasterKaryawan(w_utama);
            this.Close();
            mk.Show();
        }

        private void Btn_supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier ms = new MasterSupplier(w_utama);
            this.Close();
            ms.Show();
        }

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master(w_utama);
            this.Close();
            mma.Show();
        }
    }
}
