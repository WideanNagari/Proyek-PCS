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
    /// Interaction logic for MasterProdusen.xaml
    /// </summary>
    public partial class MasterProdusen : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari;
        public MasterProdusen()
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
            da = new OracleDataAdapter("select id_produsen as \"ID\", nama_produsen as \"Nama Produsen\" from produsen order by 1",conn);
            da.Fill(ds);
            dgvProdusen.ItemsSource = ds.DefaultView;
            conn.Close();
        }
        private void reset()
        {
            dgvProdusen.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;
            

            keyword.Text = "";
            id.Text = "";
            nama.Text = "";
            keyword.Text = "";

            id.IsReadOnly = true;
            getId();
        }
        private void getId()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "autogenProdusen"
            };
            
            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "id_produsen",
                OracleDbType = OracleDbType.Varchar2,
                Size = 4
            });

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                id.Text = cmd.Parameters["id_produsen"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            conn.Close();
        }
        private void DgvProdusen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvProdusen.SelectedIndex != -1)
            {
                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id.Text = ds.Rows[dgvProdusen.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvProdusen.SelectedIndex][1].ToString();
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

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ds = new DataTable();
                da = new OracleDataAdapter("select id_produsen as \"ID\", nama_produsen as \"Nama Produsen\" " +
                    "from produsen " +
                    "where upper(nama_produsen) like '%" + keyword.Text.ToUpper() + "%' " +
                    "order by 1",conn);
                da.Fill(ds);
                dgvProdusen.ItemsSource = ds.DefaultView;
                conn.Close();
                caricari = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            reset();
            caricari = 0;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Nama Produsen!");
            }
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper())) ada = true;
                }
                if (ada) MessageBox.Show("Nama Produsen Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("insert into produsen values (:id,initcap(:nama))", conn);
                        cmd.Parameters.Add(":id", id.Text);
                        cmd.Parameters.Add(":nama", nama.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Produsen Baru Berhasil Ditambahkan!");
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
            if (nama.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Nama Produsen!");
            }
            else
            {
                bool ada = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (row[1].ToString().ToUpper().Equals(nama.Text.ToUpper()) && !row[0].Equals(id.Text)) ada = true;
                }
                if (ada) MessageBox.Show("Nama Produsen Sudah Ada! Masukkan Nama Lain.");
                else
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("update produsen set nama_produsen = :nama where id_produsen = :id", conn);
                        cmd.Parameters.Add(":nama", nama.Text);
                        cmd.Parameters.Add(":id", id.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Produsen Berhasil diUpdate!");
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
            if (dgvProdusen.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Produsen Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from produsen where id_produsen = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Produsen Berhasil!");
            }
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

        private void Btn_master_Click(object sender, RoutedEventArgs e)
        {
            Menu_Master mma = new Menu_Master();
            this.Close();
            mma.Show();
        }
    }
}
