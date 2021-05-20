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
    /// Interaction logic for MasterKaryawan.xaml
    /// </summary>
    public partial class MasterKaryawan : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari;
        private class Pass
        {
            public string id { get; set; }
            public string pass { get; set; }
        }
        List<Pass> arrPass;

        public MasterKaryawan()
        {
            InitializeComponent();
            conn = MainWindow.conn;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            reset();
            caricari = 0;
        }

        private void reset()
        {
            salah.Visibility = Visibility.Hidden;
            pass2.Visibility = Visibility.Hidden;
            conPass2.Visibility = Visibility.Hidden;
            status.Visibility = Visibility.Hidden;
            active.Visibility = Visibility.Hidden;
            non.Visibility = Visibility.Hidden;

            pass.Password = "";
            conPass.Password = "";
            conPass2.Text = "";
            pass2.Text = "";
            id.Text = "";
            nama.Text = "";
            notelp.Text = "";
            alamat.Text = "";
            lahir.Text = "";

            rlaki.IsChecked = false;
            rperempuan.IsChecked = false;
            active.IsChecked = false;
            non.IsChecked = false;

            reveal.IsEnabled = false;
            reveal2.IsEnabled = false;

            id.IsReadOnly = true;

            sd.Visibility = Visibility.Visible;

            namaKar.Text = "";
            rAll.IsChecked = false;
            rL.IsChecked = false;
            rP.IsChecked = false;
            rAll2.IsChecked = false;
            ractive.IsChecked = false;
            rnon.IsChecked = false;
            checkAll.IsChecked = false;
            masukAwal.Text = "";
            masukAkhir.Text = "";

            getId();
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;
        }

        private void loadData()
        {
            ds = new DataTable();
            da = new OracleDataAdapter("select id_Karyawan as \"ID\", nama_Karyawan as \"Nama Karyawan\", " +
                "(case jk_karyawan" +
                "   when 'M' then 'Laki-Laki'" +
                "   when 'F' then 'Perempuan'" +
                "end) as \"Jenis Kelamin\", " +
                "NoTelp_karyawan as \"No Telepon\", alamat_karyawan as \"Alamat\", to_char(DOB_karyawan,'DD-MM-YYYY') as \"DOB\"" +
                ", to_char(Tgl_Masuk,'DD-MM-YYYY') as \"Tanggal Masuk\" " +
                ", status_karyawan as \"Status\" " +
                "from karyawan order by 1",conn);
            da.Fill(ds);
            dgvKaryawan.ItemsSource = ds.DefaultView;
            conn.Close();

            conn.Open();
            OracleCommand cmd = new OracleCommand("select password, id_Karyawan from karyawan order by 2", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            arrPass = new List<Pass>();
            while (reader.Read())
            {
                arrPass.Add(new Pass()
                {
                    id = reader.GetString(1),
                    pass = reader.GetString(0)
                });
            }
            conn.Close();
            kolom();
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

        private void DgvKaryawan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvKaryawan.SelectedIndex != -1)
            {
                status.Visibility = Visibility.Visible;
                active.Visibility = Visibility.Visible;
                non.Visibility = Visibility.Visible;

                id.Text = ds.Rows[dgvKaryawan.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvKaryawan.SelectedIndex][1].ToString();
                string jk = ds.Rows[dgvKaryawan.SelectedIndex][2].ToString();
                notelp.Text = ds.Rows[dgvKaryawan.SelectedIndex][3].ToString();
                alamat.Text = ds.Rows[dgvKaryawan.SelectedIndex][4].ToString();
                lahir.Text = ds.Rows[dgvKaryawan.SelectedIndex][5].ToString();
                string status2 = ds.Rows[dgvKaryawan.SelectedIndex][7].ToString();

                rlaki.IsChecked = true;
                if (jk.Equals("Perempuan")) rperempuan.IsChecked = true;

                active.IsChecked = true;
                if (status2.Equals("0")) non.IsChecked = true;

                string pas = "";
                foreach (Pass p in arrPass)
                {
                    if (p.id.Equals(id.Text)) pas = p.pass;
                }

                pass.Password = pas;
                conPass.Password = pas;

                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
            }
        }

        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!conPass.Password.Equals("") && !conPass.Password.Equals(pass.Password)) salah.Visibility = Visibility.Visible;
            else if (!conPass.Password.Equals("") && conPass.Password.Equals(pass.Password)) salah.Visibility = Visibility.Hidden;

            if (pass.Password.Equals("")) reveal.IsEnabled = false;
            else reveal.IsEnabled = true;
        }

        private void ConPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!conPass.Password.Equals(pass.Password)) salah.Visibility = Visibility.Visible;
            else salah.Visibility = Visibility.Hidden;

            if (conPass.Password.Equals("")) reveal2.IsEnabled = false;
            else reveal2.IsEnabled = true;
        }

        private void Reveal_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pass.Visibility = Visibility.Hidden;
            pass2.Visibility = Visibility.Visible;
            pass2.Text = pass.Password.ToString();
        }

        private void Reveal_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            pass.Visibility = Visibility.Visible;
            pass2.Visibility = Visibility.Hidden;
            pass2.Text = "";
        }

        private void Reveal2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            conPass.Visibility = Visibility.Hidden;
            conPass2.Visibility = Visibility.Visible;
            conPass2.Text = conPass.Password.ToString();
        }

        private void Reveal2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            conPass.Visibility = Visibility.Visible;
            conPass2.Visibility = Visibility.Hidden;
            conPass2.Text = "";
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
            if (rAll.IsChecked == false && rL.IsChecked == false && rP.IsChecked == false)
                MessageBox.Show("Mohon Pilih Kategori Jenis Kelamin atau Pilih All!");
            else if (checkAll.IsChecked == false && (masukAkhir.Text.Equals("") || masukAwal.Text.Equals("")))
                MessageBox.Show("Mohon Isi Range Tanggal atau Pilih All!");
            else if (rAll2.IsChecked == false && ractive.IsChecked == false && rnon.IsChecked == false)
                MessageBox.Show("Mohon Pilih Kategori Status atau Pilih All!");
            else
            {
                string where = "upper(nama_karyawan) like '%" + namaKar.Text.ToUpper() + "%'";
                if (rL.IsChecked == true) where += " and jk_karyawan = 'M' ";
                else if (rP.IsChecked == true) where += " and jk_karyawan = 'F' ";

                if (checkAll.IsChecked == false) where += " and tgl_masuk >= to_date('" + masukAwal.Text + "','DD-MM-YYYY') " +
                        "and tgl_masuk <= to_date('" + masukAkhir.Text + "','DD-MM-YYYY') ";

                if (ractive.IsChecked == true) where += " and status_karyawan = 1 ";
                else if (rnon.IsChecked == true) where += " and status_karyawan = 0 ";

                try
                {
                    ds = new DataTable();
                    da = new OracleDataAdapter("select id_Karyawan as \"ID\", nama_Karyawan as \"Nama Karyawan\", " +
                        "(case jk_karyawan" +
                        "   when 'M' then 'Laki-Laki'" +
                        "   when 'F' then 'Perempuan'" +
                        "end) as \"Jenis Kelamin\", " +
                        "NoTelp_karyawan as \"No Telepon\", alamat_karyawan as \"Alamat\", to_char(DOB_karyawan,'DD-MM-YYYY') as \"DOB\"" +
                        ", to_char(Tgl_Masuk,'DD-MM-YYYY') as \"Tanggal Masuk\" " +
                        ", status_karyawan as \"Status\" " +
                        "from karyawan where " + where +
                        " order by 1",conn);
                    da.Fill(ds);
                    dgvKaryawan.ItemsSource = ds.DefaultView;
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

        private void CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            sd.Visibility = Visibility.Hidden;
            masukAwal.Visibility = Visibility.Hidden;
            masukAkhir.Visibility = Visibility.Hidden;
        }

        private void CheckAll_Unchecked(object sender, RoutedEventArgs e)
        {
            sd.Visibility = Visibility.Visible;
            masukAwal.Visibility = Visibility.Visible;
            masukAkhir.Visibility = Visibility.Visible;
        }

        private void getId()
        {
            OracleCommand cmd = new OracleCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = conn,
                CommandText = "autogenKaryawan"
            };

            cmd.Parameters.Add(new OracleParameter()
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "id_karyawan",
                OracleDbType = OracleDbType.Varchar2,
                Size = 6
            });

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                id.Text = cmd.Parameters["id_karyawan"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            conn.Close();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Karyawan!");
            else if (rlaki.IsChecked == false && rperempuan.IsChecked == false) MessageBox.Show("Mohon Isi Jenis Kelamin Karyawan!");
            else if (notelp.Text.Length != 12 && !notelp.Text.Equals("")) MessageBox.Show("Nomor Telepon harus 12 digit!");
            else if (lahir.Text.Equals("")) MessageBox.Show("Mohon Isi Tanggal Lahir Karyawan!");
            else if (pass.Password.Equals("") || conPass.Password.Equals("")) MessageBox.Show("Mohon Isi Password dan Confirm Password Karyawan!");
            else if (salah.Visibility == Visibility.Visible) MessageBox.Show("Password dan Confirm Password Karyawan Tidak Sama!");
            else
            {
                try
                {
                    string jk = "M";
                    if (rperempuan.IsChecked == true) jk = "F";
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("insert into karyawan values (:id, initcap(:nama), :jk, :pass,:alamat,:no,to_date('" + lahir.Text + "','DD/MM/YYYY'), sysdate,'1')", conn);
                    cmd.Parameters.Add(":id", id.Text);
                    cmd.Parameters.Add(":nama", nama.Text);
                    cmd.Parameters.Add(":jk", jk);
                    cmd.Parameters.Add(":pass", pass.Password.ToString());
                    cmd.Parameters.Add(":alamat", alamat.Text);
                    cmd.Parameters.Add(":no", notelp.Text);

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    reset();
                    MessageBox.Show("Karyawan Baru Berhasil Ditambahkan!");
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
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Karyawan!");
            else if (rlaki.IsChecked == false && rperempuan.IsChecked == false) MessageBox.Show("Mohon Isi Jenis Kelamin Karyawan!");
            else if (notelp.Text.Length != 12 && !notelp.Text.Equals("")) MessageBox.Show("Nomor Telepon harus 12 digit!");
            else if (lahir.Text.Equals("")) MessageBox.Show("Mohon Isi Tanggal Lahir Karyawan!");
            else if (pass.Password.Equals("") || conPass.Password.Equals("")) MessageBox.Show("Mohon Isi Password dan Confirm Password Karyawan!");
            else if (salah.Visibility == Visibility.Visible) MessageBox.Show("Password dan Confirm Password Karyawan Tidak Sama!");
            else
            {
                try
                {
                    string jk = "M";
                    if (rperempuan.IsChecked == true) jk = "F";
                    string stat = "0";
                    if (active.IsChecked == true) stat = "1";
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("Update karyawan set nama_karyawan = initcap(:nama), " +
                        "jk_karyawan = :jk, password = :pass, alamat_karyawan = :alamat, notelp_karyawan = :no," +
                        "dob_karyawan = to_date('" + lahir.Text + "', 'DD/MM/YYYY'), status_karyawan = :stat" +
                        " where id_karyawan = :id", conn);
                    cmd.Parameters.Add(":nama", nama.Text);
                    cmd.Parameters.Add(":jk", jk);
                    cmd.Parameters.Add(":pass", pass.Password.ToString());
                    cmd.Parameters.Add(":alamat", alamat.Text);
                    cmd.Parameters.Add(":no", notelp.Text);
                    cmd.Parameters.Add(":stat", stat);
                    cmd.Parameters.Add(":id", id.Text);

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
            if (dgvKaryawan.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Karyawan Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from karyawan where id_karyawan = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Karyawan Berhasil!");
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

        private void Btn_customer_Click(object sender, RoutedEventArgs e)
        {
            MasterCustomer mk = new MasterCustomer();
            this.Close();
            mk.Show();
        }
        private void kolom()
        {
            dgvKaryawan.Columns[0].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[2].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[3].Width = new DataGridLength(0.9, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[4].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[5].Width = new DataGridLength(0.7, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[6].Width = new DataGridLength(0.9, DataGridLengthUnitType.Star);
            dgvKaryawan.Columns[7].Width = new DataGridLength(0.4, DataGridLengthUnitType.Star);
        }
        private void DgvKaryawan_Loaded(object sender, RoutedEventArgs e)
        {
            kolom();
        }
    }
}
