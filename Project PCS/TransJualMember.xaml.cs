using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for TransJualMember.xaml
    /// </summary>
    public partial class TransJualMember : Window
    {
        OracleConnection conn;
        DataTable dtMember, dtCustomer;
        OracleDataAdapter daMember, daCustomer;
        string idKaryawan;
        public TransJualMember()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            idKaryawan = Menu_Trans.idKar;
            namaKar.Content = Menu_Trans.namaKar;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadTgl();
            loadData();
            reset();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Menu_Trans log = new Menu_Trans();
            this.Close();
            log.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Btn_trans_Click(object sender, RoutedEventArgs e)
        {
            Menu_Trans mt = new Menu_Trans();
            this.Close();
            mt.Show();
        }

        private void Btn_trans_jual_Click(object sender, RoutedEventArgs e)
        {
            TransJual tj = new TransJual();
            this.Close();
            tj.Show();
        }

        private void Btn_trans_beli_Click(object sender, RoutedEventArgs e)
        {
            TransBeli tb = new TransBeli();
            this.Close();
            tb.Show();
        }

        private void loadTgl()
        {
            string query = "select to_char(sysdate,'dd MONTH yyyy') FROM DUAL";
            OracleCommand cmd = new OracleCommand(query, conn);
            conn.Open();
            tgl_hari.Content = cmd.ExecuteScalar().ToString();
            conn.Close();

            query = "SELECT NAMA_KARYAWAN FROM KARYAWAN WHERE ID_KARYAWAN = '"+idKaryawan+"'";
            cmd = new OracleCommand(query, conn);
            conn.Open();
            namaKar.Content = cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        private void Beli_Click(object sender, RoutedEventArgs e)
        {
            if(customer.Content.ToString()!="" && cbmember.SelectedIndex!=-1 && subtotal.Content.ToString()!="0")
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int total = int.Parse(subtotal.Content.ToString());
                        string nota = "";
                        string id_customer = dtCustomer.Rows[dgvCustomer.SelectedIndex][0].ToString();

                        OracleCommand cmd = new OracleCommand($"INSERT INTO PENJUALAN_MEMBER VALUES(" +
                            $"'{nota}', SYSDATE, '{idKaryawan}', '{id_customer}', '{cbmember.SelectedValue}', '{total}', 1)", conn);
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                        conn.Close();
                        MessageBox.Show("Bayar sukses");

                        cmd = new OracleCommand("select max(nota_jual_member) from penjualan_member where nota_jual_member like '%'||to_char(sysdate,'ddmmyy')||'%'", conn);
                        conn.Open();
                        string noNota = cmd.ExecuteScalar().ToString();
                        conn.Close();
                        Nota n = new Nota("Nota Penjualan Membership", this, noNota);
                        this.Hide();
                        n.Show();

                        reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        trans.Rollback();
                        conn.Close();
                    }
                }
            } else
            {
                MessageBox.Show("Form mohon diisi terlebih dahulu");
            }
            
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void Cari2_Click(object sender, RoutedEventArgs e)
        {
            string key = keyword.Text;
            if(rnama.IsChecked==true)
            {
                rnama.Foreground = Brushes.Blue;
                rnotelp.Foreground = Brushes.Black;
                string query = "SELECT ID_CUSTOMER AS \"ID\", NAMA_CUSTOMER AS \"Nama\", " +
                "CASE WHEN JK_CUSTOMER = \'M\' THEN \'Laki-Laki\' ELSE \'Perempuan\' END AS \"Jenis Kelamin\", " +
                "ALAMAT_CUSTOMER AS \"Alamat\",  NOTELP_CUSTOMER AS \"Nomor Telepon\" " +
                "FROM CUSTOMER WHERE UPPER(NAMA_CUSTOMER) like UPPER(\'%"+key+"%\') ORDER BY ID_CUSTOMER";
                daCustomer = new OracleDataAdapter(query, conn);
                dtCustomer = new DataTable();
                daCustomer.Fill(dtCustomer);
                dgvCustomer.ItemsSource = dtCustomer.DefaultView;
                kolom();
            } else if(rnotelp.IsChecked==true)
            {
                rnotelp.Foreground = Brushes.Blue;
                rnama.Foreground = Brushes.Black;
                string query = "SELECT ID_CUSTOMER AS \"ID\", NAMA_CUSTOMER AS \"Nama\", " +
                "CASE WHEN JK_CUSTOMER = \'M\' THEN \'Laki-Laki\' ELSE \'Perempuan\' END AS \"Jenis Kelamin\", " +
                "ALAMAT_CUSTOMER AS \"Alamat\",  NOTELP_CUSTOMER AS \"Nomor Telepon\" " +
                "FROM CUSTOMER WHERE NOTELP_CUSTOMER like \'%" + key + "%\' ORDER BY ID_CUSTOMER";
                daCustomer = new OracleDataAdapter(query, conn);
                dtCustomer = new DataTable();
                daCustomer.Fill(dtCustomer);
                dgvCustomer.ItemsSource = dtCustomer.DefaultView;
                kolom();
            } else if(keyword.Text.Equals(""))
            {
                reset();
                loadData();
            } else
            {
                MessageBox.Show("Mohon Isi Kategori Filter Terlebih Dahulu!");
            }
        }

        private void Cbmember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbmember.SelectedIndex!=-1)
            {
                string query = "SELECT DISKON_PEMBELIAN FROM MEMBER WHERE ID_MEMBER = '" + cbmember.SelectedValue + "'";
                OracleCommand cmd = new OracleCommand(query, conn);
                conn.Open();
                int diskon = int.Parse(cmd.ExecuteScalar().ToString());
                ket_diskon.Text = diskon.ToString();
                conn.Close();

                query = "SELECT HARGA_MEMBER FROM MEMBER WHERE ID_MEMBER = '" + cbmember.SelectedValue + "' ";
                cmd = new OracleCommand(query, conn);
                conn.Open();
                int sub = int.Parse(cmd.ExecuteScalar().ToString());
                subtotal.Content = sub.ToString();
                conn.Close();
            }
        }

        private void DgvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCustomer.SelectedIndex != -1)
            {
                customer.Content = dtCustomer.Rows[dgvCustomer.SelectedIndex][1].ToString();                
            }
        }
        private void kolom()
        {
            dgvCustomer.Columns[0].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[3].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgvCustomer.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DgvCustomer_Loaded(object sender, RoutedEventArgs e) { kolom(); }

        private void loadData()
        {
            string query = "SELECT ID_CUSTOMER AS \"ID\", NAMA_CUSTOMER AS \"Nama\", " +
                "CASE WHEN JK_CUSTOMER = \'M\' THEN \'Laki-Laki\' ELSE \'Perempuan\' END AS \"Jenis Kelamin\", " +
                "ALAMAT_CUSTOMER AS \"Alamat\",  NOTELP_CUSTOMER AS \"Nomor Telepon\" " +
                "FROM CUSTOMER ORDER BY ID_CUSTOMER";

            daCustomer = new OracleDataAdapter(query, conn);
            dtCustomer = new DataTable();
            daCustomer.Fill(dtCustomer);
            dgvCustomer.ItemsSource = dtCustomer.DefaultView;

            daMember = new OracleDataAdapter("select ID_MEMBER, ID_MEMBER || ' - ' || JENIS_MEMBER AS \"KET\" from MEMBER order by ID_MEMBER", conn);
            dtMember = new DataTable();
            daMember.Fill(dtMember);
            cbmember.ItemsSource = dtMember.DefaultView;
            cbmember.DisplayMemberPath = dtMember.Columns["KET"].ToString();
            cbmember.SelectedValuePath = "ID_MEMBER";
            cbmember.SelectedIndex = -1;
            kolom();
        }
        
        private void reset()
        {
            dgvCustomer.SelectedIndex = -1;
            rnama.Foreground = Brushes.Black;
            rnotelp.Foreground = Brushes.Black;
            cbmember.SelectedIndex = -1;
            rnama.IsChecked = false;
            rnotelp.IsChecked = false;
            ket_diskon.Text = "";
            keyword.Text = "";
            customer.Content = "";
            subtotal.Content = "0";
        }
    }
}
