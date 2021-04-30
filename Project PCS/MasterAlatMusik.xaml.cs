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
        DataTable ds, dtjenis, dtprodusen;
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                Images.Source = new BitmapImage(new Uri(newPath + sources));

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
        private void getId(string paramNama)
        {
            //OracleCommand cmd = new OracleCommand()
            //{
            //    CommandType = CommandType.StoredProcedure,
            //    Connection = conn,
            //    CommandText = "autogenMusik"
            //};

            //cmd.Parameters.Add(new OracleParameter()
            //{
            //    Direction = ParameterDirection.Input,
            //    ParameterName = "nama",
            //    OracleDbType = OracleDbType.Varchar2,
            //    Size = 50,
            //    Value = "a s"
            //});

            //cmd.Parameters.Add(new OracleParameter()
            //{
            //    Direction = ParameterDirection.ReturnValue,
            //    ParameterName = "id_musik",
            //    OracleDbType = OracleDbType.Varchar2,
            //    Size = 999
            //});

            //try
            //{
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    id2.Text = cmd.Parameters["id_musik"].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    MessageBox.Show(ex.Message.ToString());
            //}
            //conn.Close();
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

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rstok.IsChecked == true || rharga.IsChecked == true)
            {
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = keyword.Text.Length;
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
                try
                {
                    string[] file = source.Text.Split('\\');
                    string filename = file[file.Length - 1];
                    File.Copy(source.Text, System.IO.Path.Combine(newPath, filename), true);

                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("insert into alat_musik values (:id,:nama,:idjenis,:idprodusen,:stok,:harga,:source)", conn);
                    cmd.Parameters.Add(":id", "");
                    cmd.Parameters.Add(":nama", nama2.Text);
                    cmd.Parameters.Add(":idjenis", jenis2.SelectedValue.ToString());
                    cmd.Parameters.Add(":idprodusen", produsen2.SelectedValue.ToString());
                    cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                    cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                    cmd.Parameters.Add(":source", filename);

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (nama2.Text.Equals("") || stok2.Text.Equals("") || harga2.Text.Equals("") || jenis2.SelectedIndex == -1 || produsen2.SelectedIndex == -1)
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
                        System.IO.File.Copy(source.Text, System.IO.Path.Combine(newPath, filename), true);

                        conn.Close();
                        cmd = new OracleCommand("Update alat_musik set nama_alat_musik = :nama, id_jenis = :idjenis, " +
                            "id_produsen = :idprodusen, stok = :stok, harga = :harga, nama_file = :source" +
                            " where id_alat_musik = :id", conn);
                        cmd.Parameters.Add(":nama", nama2.Text);
                        cmd.Parameters.Add(":idjenis", jenis2.SelectedValue.ToString());
                        cmd.Parameters.Add(":idprodusen", produsen2.SelectedValue.ToString());
                        cmd.Parameters.Add(":stok", Convert.ToInt32(stok2.Text));
                        cmd.Parameters.Add(":harga", Convert.ToInt32(harga2.Text));
                        cmd.Parameters.Add(":source", filename);
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
                    OracleCommand cmd = new OracleCommand();
                    da = new OracleDataAdapter();
                    
                    cmd.Connection = conn;
                    cmd.CommandText = "select a.id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                        "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                        "from alat_musik a, jenis_alat_musik j, produsen p " +
                        "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen and" + where +
                        " order by 1";
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    dgvMusik.ItemsSource = ds.DefaultView;
                    conn.Close();
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

            string[] path2;
            string path;
            path = System.AppDomain.CurrentDomain.BaseDirectory;
            path2 = path.Split('\\');
            newPath = "";
            for (int i = 0; i < path2.Length - 3; i++)
            {
                newPath += path2[i] + "\\";
            }
            newPath += "ImageList\\";

            da = new OracleDataAdapter("select nama_jenis as \"nama\", id_jenis as id from jenis_alat_musik order by 2", conn);
            dtjenis = new DataTable();
            da.Fill(dtjenis);

            DataRow newRow = dtjenis.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            dtjenis.Rows.InsertAt(newRow,0);

            jenis.ItemsSource = dtjenis.DefaultView;
            jenis.DisplayMemberPath = dtjenis.Columns["nama"].ToString();
            jenis.SelectedValuePath = "ID";
            jenis.SelectedIndex = 0;

            jenis2.ItemsSource = dtjenis.DefaultView;
            jenis2.DisplayMemberPath = dtjenis.Columns["nama"].ToString();
            jenis2.SelectedValuePath = "ID";

            conn.Open();
            OracleCommand cmd = new OracleCommand("select nama_jenis as \"nama\", id_jenis as id from jenis_alat_musik order by 2", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            arrJenis = new List<jenisX>();
            int ctr = 1;
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
            da.Fill(dtprodusen);

            DataRow newRow2 = dtprodusen.NewRow();
            newRow2[0] = "All";
            newRow2[1] = "0";
            dtprodusen.Rows.InsertAt(newRow2, 0);

            produsen.ItemsSource = dtprodusen.DefaultView;
            produsen.DisplayMemberPath = dtprodusen.Columns["nama"].ToString();
            produsen.SelectedValuePath = "ID";
            produsen.SelectedIndex = 0;
            
            produsen2.ItemsSource = dtprodusen.DefaultView;
            produsen2.DisplayMemberPath = dtprodusen.Columns["nama"].ToString();
            produsen2.SelectedValuePath = "ID";

            conn.Open();
            cmd = new OracleCommand("select nama_produsen as \"nama\", id_produsen as id from produsen order by 2", conn);
            reader = cmd.ExecuteReader();
            arrProdusen = new List<produsenX>();
            ctr = 1;
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
    }
}
