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
    /// Interaction logic for TransJual.xaml
    /// </summary>
    public partial class TransJual : Window
    {
        OracleConnection conn;
        DataTable dam, dak, dt;
        DataTable produsens, jeniss, customers, promos;
        DataTable dtdjual, dtdjuala, dthjual;
        OracleDataAdapter daam, daak, dat, uni;
        OracleDataAdapter djual, djualA, hjual;
        
        OracleCommandBuilder builder;
        string idMember, namaMember;
        int diskon, promo;
        string idKaryawan;
        public TransJual()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            idKaryawan = Menu_Trans.idKar;
            namaKar.Content = Menu_Trans.namaKar;
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

        private void Btn_jual_member_Click(object sender, RoutedEventArgs e)
        {
            TransJualMember tjm = new TransJualMember();
            this.Close();
            tjm.Show();
        }

        private void Btn_trans_beli_Click(object sender, RoutedEventArgs e)
        {
            TransBeli tb = new TransBeli();
            this.Close();
            tb.Show();
        }

        private void loadData()
        {
            daam = new OracleDataAdapter("select id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                "from alat_musik a, jenis_alat_musik j, produsen p " +
                "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen and a.stok>0 " +
                "order by 1", conn);
            dam = new DataTable();
            daam.Fill(dam);
            dgvMusik.ItemsSource = dam.DefaultView;
            kolom();

            daak = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                "from aksesoris where stok>0 order by 1", conn);
            dak = new DataTable();
            daak.Fill(dak);
            dgvAksesoris.ItemsSource = dak.DefaultView;
            kolom2();

            dgvTrans.Columns.Clear();
            dat = new OracleDataAdapter("select '' as \"ID\", '' as \"Nama Barang\", " +
                "'' as \"Quantity\", '' as \"Harga\", '' as \"Jenis Barang\" from dual where 1=2", conn);
            dt = new DataTable();
            dat.Fill(dt);
            dgvTrans.ItemsSource = dt.DefaultView;
            kolom3();

            djual = new OracleDataAdapter("select * from d_jual", conn);
            builder = new OracleCommandBuilder(djual);
            dtdjual = new DataTable();
            djual.Fill(dtdjual);

            djualA = new OracleDataAdapter("select * from d_jual_aksesoris", conn);
            builder = new OracleCommandBuilder(djualA);
            dtdjuala = new DataTable();
            djualA.Fill(dtdjuala);

            hjual = new OracleDataAdapter("select * from h_jual", conn);
            builder = new OracleCommandBuilder(hjual);
            dthjual = new DataTable();
            hjual.Fill(dthjual);
        }

        private void reset()
        {
            produsen.SelectedIndex = 0;
            jenis.SelectedIndex = 0;
            customer.SelectedIndex = -1;
            kode.SelectedIndex = -1;

            namaMember = "-";
            idMember = "";
            diskon = 0;
            promo = 0;

            keyword.Text = "";
            keywords.Text = "";
            qty.Text = "";
            qty2.Text = "";
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;

            jumlah.Content = "0";
            subtotal.Content = "0";
            member.Content = "-";
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

        private int cekStok(string id, string jenis)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select stok from "+jenis+"=:id";
            cmd.Parameters.Add(":id", id);
            conn.Open();
            string hasil = cmd.ExecuteScalar().ToString();
            conn.Close();
            return Convert.ToInt32(hasil);
        }

        private void updateHarga()
        {
            int total = 0, discount;
            string temp, h;
            foreach (DataRow row in dt.Rows)
            {
                temp = "";
                h = row["harga"].ToString();
                for (int i = 0; i < h.Length; i++)
                {
                    if (Char.IsDigit(h[i])) temp += h[i];
                }
                total += Convert.ToInt32(temp)* Convert.ToInt32(row["Quantity"].ToString());
            }

            jumlah.Content = "" + total;

            if(total == 0) subtotal.Content = "0";
            else
            {
                discount = total * diskon/100;
                int subt = total - discount - promo;
                if (subt < 0)
                {
                    kode.SelectedIndex = -1;
                    subtotal.Content = (subt+promo) + "";
                }else subtotal.Content = subt + "";
            }
        }

        private void AddM_Click(object sender, RoutedEventArgs e)
        {
            if (qty.Text.Equals("")) MessageBox.Show("Mohon Masukkan Jumlah Barang yang Mau Dibeli!");
            else if(dgvMusik.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Barang yang Mau Dibeli!");
            else
            {
                int ctr = 0, pos = -1, qtyy = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ID"].ToString().Equals(dam.Rows[dgvMusik.SelectedIndex][0].ToString()))
                    {
                        pos = ctr;
                        qtyy = Convert.ToInt32(row["Quantity"]);
                    }
                    ctr += 1;
                }
                if (cekStok(dam.Rows[dgvMusik.SelectedIndex][0].ToString(), "alat_musik where id_alat_musik") >= Convert.ToInt32(qty.Text) + qtyy)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = dam.Rows[dgvMusik.SelectedIndex][0].ToString();
                    dr[1] = dam.Rows[dgvMusik.SelectedIndex][1].ToString();
                    dr[2] = Convert.ToInt32(qty.Text)+qtyy;
                    dr[3] = String.Format("Rp. {0:N0}", Convert.ToInt32(dam.Rows[dgvMusik.SelectedIndex][5].ToString()));
                    dr[4] = "Alat Musik";
                    if (pos!=-1) dt.Rows[pos][2] = Convert.ToInt32(qty.Text) + qtyy;
                    else dt.Rows.Add(dr);
                }
                else MessageBox.Show("Stok tidak mencukupi");
                qty.Text = "";
            }
            dgvMusik.SelectedIndex = -1;
            updateHarga();
        }

        private void AddA_Click(object sender, RoutedEventArgs e)
        {
            if (qty2.Text.Equals("")) MessageBox.Show("Mohon Masukkan Jumlah Barang yang Mau Dibeli!");
            else if (dgvAksesoris.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Barang yang Mau Dibeli!");
            else
            {
                int ctr = 0, pos = -1, qtyy = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ID"].ToString().Equals(dak.Rows[dgvAksesoris.SelectedIndex][0].ToString()))
                    {
                        pos = ctr;
                        qtyy = Convert.ToInt32(row["Quantity"]);
                    }
                    ctr += 1;
                }
                if (cekStok(dak.Rows[dgvAksesoris.SelectedIndex][0].ToString(), "aksesoris where id_aksesoris") >= Convert.ToInt32(qty2.Text) + qtyy)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = dak.Rows[dgvAksesoris.SelectedIndex][0].ToString();
                    dr[1] = dak.Rows[dgvAksesoris.SelectedIndex][1].ToString();
                    dr[2] = Convert.ToInt32(qty2.Text) + qtyy;
                    dr[3] = String.Format("Rp. {0:N0}", Convert.ToInt32(dak.Rows[dgvAksesoris.SelectedIndex][3].ToString()));
                    dr[4] = "Aksesoris";
                    if (pos != -1) dt.Rows[pos][2] = Convert.ToInt32(qty2.Text) + qtyy;
                    else dt.Rows.Add(dr);
                }
                else MessageBox.Show("Stok tidak mencukupi");
                qty2.Text = "";
            }
            dgvAksesoris.SelectedIndex = -1;
            updateHarga();
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTrans.SelectedIndex != -1)
            {
                dt.Rows.RemoveAt(dgvTrans.SelectedIndex);
                updateHarga();
            }
        }

        private void Cari2_Click(object sender, RoutedEventArgs e)
        {
            if (rnama.IsChecked == false && rketerangan.IsChecked == false) MessageBox.Show("Mohon Pilih Kategori Filter!");
            else
            {
                string where = "";
                if (rnama.IsChecked == true) where = "upper(nama_aksesoris) like '%" + keyword.Text.ToUpper() + "%'";
                if (rketerangan.IsChecked == true) where = "upper(keterangan) like '%" + keyword.Text.ToUpper() + "%'";
                try
                {
                    daak = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                    "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                    "from aksesoris where stok>0 and " + where +
                    " order by 1", conn);
                    builder = new OracleCommandBuilder(daak);
                    dak = new DataTable();
                    daak.Fill(dak);
                    dgvAksesoris.ItemsSource = dak.DefaultView;
                    kolom2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Beli_Click(object sender, RoutedEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                if (customer.SelectedIndex != -1)
                {
                    conn.Open();
                    using (OracleTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string cus = customer.SelectedValue.ToString();
                            string idpromo = "";
                            if (kode.SelectedIndex != -1) idpromo = kode.SelectedValue.ToString();

                            DataRow dr = dthjual.NewRow();
                            dr[2] = cus;
                            dr[3] = idMember;
                            dr[4] = idKaryawan;
                            dr[5] = jumlah.Content;
                            dr[6] = idpromo;
                            dr[7] = subtotal.Content;
                            dthjual.Rows.Add(dr);
                            hjual.Update(dthjual);
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[4].Equals("Alat Musik"))
                                {
                                    DataRow drA = dtdjual.NewRow();
                                    drA[1] = row[0];
                                    drA[2] = row[2];
                                    dtdjual.Rows.Add(drA);
                                }
                            }
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[4].Equals("Aksesoris"))
                                {
                                    DataRow drA = dtdjuala.NewRow();
                                    drA[1] = row[0];
                                    drA[2] = row[2];
                                    dtdjuala.Rows.Add(drA);
                                }
                            }

                            djual.Update(dtdjual);
                            djualA.Update(dtdjuala);
                            loadData();

                            trans.Commit();
                            conn.Close();
                            MessageBox.Show("Penjualan Berhasil!");
                            dt.Rows.Clear();

                            OracleCommand cmd = new OracleCommand("select max(nota_jual) from h_jual", conn);
                            conn.Open();
                            string noNota = cmd.ExecuteScalar().ToString();
                            conn.Close();
                            Nota n = new Nota("Nota Penjualan", this, noNota);
                            this.Hide();
                            n.Show();

                            reset();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                            MessageBox.Show(ex.ToString());
                            trans.Rollback();
                            conn.Close();
                        }
                    }
                }
                else MessageBox.Show("Pilih Customer Terlebih Dahulu!");
            }
            else MessageBox.Show("Keranjang Belanja Kosong!");
        }

        private void DgvAksesoris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void kolom()
        {
            dgvMusik.Columns[5].ClipboardContentBinding.StringFormat = "Rp. {0:N0}";
            dgvMusik.Columns[0].Width = new DataGridLength(0.4, DataGridLengthUnitType.Star);
            dgvMusik.Columns[1].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
            dgvMusik.Columns[2].Width = new DataGridLength(0.7, DataGridLengthUnitType.Star);
            dgvMusik.Columns[3].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvMusik.Columns[4].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvMusik.Columns[5].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
        }
        private void kolom2()
        {
            dgvAksesoris.Columns[3].ClipboardContentBinding.StringFormat = "Rp. {0:N0}";
            dgvAksesoris.Columns[0].Width = new DataGridLength(0.6, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[1].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[2].Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgvAksesoris.Columns[4].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
        }
        private void kolom3()
        {
            dgvTrans.Columns[0].Width = new DataGridLength(0.7, DataGridLengthUnitType.Star);
            dgvTrans.Columns[1].Width = new DataGridLength(1.4, DataGridLengthUnitType.Star);
            dgvTrans.Columns[2].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
            dgvTrans.Columns[3].Width = new DataGridLength(1.2, DataGridLengthUnitType.Star);
            dgvTrans.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DgvMusik_Loaded(object sender, RoutedEventArgs e) { kolom(); }

        private void DgvAksesoris_Loaded(object sender, RoutedEventArgs e) { kolom2(); }
        
        private void DgvTrans_Loaded(object sender, RoutedEventArgs e) { kolom3(); }

        private void Kode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kode.SelectedIndex!=-1)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select besar_potongan from promo where kode_promo = '" + kode.SelectedValue.ToString() + "'";
                    conn.Close();
                    conn.Open();
                    int potPromo = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    conn.Close();
                    if (potPromo > (Convert.ToInt32(jumlah.Content)-(diskon*Convert.ToInt32(jumlah.Content)/100)))
                    {
                        MessageBox.Show("Potongan Promo melebihi total belanja!");
                        kode.SelectedIndex = -1;
                        promo = 0;
                        updateHarga();
                    }
                    else
                    {
                        promo = potPromo;
                        updateHarga();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select nvl(m.id_member,'-') as \"id\", nvl(m.jenis_member,'-') as \"member\", nvl(m.diskon_pembelian,0) as \"diskon\" " +
                "from member m " +
                "right join penjualan_member p on m.id_member = p.id_member " +
                "right join customer c on p.id_customer = c.id_customer " +
                "where c.id_customer = :id and status = 1";
            cmd.Parameters.Add(":id", customer.SelectedValue);
            OracleDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();
            namaMember = "-";
            idMember = "";
            diskon = 0;
            while (reader.Read())
            {
                namaMember = reader.GetValue(1).ToString();
                idMember = reader.GetValue(0).ToString();
                diskon = Convert.ToInt32(reader.GetValue(2).ToString());
            }
            reader.Close();
            conn.Close();
            if (namaMember.Equals("-")) member.Content = "-";
            else member.Content = namaMember + " (Diskon "+diskon+"%)";
            updateHarga();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dt.Rows.Clear();
            jumlah.Content = "0";
            subtotal.Content = "0";
            kolom3();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            //produsen
            uni = new OracleDataAdapter("select nama_produsen as \"nama\", id_produsen as id from produsen order by 2", conn);
            produsens = new DataTable();
            uni.Fill(produsens);

            DataRow newRow = produsens.NewRow();
            newRow[0] = "All";
            newRow[1] = "0";
            produsens.Rows.InsertAt(newRow, 0);

            produsen.ItemsSource = produsens.DefaultView;
            produsen.DisplayMemberPath = produsens.Columns["nama"].ToString();
            produsen.SelectedValuePath = "ID";

            //jenis alat musik
            uni = new OracleDataAdapter("select nama_jenis as \"nama\", id_jenis as id from jenis_alat_musik order by 2", conn);
            jeniss = new DataTable();
            uni.Fill(jeniss);

            DataRow newRow2 = jeniss.NewRow();
            newRow2[0] = "All";
            newRow2[1] = "0";
            jeniss.Rows.InsertAt(newRow2, 0);

            jenis.ItemsSource = jeniss.DefaultView;
            jenis.DisplayMemberPath = jeniss.Columns["nama"].ToString();
            jenis.SelectedValuePath = "ID";

            //customer
            uni = new OracleDataAdapter("select nama_customer as \"nama\", id_customer as id from customer order by 2", conn);
            customers = new DataTable();
            uni.Fill(customers);

            customer.ItemsSource = customers.DefaultView;
            customer.DisplayMemberPath = customers.Columns["nama"].ToString();
            customer.SelectedValuePath = "ID";

            //promo
            uni = new OracleDataAdapter("select nama_promo||'-'||besar_potongan as \"nama\", kode_promo as id from promo order by 2", conn);
            promos = new DataTable();
            uni.Fill(promos);

            kode.ItemsSource = promos.DefaultView;
            kode.DisplayMemberPath = promos.Columns["nama"].ToString();
            kode.SelectedValuePath = "ID";
            reset();
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            string where = " and upper(a.nama_alat_musik) like '%" + keywords.Text.ToUpper() + "%'";
            if (jenis.SelectedIndex > 0) where += " and a.id_jenis = '" + jenis.SelectedValue.ToString() + "'";
            if (produsen.SelectedIndex > 0) where += " and a.id_produsen like '%" + produsen.SelectedValue.ToString() + "%'";

            try
            {
                daam = new OracleDataAdapter("select id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                   "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                   "from alat_musik a, jenis_alat_musik j, produsen p " +
                   "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen and a.stok>0 " + where +
                   " order by 1", conn);
                dam = new DataTable();
                daam.Fill(dam);
                dgvMusik.ItemsSource = dam.DefaultView;
                kolom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Qty_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = qty.SelectionStart;
            string kata2 = katabaru(qty.Text);
            qty.Text = kata2;
            qty.SelectionStart = temp;
        }

        private void Qty2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = qty2.SelectionStart;
            string kata2 = katabaru(qty2.Text);
            qty2.Text = kata2;
            qty2.SelectionStart = temp;
        }

        private void ResetM_Click(object sender, RoutedEventArgs e)
        {
            daam = new OracleDataAdapter("select id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                   "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                   "from alat_musik a, jenis_alat_musik j, produsen p " +
                   "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen and a.stok>0 " +
                   "order by 1", conn);
            dam = new DataTable();
            daam.Fill(dam);
            dgvMusik.ItemsSource = dam.DefaultView;
            qty.Text = "";
            dgvMusik.SelectedIndex = -1;
            keywords.Text = "";
            jenis.SelectedIndex = 0;
            produsen.SelectedIndex = 0;
            kolom();
        }

        private void ResetA_Click(object sender, RoutedEventArgs e)
        {
            daak = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                "from aksesoris where stok>0 order by 1", conn);
            builder = new OracleCommandBuilder(daak);
            dak = new DataTable();
            daak.Fill(dak);
            dgvAksesoris.ItemsSource = dak.DefaultView;
            qty2.Text = "";
            dgvAksesoris.SelectedIndex = -1;
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;
            keyword.Text = "";
            kolom2();
        }

    }
}
