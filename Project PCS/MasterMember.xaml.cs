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
        int caricari;
        List<string> listx;
        public MasterMember()
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
            da = new OracleDataAdapter("select id_member as \"ID\", jenis_member as \"Jenis Member\", harga_member as \"Harga Member\", diskon_pembelian||'%' as \"Diskon\" " +
                "from member order by 1",conn);
            dgvMember.Columns.Clear();
            da.Fill(ds);
            dgvMember.ItemsSource = ds.DefaultView;
            kolom();
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
            //inserts = 1;

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
                for (int i = 0; i < kata.Length; i++)
                {
                    if (Char.IsDigit(kata[i])) kata2 += kata[i];
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
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Member!");
            else if (jenis.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Member!");
            else if (nominal.Text.Equals("")) MessageBox.Show("Mohon Isi Field Harga Member!");
            else if (diskon.Text.Equals("")) MessageBox.Show("Mohon Isi Field Diskon!");
            else if (Convert.ToInt32(diskon.Text)>=100) MessageBox.Show("Diskon tidak bisa >= 100% !");
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(jenis.Text.ToUpper())) ada = true;
                }
                if (ada) MessageBox.Show("Nama Member Sudah Ada! Masukkan Nama Lain.");
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
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field Kode Member!");
            else if (jenis.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Member!");
            else if (nominal.Text.Equals("")) MessageBox.Show("Mohon Isi Field Harga Member!");
            else if (diskon.Text.Equals("")) MessageBox.Show("Mohon Isi Field Diskon!");
            else
            {
                string[] x = diskon.Text.Split('%');
                if (Convert.ToInt32(x[0]) >= 100) MessageBox.Show("Diskon tidak bisa >= 100% !");
                else
                {
                    bool ada = false;
                    foreach (DataRow row in ds.Rows)
                    {
                        if (row[1].ToString().ToUpper().Equals(jenis.Text.ToUpper()) && !row[0].Equals(id.Text)) ada = true;
                    }
                    if (ada) MessageBox.Show("Nama Member Sudah Ada! Masukkan Nama Lain.");
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
                            cmd.Parameters.Add(":potongan", Convert.ToInt32(x[0]));
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
                    da = new OracleDataAdapter("select id_member as \"ID\", jenis_member as \"Jenis Member\", harga_member as \"Harga Member\", diskon_pembelian||'%' as \"Diskon\" " +
                        "from member where " + where +
                        "order by 1",conn);
                    da.Fill(ds);
                    dgvMember.ItemsSource = ds.DefaultView;

                    caricari = 1;
                    kolom();
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
                //inserts = 0;
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
                int temp = keyword.SelectionStart;
                string kata2 = katabaru(keyword.Text);
                keyword.Text = kata2;
                keyword.SelectionStart = temp;
            }
        }
        private void kolom()
        {
            dgvMember.Columns[2].ClipboardContentBinding.StringFormat = "Rp. {0:N0}";
            dgvMember.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvMember.Columns[1].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgvMember.Columns[2].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
            dgvMember.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
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

        private void Btn_promo_Click(object sender, RoutedEventArgs e)
        {
            MasterPromo mpr = new MasterPromo();
            this.Close();
            mpr.Show();
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

        private void DgvMember_Loaded(object sender, RoutedEventArgs e) { kolom(); }

        private void Nominal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = nominal.SelectionStart;
            string kata2 = katabaru(nominal.Text);
            nominal.Text = kata2;
            nominal.SelectionStart = temp;
        }

        private void Diskon_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = diskon.SelectionStart;
            string kata2 = katabaru(diskon.Text);
            diskon.Text = kata2;
            diskon.SelectionStart = temp;
        }
    }
}
