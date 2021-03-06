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
    /// Interaction logic for MasterCustomer.xaml
    /// </summary>
    public partial class MasterCustomer : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari;
        public MasterCustomer()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select id_Customer as \"ID\", nama_Customer as \"Nama Customer\", " +
                "(case jk_Customer" +
                "   when 'M' then 'Laki-Laki'" +
                "   when 'F' then 'Perempuan'" +
                "end) as \"Jenis Kelamin\", " +
                "NoTelp_customer as \"No Telepon\", alamat_customer as \"Alamat\" " +
                "from customer order by 1",conn);
            da.Fill(ds);
            dgvCustomer.ItemsSource = ds.DefaultView;
            conn.Close();
            kolom();
        }

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            notelp.Text = "";
            alamat.Text = "";
            keyword.Text = "";

            rlaki.IsChecked = false;
            rperempuan.IsChecked = false;

            id.IsReadOnly = true;

            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            dgvCustomer.SelectedIndex = -1;

            keyword.Visibility = Visibility.Visible;
            canvas.Visibility = Visibility.Hidden;
            rL.IsChecked = false;
            rP.IsChecked = false;
            rnama.IsChecked = false;
            rjk.IsChecked = false;

            getId();
        }

        private void getId()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "autogenCustomer"
            };

            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "id_customer",
                OracleDbType = OracleDbType.Varchar2,
                Size = 6
            });

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                id.Text = cmd.Parameters["id_customer"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            conn.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            reset();
            caricari = 0;
        }

        private void DgvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCustomer.SelectedIndex != -1)
            {
                id.Text = ds.Rows[dgvCustomer.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvCustomer.SelectedIndex][1].ToString();
                notelp.Text = ds.Rows[dgvCustomer.SelectedIndex][3].ToString();
                alamat.Text = ds.Rows[dgvCustomer.SelectedIndex][4].ToString();

                if (ds.Rows[dgvCustomer.SelectedIndex][2].ToString().Equals("Laki-Laki")) rlaki.IsChecked = true;
                else rperempuan.IsChecked = true;

                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            if (rnama.IsChecked == false && rjk.IsChecked == false)
                MessageBox.Show("Mohon Isi Kategori Filter Terlebih Dahulu!");
            else if (rjk.IsChecked == true && rL.IsChecked == false && rP.IsChecked == false)
                MessageBox.Show("Mohon Pilih Jenis Kelamin Terlebih Dahulu!");
            else
            {
                try
                {
                    string where = "";
                    if (rnama.IsChecked == true) where = " upper(nama_customer) like '%" + keyword.Text.ToUpper() + "%'";
                    else if (rjk.IsChecked == true)
                    {
                        if (rL.IsChecked == true) where = " JK_Customer = 'M'";
                        else if (rP.IsChecked == true) where = " JK_Customer = 'F'";
                    }

                    ds = new DataTable();
                    da = new OracleDataAdapter("select id_Customer as \"ID\", nama_Customer as \"Nama Customer\", " +
                        "(case jk_Customer" +
                        "   when 'M' then 'Laki-Laki'" +
                        "   when 'F' then 'Perempuan'" +
                        "end) as \"Jenis Kelamin\", " +
                        "NoTelp_customer as \"No Telepon\", alamat_customer as \"Alamat\" " +
                        "from customer where " + where +
                        " order by 1",conn);
                    da.Fill(ds);
                    dgvCustomer.ItemsSource = ds.DefaultView;
                    conn.Close();
                    kolom();
                    caricari = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Resets_Click(object sender, RoutedEventArgs e)
        {
            reset();
            if (caricari==1)
            {
                loadData();
                caricari = 0;
            }
        }

        private void Rnama_Checked(object sender, RoutedEventArgs e)
        {
            canvas.Visibility = Visibility.Hidden;
            rL.IsChecked = false;
            rP.IsChecked = false;
            keyword.Text = "";
            keyword.Visibility = Visibility.Visible;
            label_nama.Visibility = Visibility.Visible;
        }

        private void Rjk_Checked(object sender, RoutedEventArgs e)
        {
            canvas.Visibility = Visibility.Visible;
            keyword.Visibility = Visibility.Hidden;
            label_nama.Visibility = Visibility.Hidden;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Customer!");
            else if ((rlaki.IsChecked == false && rperempuan.IsChecked == false)) MessageBox.Show("Mohon Isi Jenis Kelamin Customer!");
            else if (notelp.Text.Length!=12 && !notelp.Text.Equals("")) MessageBox.Show("Nomor Telepon Customer harus 12 digit!");
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper())) ada = true;
                }
                if (ada) MessageBox.Show("Nama Customer Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        string jk = "M";
                        if (rperempuan.IsChecked == true) jk = "F";
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("insert into customer values (:id,initcap(:nama),:jk,:alamat,:no)", conn);
                        cmd.Parameters.Add(":id", id.Text);
                        cmd.Parameters.Add(":nama", nama.Text);
                        cmd.Parameters.Add(":jk", jk);
                        cmd.Parameters.Add(":alamat", alamat.Text);
                        cmd.Parameters.Add(":no", notelp.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Customer Baru Berhasil Ditambahkan!");
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
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Customer!");
            else if ((rlaki.IsChecked == false && rperempuan.IsChecked == false)) MessageBox.Show("Mohon Isi Jenis Kelamin Customer!");
            else if (notelp.Text.Length != 12 && !notelp.Text.Equals("")) MessageBox.Show("Nomor Telepon Customer harus 12 digit!");
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper()) && !row[0].Equals(id.Text)) ada = true;
                }
                if (ada) MessageBox.Show("Nama Customer Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        string jk = "M";
                        if (rperempuan.IsChecked == true) jk = "F";
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("update customer set nama_customer = initcap(:nama), jk_customer = :jk, alamat_customer = :alamat, " +
                            "notelp_customer = :no where id_customer = :id", conn);
                        cmd.Parameters.Add(":nama", nama.Text);
                        cmd.Parameters.Add(":jk", jk);
                        cmd.Parameters.Add(":alamat", alamat.Text);
                        cmd.Parameters.Add(":no", notelp.Text);
                        cmd.Parameters.Add(":id", id.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Data Customer Berhasil diUpdate!");
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message.ToString());
                    }
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
        private void Notelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = notelp.SelectionStart;
            string kata2 = katabaru(notelp.Text);
            notelp.Text = kata2;
            notelp.SelectionStart = temp;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCustomer.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Customer Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from customer where ID_Customer = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Customer Berhasil!");
            }
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

        private void Btn_aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris ma = new MasterAksesoris();
            this.Close();
            ma.Show();
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
            dgvCustomer.Columns[0].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[2].Width = new DataGridLength(0.7, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[3].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[4].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        }
        private void DgvCustomer_Loaded(object sender, RoutedEventArgs e) { kolom(); }
    }
}
