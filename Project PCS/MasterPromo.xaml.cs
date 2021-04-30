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
            cmd.CommandText = "select kode_promo as \"Kode Promo\", nama_promo as \"Nama Promo\", besar_potongan as \"Besar Potongan\" " +
                "from promo order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dgvPromo.ItemsSource = ds.DefaultView;
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
                    OracleCommand cmd = new OracleCommand();
                    da = new OracleDataAdapter();

                    cmd.Connection = conn;
                    cmd.CommandText = "select kode_promo as \"Kode Promo\", nama_promo as \"Nama Promo\", besar_potongan as \"Besar Potongan\" " +
                        "from promo " +
                        "where " + where +
                        " order by 1";
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    dgvPromo.ItemsSource = ds.DefaultView;
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Kode Promo!");
            }
            else if (nama.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Nama Promo!");
            }
            else if (nominal.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Potongan!");
            }
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

        private void Nominal_TextChanged(object sender, TextChangedEventArgs e)
        {
            string kata2 = katabaru(nominal.Text);
            nominal.Text = kata2;
            nominal.SelectionStart = nominal.Text.Length;
        }

        private void Rkode_Checked(object sender, RoutedEventArgs e)
        {
            rpotongan.IsChecked = false;
            rnama.IsChecked = false;
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rnama_Checked(object sender, RoutedEventArgs e)
        {
            rpotongan.IsChecked = false;
            rkode.IsChecked = false;
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rpotongan_Checked(object sender, RoutedEventArgs e)
        {
            rkode.IsChecked = false;
            rnama.IsChecked = false;
            potongan.IsEnabled = true;
            potongan.SelectedIndex = 0;
            keyword.Text = "";
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rpotongan.IsChecked == true)
            {
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = keyword.Text.Length;
            }
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
