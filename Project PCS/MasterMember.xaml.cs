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
    /// Interaction logic for MasterMember.xaml
    /// </summary>
    public partial class MasterMember : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari, inserts;
        List<string> listx;
        Menu w_utama;
        public MasterMember(Menu wm)
        {
            InitializeComponent();
            conn = MainWindow.conn;
            w_utama = wm;
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
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();
            dgvMember.Columns.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "select id_member as \"ID\", jenis_member as \"Jenis Member\", harga_member as \"Harga Member\", diskon_pembelian||'%' as \"Diskon\" " +
                "from member order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
            Binding bind = new Binding("Harga Member") { StringFormat = "Rp. {0:N0}" };
            newC.Binding = bind;
            dgvMember.ItemsSource = ds.DefaultView;
            dgvMember.Columns.Insert(2, newC);
            dgvMember.Columns.RemoveAt(3);
            conn.Close();
        }

        private void reset()
        {
            dgvMember.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;

            harga.IsEnabled = false;
            harga.SelectedIndex = -1;

            rjenis.IsChecked = false;
            rharga.IsChecked = false;
            rpotongan.IsChecked = false;

            keyword.Text = "";
            id.Text = "";
            jenis.Text = "";
            nominal.Text = "";
            diskon.Text = "";
            keyword.Text = "";

            id.IsReadOnly = false;
            inserts = 1;

            getId();
        }

        private void getId()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "autogenMember"
            };

            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "id_member",
                OracleDbType = OracleDbType.Varchar2,
                Size = 4
            });

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                id.Text = cmd.Parameters["id_member"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            conn.Close();
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
            loadData();
            reset();
            caricari = 0;

            listx = new List<string>();
            listx.Add("Greater");
            listx.Add("Lesser");

            potongan.ItemsSource = null;
            potongan.ItemsSource = listx;
            harga.ItemsSource = null;
            harga.ItemsSource = listx;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Promo!");
            else if (jenis.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Promo!");
            else if (nominal.Text.Equals("")) MessageBox.Show("Mohon Isi Field Harga Promo!");
            else if (diskon.Text.Equals("")) MessageBox.Show("Mohon Isi Field Diskon!");
            else if (Convert.ToInt32(diskon.Text)>=100) MessageBox.Show("Diskon tidak bisa >= 100% !");
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("insert into member values (:id,initcap(:jenis),:harga,:potongan)", conn);
                    cmd.Parameters.Add(":id", id.Text);
                    cmd.Parameters.Add(":jenis", jenis.Text);
                    cmd.Parameters.Add(":harga", Convert.ToInt32(nominal.Text));
                    cmd.Parameters.Add(":potongan", Convert.ToInt32(diskon.Text));

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    reset();
                    MessageBox.Show("Member Baru Berhasil Ditambahkan!");
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
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Promo!");
            else if (jenis.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Promo!");
            else if (nominal.Text.Equals("")) MessageBox.Show("Mohon Isi Field Harga Promo!");
            else if (diskon.Text.Equals("")) MessageBox.Show("Mohon Isi Field Diskon!");
            else if (Convert.ToInt32(diskon.Text) >= 100) MessageBox.Show("Diskon tidak bisa >= 100% !");
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("update member set jenis_member = initcap(:jenis), harga_member = :harga, " +
                        "diskon_pembelian = :potongan where id_member = :id", conn);
                    cmd.Parameters.Add(":jenis", jenis.Text);
                    cmd.Parameters.Add(":harga", Convert.ToInt32(nominal.Text));
                    cmd.Parameters.Add(":potongan", Convert.ToInt32(diskon.Text));
                    cmd.Parameters.Add(":id", id.Text);

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    reset();
                    MessageBox.Show("Member Berhasil DiUpdate!");
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
            if (dgvMember.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Member Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from Member where id_member = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Member Berhasil!");
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
            if (rjenis.IsChecked == false && rharga.IsChecked == false && rpotongan.IsChecked == false)
                MessageBox.Show("Mohon Isi Kategori Filter Terlebih Dahulu!");
            else if ((rpotongan.IsChecked == true||rharga.IsChecked == true) && keyword.Text.Equals("")) MessageBox.Show("Mohon Isi Keyword Terlebih Dahulu!");
            else
            {
                try
                {
                    string where = "";
                    if (rjenis.IsChecked == true) where = " upper(jenis_member) like '%" + keyword.Text.ToUpper() + "%'";
                    else if (rharga.IsChecked == true)
                    {
                        if (harga.SelectedIndex == 0) where = " harga_member >= " + keyword.Text;
                        else if (harga.SelectedIndex == 1) where = " harga_member <= " + keyword.Text;
                    }
                    else if (rpotongan.IsChecked == true)
                    {
                        if (potongan.SelectedIndex == 0) where = " diskon_pembelian >= " + keyword.Text;
                        else if (potongan.SelectedIndex == 1) where = " diskon_pembelian <= " + keyword.Text;
                    }

                    ds = new DataTable();
                    OracleCommand cmd = new OracleCommand();
                    da = new OracleDataAdapter();
                    dgvMember.Columns.Clear();
                    cmd.Connection = conn;
                    cmd.CommandText = "select id_member as \"ID\", jenis_member as \"Jenis Member\", harga_member as \"Harga Member\", diskon_pembelian||'%' as \"Diskon\" " +
                        "from member where " + where +
                        "order by 1";
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    DataGridTextColumn newC = new DataGridTextColumn() { Header = "Harga" };
                    Binding bind = new Binding("Harga Member") { StringFormat = "Rp. {0:N0}" };
                    newC.Binding = bind;
                    dgvMember.ItemsSource = ds.DefaultView;
                    dgvMember.Columns.Insert(2, newC);
                    dgvMember.Columns.RemoveAt(3);
                    conn.Close();

                    caricari = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void DgvMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvMember.SelectedIndex != -1)
            {
                inserts = 0;
                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id.Text = ds.Rows[dgvMember.SelectedIndex][0].ToString();
                jenis.Text = ds.Rows[dgvMember.SelectedIndex][1].ToString();
                nominal.Text = ds.Rows[dgvMember.SelectedIndex][2].ToString();
                diskon.Text = ds.Rows[dgvMember.SelectedIndex][3].ToString();
            }
        }

        private void Rjenis_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            harga.IsEnabled = false;
            harga.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rpotongan_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = true;
            potongan.SelectedIndex = 0;
            harga.IsEnabled = false;
            harga.SelectedIndex = -1;
            keyword.Text = "";
        }

        private void Rharga_Checked(object sender, RoutedEventArgs e)
        {
            potongan.IsEnabled = false;
            potongan.SelectedIndex = -1;
            harga.IsEnabled = true;
            harga.SelectedIndex = 0;
            keyword.Text = "";
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rharga.IsChecked == true || rpotongan.IsChecked == true)
            {
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = keyword.Text.Length;
            }
        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mc = new MasterCustomer(w_utama);
            this.Close();
            mc.Show();
        }

        private void Btn_supplier_Click(object sender, RoutedEventArgs e)
        {
            MasterSupplier ms = new MasterSupplier(w_utama);
            this.Close();
            ms.Show();
        }

        private void Btn_karyawan_Click(object sender, RoutedEventArgs e)
        {
            MasterKaryawan mk = new MasterKaryawan(w_utama);
            this.Close();
            mk.Show();
        }

        private void Btn_alat_musik_Click(object sender, RoutedEventArgs e)
        {
            MasterAlatMusik mam = new MasterAlatMusik(w_utama);
            this.Close();
            mam.Show();
        }

        private void Btn_aksesoris_Click(object sender, RoutedEventArgs e)
        {
            MasterAksesoris ma = new MasterAksesoris(w_utama);
            this.Close();
            ma.Show();
        }

        private void Btn_produsen_Click(object sender, RoutedEventArgs e)
        {
            MasterProdusen mps = new MasterProdusen(w_utama);
            this.Close();
            mps.Show();
        }

        private void Btn_jenis_Click(object sender, RoutedEventArgs e)
        {
            MasterJenis mj = new MasterJenis(w_utama);
            this.Close();
            mj.Show();
        }

        private void Btn_promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo mpr = new MasterPromo(w_utama);
            this.Close();
            mpr.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w_utama.Show();
        }

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master(w_utama);
            this.Close();
            mma.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w_utama.Show();
        }
    }
}
