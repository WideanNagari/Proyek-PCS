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
    /// Interaction logic for TransBeli.xaml
    /// </summary>
    public partial class TransBeli : Window
    {
        OracleConnection conn;
        DataTable dam, dak, dt;
        DataTable produsens, jeniss, suppliers;
        OracleDataAdapter daam, daak, dat, uni;
        OracleCommandBuilder builder;
        string idKaryawan;
        public TransBeli()
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

        private void Btn_jual_member_Click(object sender, RoutedEventArgs e)
        {
            TransJualMember tjm = new TransJualMember();
            this.Close();
            tjm.Show();
        }

        private void Btn_trans_jual_Click(object sender, RoutedEventArgs e)
        {
            TransJual tj = new TransJual();
            this.Close();
            tj.Show();
        }

        private void Btn_trans_Click(object sender, RoutedEventArgs e)
        {
            Menu_Trans mt = new Menu_Trans();
            this.Close();
            mt.Show();
        }

        private void loadData()
        {
            dgvMusik.Columns.Clear();
            daam = new OracleDataAdapter("select id_alat_musik as \"ID\", a.nama_alat_musik as \"Nama Alat Musik\", j.nama_jenis as \"Jenis\", " +
                "p.nama_produsen as \"Produsen\", a.stok as \"Stok\", a.harga as \"Harga\"  " +
                "from alat_musik a, jenis_alat_musik j, produsen p " +
                "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen " +
                "order by 1", conn);
            builder = new OracleCommandBuilder(daam);
            dam = new DataTable();
            daam.Fill(dam);
            dgvMusik.ItemsSource = dam.DefaultView;
            kolom();

            dgvAksesoris.Columns.Clear();
            daak = new OracleDataAdapter("select id_aksesoris as \"ID\", nama_aksesoris as \"Nama Aksesoris\", " +
                "stok as \"Stok\", harga as \"Harga\", keterangan as \"Keterangan\"  " +
                "from aksesoris order by 1", conn);
            builder = new OracleCommandBuilder(daak);
            dak = new DataTable();
            daak.Fill(dak);
            dgvAksesoris.ItemsSource = dak.DefaultView;
            kolom2();

            dgvTrans.Columns.Clear();
            dat = new OracleDataAdapter("select '' as \"ID\", '' as \"Nama Barang\", " +
                "'' as \"Quantity\", '' as \"Harga\", '' as \"Jenis Barang\" from dual where 1=2", conn);
            builder = new OracleCommandBuilder(dat);
            dt = new DataTable();
            dat.Fill(dt);
            dgvTrans.ItemsSource = dt.DefaultView;
            kolom3();
        }

        private void reset()
        {
            produsen.SelectedIndex = 0;
            jenis.SelectedIndex = 0;
            supplier.SelectedIndex = -1;

            keyword.Text = "";
            keywords.Text = "";
            qty.Text = "";
            harga.Text = "";
            qty2.Text = "";
            harga2.Text = "";
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;
            
            subtotal.Content = "0";
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

        private void updateHarga()
        {
            int total = 0;
            string temp, h;
            foreach (DataRow row in dt.Rows)
            {
                temp = "";
                h = row["harga"].ToString();
                for (int i = 0; i < h.Length; i++)
                {
                    if (Char.IsDigit(h[i])) temp += h[i];
                }
                total += Convert.ToInt32(temp) * Convert.ToInt32(row["Quantity"].ToString());
            }
            subtotal.Content = "" + total;
        }

        private void AddM_Click(object sender, RoutedEventArgs e)
        {
            if (qty.Text.Equals("")) MessageBox.Show("Mohon Masukkan Jumlah Barang yang Mau DiRestock!");
            else if (dgvMusik.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Barang yang Mau DiRestock!");
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
                if (Convert.ToInt32(qty.Text)>0)
                {
                    if (Convert.ToInt32(harga.Text) > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = dam.Rows[dgvMusik.SelectedIndex][0].ToString();
                        dr[1] = dam.Rows[dgvMusik.SelectedIndex][1].ToString();
                        dr[2] = Convert.ToInt32(qty.Text) + qtyy;
                        dr[3] = String.Format("Rp. {0:N0}", Convert.ToInt32(harga.Text));
                        dr[4] = "Alat Musik";
                        if (pos != -1) dt.Rows[pos][2] = Convert.ToInt32(qty.Text) + qtyy;
                        else dt.Rows.Add(dr);
                    } else MessageBox.Show("Harga Barang Harus diatas 0!");
                }
                else MessageBox.Show("Quantity Barang Harus diatas 0!");
                qty.Text = "";
                harga.Text = "";
            }
            dgvMusik.SelectedIndex = -1;
            updateHarga();
        }

        private void AddA_Click(object sender, RoutedEventArgs e)
        {
            if (qty2.Text.Equals("")) MessageBox.Show("Mohon Masukkan Jumlah Barang yang Mau DiRestock!");
            else if (dgvAksesoris.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Barang yang Mau DiRestock!");
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
                if (Convert.ToInt32(qty2.Text)>0)
                {
                    if (Convert.ToInt32(harga2.Text) > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = dak.Rows[dgvAksesoris.SelectedIndex][0].ToString();
                        dr[1] = dak.Rows[dgvAksesoris.SelectedIndex][1].ToString();
                        dr[2] = Convert.ToInt32(qty2.Text) + qtyy;
                        dr[3] = string.Format("Rp. {0:N0}", Convert.ToInt32(harga2.Text));
                        dr[4] = "Aksesoris";
                        if (pos != -1) dt.Rows[pos][2] = Convert.ToInt32(qty2.Text) + qtyy;
                        else dt.Rows.Add(dr);
                    }
                    else MessageBox.Show("Harga Barang Harus diatas 0!");
                }
                else MessageBox.Show("Quantity Barang Harus diatas 0!");
                qty2.Text = "";
                harga2.Text = "";
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
                    "from aksesoris where " + where +
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
                if (supplier.SelectedIndex != -1)
                {
                    conn.Open();
                    using (OracleTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string sup = supplier.SelectedValue.ToString();

                            OracleCommand cmd = new OracleCommand($"insert into h_beli values('', sysdate,'{idKaryawan}','{sup}','{subtotal.Content}')", conn);
                            cmd.ExecuteNonQuery();
                            string temp, h;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[4].Equals("Alat Musik"))
                                {
                                    temp = "";
                                    h = row[3].ToString();
                                    for (int i = 0; i < h.Length; i++)
                                    {
                                        if (Char.IsDigit(h[i])) temp += h[i];
                                    }

                                    cmd = new OracleCommand($"insert into d_beli values('','{row[0]}',{Convert.ToInt32(temp)},{row[2]})", conn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[4].Equals("Aksesoris"))
                                {
                                    temp = "";
                                    h = row[3].ToString();
                                    for (int i = 0; i < h.Length; i++)
                                    {
                                        if (Char.IsDigit(h[i])) temp += h[i];
                                    }
                                    cmd = new OracleCommand($"insert into d_beli_aksesoris values('','{row[0]}',{Convert.ToInt32(temp)},{row[2]})", conn);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            daam.Update(dam);
                            daak.Update(dak);
                            loadData();

                            trans.Commit();
                            conn.Close();
                            MessageBox.Show("Pembelian Berhasil!");
                            dt.Rows.Clear();

                            cmd = new OracleCommand("select max(nota_beli) from h_beli", conn);
                            conn.Open();
                            string noNota = cmd.ExecuteScalar().ToString();
                            conn.Close();
                            Nota n = new Nota("Nota Pembelian", this, noNota);
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
                else MessageBox.Show("Pilih Supplier Terlebih Dahulu!");
            }
            else MessageBox.Show("Keranjang Belanja Kosong!");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dt.Rows.Clear();
            subtotal.Content = "0";
            kolom3();
        }

        private void Harga_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = harga.SelectionStart;
            string kata2 = katabaru(harga.Text);
            harga.Text = kata2;
            harga.SelectionStart = temp;
        }

        private void Harga2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp = harga2.SelectionStart;
            string kata2 = katabaru(harga2.Text);
            harga2.Text = kata2;
            harga2.SelectionStart = temp;
        }

        private void DgvMusik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvMusik.SelectedIndex!=-1) harga.Text = dam.Rows[dgvMusik.SelectedIndex][5].ToString();
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



        private void DgvAksesoris_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvAksesoris.SelectedIndex != -1) harga2.Text = dak.Rows[dgvAksesoris.SelectedIndex][3].ToString();
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

            //supplier
            uni = new OracleDataAdapter("select nama_supplier as \"nama\", id_supplier as id from supplier order by 2", conn);
            suppliers = new DataTable();
            uni.Fill(suppliers);

            supplier.ItemsSource = suppliers.DefaultView;
            supplier.DisplayMemberPath = suppliers.Columns["nama"].ToString();
            supplier.SelectedValuePath = "ID";
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
                   "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen " + where +
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
                   "where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen " +
                   "order by 1", conn);
            dam = new DataTable();
            daam.Fill(dam);
            dgvMusik.ItemsSource = dam.DefaultView;
            qty.Text = "";
            harga.Text = "";
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
                "from aksesoris order by 1", conn);
            builder = new OracleCommandBuilder(daak);
            dak = new DataTable();
            daak.Fill(dak);
            dgvAksesoris.ItemsSource = dak.DefaultView;
            qty2.Text = "";
            harga2.Text = "";
            dgvAksesoris.SelectedIndex = -1;
            rnama.IsChecked = false;
            rketerangan.IsChecked = false;
            keyword.Text = "";
            kolom2();
        }

    }
}
