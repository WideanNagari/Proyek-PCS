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
    /// Interaction logic for MasterSupplier.xaml
    /// </summary>
    public partial class MasterSupplier : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari;
        public MasterSupplier()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }


        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select id_Supplier as \"ID\", nama_Supplier as \"Nama Supplier\", " +
                "cp_supplier as \"Contact Person\", pn_supplier as \"No Telepon\", alamat_supplier as \"Alamat\" " +
                "from supplier order by 1",conn);
            da.Fill(ds);
            dgvSupplier.ItemsSource = ds.DefaultView;
            conn.Close();
            kolom();
        }

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            cp.Text = "";
            notelp.Text = "";
            alamat.Text = "";
            keyword.Text = "";

            id.IsReadOnly = true;

            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;

            dgvSupplier.SelectedIndex = -1;
            
            rnama.IsChecked = false;
            rcp.IsChecked = false;

            getId();
            nama.Text = "PT. ";
        }

        private void getId()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "autogenSupplier"
            };

            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "id_supplier",
                OracleDbType = OracleDbType.Varchar2,
                Size = 5
            });

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                id.Text = cmd.Parameters["id_supplier"].Value.ToString();
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

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Supplier!");
            else if (cp.Text.Equals("")) MessageBox.Show("Mohon Isi Field Contact Person Supplier!");
            else if (notelp.Text.Length != 12) MessageBox.Show("Nomor Telepon Supplier harus 12 digit!");
            else if (alamat.Text.Equals("")) MessageBox.Show("Mohon Isi Field Alamat Supplier!");
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("insert into supplier values (:id,initcap(:nama),initcap(:cp),:no,:alamat)", conn);
                    cmd.Parameters.Add(":id", id.Text);
                    cmd.Parameters.Add(":nama", nama.Text);
                    cmd.Parameters.Add(":cp", cp.Text);
                    cmd.Parameters.Add(":no", notelp.Text);
                    cmd.Parameters.Add(":alamat", alamat.Text);

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    reset();
                    MessageBox.Show("Supplier Baru Berhasil Ditambahkan!");
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
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Supplier!");
            else if (cp.Text.Equals("")) MessageBox.Show("Mohon Isi Field Contact Person Supplier!");
            else if (notelp.Text.Length != 12) MessageBox.Show("Nomor Telepon Supplier harus 12 digit!");
            else if (alamat.Text.Equals("")) MessageBox.Show("Mohon Isi Field Alamat Supplier!");
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("update supplier set nama_supplier = initcap(:nama), cp_supplier = :jk, alamat_supplier = :alamat, " +
                        "pn_supplier = :no where id_supplier = :id", conn);
                    cmd.Parameters.Add(":nama", nama.Text);
                    cmd.Parameters.Add(":cp", cp.Text);
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(dgvSupplier.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Supplier Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from supplier where id_supplier = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Supplier Berhasil!");
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
            if (rnama.IsChecked == false && rcp.IsChecked == false)
                MessageBox.Show("Mohon Pilih Kategori Filter!");
            else
            {
                string where = "";
                if (rnama.IsChecked == true) where += " upper(nama_supplier) like '%" + keyword.Text.ToUpper() + "%'";
                else if (rcp.IsChecked == true) where += " upper(cp_supplier) like '%" + keyword.Text.ToUpper() + "%'";

                try
                {
                    ds = new DataTable();
                    da = new OracleDataAdapter("select id_Supplier as \"ID\", nama_Supplier as \"Nama Supplier\", " +
                        "cp_supplier as \"Contact Person\", pn_supplier as \"No Telepon\", alamat_supplier as \"Alamat\" " +
                        "from supplier where " + where +
                        " order by 1",conn);
                    da.Fill(ds);
                    dgvSupplier.ItemsSource = ds.DefaultView;
                    conn.Close();
                    kolom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                caricari = 1;
            }
        }
        
        private void DgvSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvSupplier.SelectedIndex != -1)
            {
                id.Text = ds.Rows[dgvSupplier.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvSupplier.SelectedIndex][1].ToString();
                cp.Text = ds.Rows[dgvSupplier.SelectedIndex][2].ToString();
                notelp.Text = ds.Rows[dgvSupplier.SelectedIndex][3].ToString();
                alamat.Text = ds.Rows[dgvSupplier.SelectedIndex][4].ToString();

                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
            }
        }

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mm = new MasterCustomer();
            this.Close();
            mm.ShowDialog();
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

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master();
            this.Close();
            mma.Show();
        }
        private void kolom()
        {
            dgvSupplier.Columns[0].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvSupplier.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvSupplier.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvSupplier.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvSupplier.Columns[4].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        }

        private void DgvSupplier_Loaded(object sender, RoutedEventArgs e) { kolom(); }
    }
}
