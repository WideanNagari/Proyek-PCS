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
    /// Interaction logic for MasterAlatMusik.xaml
    /// </summary>
    public partial class MasterAlatMusik : Window
    {
        OracleConnection conn;
        DataTable ds, dtjenis, dtjenis2, dtprodusen, dtprodusen2;
        OracleDataAdapter da;
        List<string> listx;
        int inserts;
        string newPath;
        int caricari;
        private class produsenX
        {
            public int id { get; set; }
            public string nama { get; set; }
        }
        private class jenisX
        {
            public int id { get; set; }
            public string nama { get; set; }
        }

        List<produsenX> arrProdusen;
        List<jenisX> arrJenis;

        public MasterAlatMusik()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select a.id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                "from alat_musik a, jenis_alat_musik j, produsen p " +
                "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen " +
                "order by 1",conn);
            da.Fill(ds);
            dgvMusik.ItemsSource = ds.DefaultView;
            kolom();
        }

        private void Rnama_Checked(object sender, RoutedEventArgs e)
        {
            stok.SelectedIndex = -1;
            stok.IsEnabled = false;
            harga.SelectedIndex = -1;
            harga.IsEnabled = false;
            rstok.IsChecked = false;
            rharga.IsChecked = false;
            keyword.Text = "";
        }

        private void Rstok_Checked(object sender, RoutedEventArgs e)
        {
            stok.IsEnabled = true;
            stok.SelectedIndex = 0;
            harga.SelectedIndex = -1;
            harga.IsEnabled = false;
            rnama.IsChecked = false;
            rharga.IsChecked = false;
            keyword.Text = "";
        }

        private void Rharga_Checked(object sender, RoutedEventArgs e)
        {
            harga.IsEnabled = true;
            harga.SelectedIndex = 0;
            stok.SelectedIndex = -1;
            stok.IsEnabled = false;
            rstok.IsChecked = false;
            rnama.IsChecked = false;
            keyword.Text = "";
        }

        private void DgvMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvMusik.SelectedIndex!=-1)
            {
                inserts = 0;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select Nama_File from alat_musik where id_alat_musik = :id";
                cmd.Parameters.Add(":id", ds.Rows[dgvMusik.SelectedIndex][0].ToString());
                conn.Close();
                conn.Open();
                string sources = cmd.ExecuteScalar().ToString();
                conn.Close();

                if (!sources.Equals("")) Images.Source = new BitmapImage(new Uri(newPath + sources));
                else Images.Source = null;

                id2.IsReadOnly = true;
                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id2.Text = ds.Rows[dgvMusik.SelectedIndex][0].ToString();
                nama2.Text = ds.Rows[dgvMusik.SelectedIndex][1].ToString();
                int a = -1, b = -1;
                foreach (jenisX j in arrJenis)
                {
                    if (j.nama.Equals(ds.Rows[dgvMusik.SelectedIndex][2].ToString())) a = j.id;
                }
                foreach (produsenX p in arrProdusen)
                {
                    if (p.nama.Equals(ds.Rows[dgvMusik.SelectedIndex][3].ToString())) b = p.id;
                }
                jenis2.SelectedIndex = a;
                produsen2.SelectedIndex = b;
                stok2.Text = ds.Rows[dgvMusik.SelectedIndex][4].ToString();
                harga2.Text = ds.Rows[dgvMusik.SelectedIndex][5].ToString();

                source.Text = "";
            }
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

        private void Nama2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nama2.Text.Equals(" "))
            {
                nama2.Text = "";
            }
            if (inserts==1 && !nama2.Text.Equals(""))
            {
                if (nama2.Text[nama2.Text.Length-1]==' '&& nama2.Text[nama2.Text.Length - 2] == ' ')
                {
                    string kata = "";
                    for (int i = 0; i < nama2.Text.Length-1; i++)
                    {
                        kata += nama2.Text[i];
                    }
                    nama2.Text = kata;
                    nama2.SelectionStart = nama2.Text.Length;
                }
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

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || jenis2.SelectedIndex == -1 || produsen2.SelectedIndex == -1)
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
                if (ada) MessageBox.Show("Nama Alat Musik Sudah Ada! Masukkan Nama Lain.");
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
                        cmd = new OracleCommand("insert into alat_musik values (:id,:nama,:idjenis,:idprodusen,:stok,:harga,:source)", conn);
                        cmd.Parameters.Add(":id", "");
                        cmd.Parameters.Add(":nama", nama2.Text);
                        cmd.Parameters.Add(":idjenis", jenis2.SelectedValue.ToString());
                        cmd.Parameters.Add(":idprodusen", produsen2.SelectedValue.ToString());
                        cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                        cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                        cmd.Parameters.Add(":source", "");

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (!source.Text.Equals(""))
                        {
                            cmd = new OracleCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "select id_alat_musik from alat_musik where nama_alat_musik = initcap('" + nama2.Text + "')";
                            conn.Close();
                            conn.Open();
                            string newName = cmd.ExecuteScalar().ToString();
                            conn.Close();
                            File.Copy(source.Text, System.IO.Path.Combine(newPath, newName + ext2), true);

                            cmd = new OracleCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "update alat_musik set nama_file = '" + newName + ext2 + "' where id_alat_musik = '" + newName + "'";
                            conn.Close();
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        loadData();
                        reset();
                        MessageBox.Show("Alat Musik Baru Berhasil Ditambahkan!");
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
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || jenis2.SelectedIndex == -1 || produsen2.SelectedIndex == -1)
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
                if (ada) MessageBox.Show("Nama Alat Musik Sudah Ada! Masukkan Nama Lain.");
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
                            cmd = new OracleCommand("Update alat_musik set nama_alat_musik = :nama, id_jenis = :idjenis, " +
                                "id_produsen = :idprodusen, stok = :stok, harga = :harga, nama_file = :source" +
                                " where id_alat_musik = :id", conn);
                            cmd.Parameters.Add(":nama", nama2.Text);
                            cmd.Parameters.Add(":idjenis", jenis2.SelectedValue.ToString());
                            cmd.Parameters.Add(":idprodusen", produsen2.SelectedValue.ToString());
                            cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                            cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                            cmd.Parameters.Add(":source", newName + ext2);
                            cmd.Parameters.Add("id", id2.Text);
                        }
                        else
                        {
                            conn.Close();
                            cmd = new OracleCommand("Update alat_musik set nama_alat_musik = :nama, id_jenis = :idjenis, " +
                                "id_produsen = :idprodusen, stok = :stok, harga = :harga" +
                                " where id_alat_musik = :id", conn);
                            cmd.Parameters.Add(":nama", nama2.Text);
                            cmd.Parameters.Add(":idjenis", jenis2.SelectedValue.ToString());
                            cmd.Parameters.Add(":idprodusen", produsen2.SelectedValue.ToString());
                            cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                            cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
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
            if (dgvMusik.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Alat Musik Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from alat_musik where id_alat_musik = '" + id2.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Alat Musik Berhasil!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";
            if (open.ShowDialog() == true)
            {
                source.Text = open.FileName;
                Images.Source = new BitmapImage(new Uri(source.Text));
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            if (rnama.IsChecked == false && rstok.IsChecked == false && rharga.IsChecked == false)
            {
                MessageBox.Show("Mohon Isi Kategori Filter Terlebih Dahulu!");
            }
            else if ((rstok.IsChecked == true || rharga.IsChecked == true) && keyword.Text.Equals("")) MessageBox.Show("Mohon Isi Keyword Terlebih Dahulu!");
            else
            {
                string where = "";
                if (rnama.IsChecked == true) where = " upper(a.nama_alat_musik) like '%" + keyword.Text.ToUpper()+"%'";
                else if (rstok.IsChecked == true)
                {
                    if (stok.SelectedIndex == 0) where = " a.stok >= " + keyword.Text;
                    else if (stok.SelectedIndex == 1) where = " a.stok <= "+keyword.Text;
                }
                else if (rharga.IsChecked == true)
                {
                    if (harga.SelectedIndex == 0) where = " a.harga >= " + keyword.Text;
                    else if (harga.SelectedIndex == 1) where = " a.harga <= " + keyword.Text;
                }

                if (produsen.SelectedIndex>0) where += " and a.id_produsen = '"+produsen.SelectedValue+"'";
                if (jenis.SelectedIndex>0) where += " and a.id_jenis = '"+jenis.SelectedValue+"'";

                try
                {
                    ds = new DataTable();
                    da = new OracleDataAdapter("select a.id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                        "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                        "from alat_musik a, jenis_alat_musik j, produsen p " +
                        "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen and " + where +
                        "order by 1",conn);
                    da.Fill(ds);
                    dgvMusik.ItemsSource = ds.DefaultView;
                    kolom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                caricari = 1;
            }
        }

        private void reset()
        {
            dgvMusik.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            stok.IsEnabled = false;
            harga.IsEnabled = false;

            rnama.IsChecked = false;
            rstok.IsChecked = false;
            rharga.IsChecked = false;

            jenis.SelectedIndex = -1;
            jenis2.SelectedIndex = -1;
            produsen.SelectedIndex = -1;
            produsen2.SelectedIndex = -1;

            keyword.Text = "";
            id2.Text = "";
            nama2.Text = "";
            stok2.Text = "";
            harga2.Text = "";
            source.Text = "";

            id2.IsReadOnly = true;
            source.IsReadOnly = true;
            Images.Source = null;

            inserts = 1;
            jenis.SelectedIndex = 0;
            produsen.SelectedIndex = 0;
            stok.SelectedIndex = -1;
            harga.SelectedIndex = -1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listx = new List<string>();
            listx.Add("Greater");
            listx.Add("Lesser");
            loadData();
            reset();
            
            newPath = System.AppDomain.CurrentDomain.BaseDirectory+"ImageList\\";

            da = new OracleDataAdapter("select nama_jenis as \"nama\", id_jenis as id from jenis_alat_musik order by 2", conn);
            dtjenis = new DataTable();
            dtjenis2 = new DataTable();
            da.Fill(dtjenis);
            da.Fill(dtjenis2);

            DataRow newRow = dtjenis.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtjenis.Rows.InsertAt(newRow,0);

            jenis.ItemsSource = dtjenis.DefaultView;
            jenis.DisplayMemberPath = dtjenis.Columns["nama"].ToString();
            jenis.SelectedValuePath = "ID";
            jenis.SelectedIndex = 0;

            jenis2.ItemsSource = dtjenis2.DefaultView;
            jenis2.DisplayMemberPath = dtjenis2.Columns["nama"].ToString();
            jenis2.SelectedValuePath = "ID";

            conn.Open();
            OracleCommand cmd = new OracleCommand("select nama_jenis as \"nama\", id_jenis as id from jenis_alat_musik order by 2", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            arrJenis = new List<jenisX>();
            int ctr = 0;
            while (reader.Read())
            {
                arrJenis.Add(new jenisX()
                {
                    id = ctr,
                    nama = reader.GetString(0)
                });
                ctr++;
            }
            conn.Close();

            da = new OracleDataAdapter("select nama_produsen as \"nama\", id_produsen as id from produsen order by 2", conn);
            dtprodusen = new DataTable();
            dtprodusen2 = new DataTable();
            da.Fill(dtprodusen);
            da.Fill(dtprodusen2);

            DataRow newRow2 = dtprodusen.NewRow();
            newRow2[0] = "All";
            newRow2[1] = "0";
            dtprodusen.Rows.InsertAt(newRow2, 0);

            produsen.ItemsSource = dtprodusen.DefaultView;
            produsen.DisplayMemberPath = dtprodusen.Columns["nama"].ToString();
            produsen.SelectedValuePath = "ID";
            produsen.SelectedIndex = 0;
            
            produsen2.ItemsSource = dtprodusen2.DefaultView;
            produsen2.DisplayMemberPath = dtprodusen2.Columns["nama"].ToString();
            produsen2.SelectedValuePath = "ID";

            conn.Open();
            cmd = new OracleCommand("select nama_produsen as \"nama\", id_produsen as id from produsen order by 2", conn);
            reader = cmd.ExecuteReader();
            arrProdusen = new List<produsenX>();
            ctr = 0;
            while (reader.Read())
            {
                arrProdusen.Add(new produsenX()
                {
                    id = ctr,
                    nama = reader.GetString(0)
                });
                ctr++;
            }
            conn.Close();

            stok.ItemsSource = null;
            stok.ItemsSource = listx;
            harga.ItemsSource = null;
            harga.ItemsSource = listx;

            caricari = 0;
        }
        private void kolom()
        {
            dgvMusik.Columns[5].ClipboardContentBinding.StringFormat = "Rp. {0:N0}";
            dgvMusik.Columns[0].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvMusik.Columns[1].Width = new DataGridLength(2.5, DataGridLengthUnitType.Star);
            dgvMusik.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvMusik.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvMusik.Columns[4].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvMusik.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DgvMusik_Loaded(object sender, RoutedEventArgs e) { kolom(); }

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

        private void Btn_aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris ma = new MasterAksesoris();
            this.Close();
            ma.Show();
        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mam = new MasterCustomer();
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
    }
}
