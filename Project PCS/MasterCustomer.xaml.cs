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
        List<string> listx;
        int inserts;
        int caricari;
        public MasterCustomer()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void loadData()
        {
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "select id_Customer as \"ID\", nama_Customer as \"Nama Customer\", " +
                "(case jk_Customer" +
                "   when 'M' then 'Laki-Laki'" +
                "   when 'F' then 'Perempuan'" +
                "end) as \"Jenis Kelamin\", " +
                "NoTelp_customer as \"No Telepon\", alamat_customer as \"Alamat\" " +
                "from customer order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dgvCustomer.ItemsSource = ds.DefaultView;
            conn.Close();
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
            rL.Visibility = Visibility.Hidden;
            rP.Visibility = Visibility.Hidden;
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

        private void DgvMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    OracleCommand cmd = new OracleCommand();
                    da = new OracleDataAdapter();

                    cmd.Connection = conn;
                    cmd.CommandText = "select id_Customer as \"ID\", nama_Customer as \"Nama Customer\", " +
                        "(case jk_Customer" +
                        "   when 'M' then 'Laki-Laki'" +
                        "   when 'F' then 'Perempuan'" +
                        "end) as \"Jenis Kelamin\", " +
                        "NoTelp_customer as \"No Telepon\", alamat_customer as \"Alamat\" " +
                        "from customer where " +where+
                        " order by 1";

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    dgvCustomer.ItemsSource = ds.DefaultView;
                    conn.Close();
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
            rL.Visibility = Visibility.Hidden;
            rP.Visibility = Visibility.Hidden;
            rL.IsChecked = false;
            rP.IsChecked = false;
            keyword.Text = "";
            keyword.Visibility = Visibility.Visible;
        }

        private void Rjk_Checked(object sender, RoutedEventArgs e)
        {
            rL.Visibility = Visibility.Visible;
            rP.Visibility = Visibility.Visible;
            keyword.Visibility = Visibility.Hidden;
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Customer!");
            else if ((rlaki.IsChecked == false && rperempuan.IsChecked == false)) MessageBox.Show("Mohon Isi Jenis Kelamin Customer!");
            else if (notelp.Text.Length != 12 && !notelp.Text.Equals("")) MessageBox.Show("Nomor Telepon Customer harus 12 digit!");
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
        private void Notelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            string kata2 = katabaru(notelp.Text);
            notelp.Text = kata2;
            notelp.SelectionStart = notelp.Text.Length;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCustomer.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Customer Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from d_jual where nota_jual in (select nota_jual from h_jual where ID_Customer = '" + id.Text + "')", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cmd = new OracleCommand("delete from h_jual where ID_Customer = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cmd = new OracleCommand("delete from customer where ID_Customer = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Customer Berhasil!");
            }
        }
    }
}
