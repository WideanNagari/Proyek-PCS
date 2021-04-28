﻿using System;
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
    /// Interaction logic for MasterJenis.xaml
    /// </summary>
    public partial class MasterJenis : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int caricari, inserts;
        public MasterJenis()
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
            cmd.CommandText = "select id_jenis as \"ID\", nama_jenis as \"Nama Jenis\" from jenis_alat_musik order by 1";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dgvJenis.ItemsSource = ds.DefaultView;
            conn.Close();
        }
        private void reset()
        {
            dgvJenis.SelectedIndex = -1;
            insert.IsEnabled = true;
            update.IsEnabled = false;
            delete.IsEnabled = false;
            
            keyword.Text = "";
            id.Text = "";
            nama.Text = "";
            keyword.Text = "";

            id.IsReadOnly = false;
            inserts = 1;
        }
        private void DgvProdusen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvJenis.SelectedIndex != -1)
            {
                inserts = 0;
                insert.IsEnabled = false;
                update.IsEnabled = true;
                delete.IsEnabled = true;
                id.Text = ds.Rows[dgvJenis.SelectedIndex][0].ToString();
                nama.Text = ds.Rows[dgvJenis.SelectedIndex][1].ToString();

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
            try
            {
                ds = new DataTable();
                OracleCommand cmd = new OracleCommand();
                da = new OracleDataAdapter();

                cmd.Connection = conn;
                cmd.CommandText = "select id_jenis as \"ID\", nama_jenis as \"Nama Jenis\" " +
                    "from jenis_alat_musik " +
                    "where upper(nama_jenis) like '%" + keyword.Text.ToUpper() + "%' " +
                    "order by 1";
                conn.Close();
                conn.Open();
                cmd.ExecuteReader();
                da.SelectCommand = cmd;
                da.Fill(ds);
                dgvJenis.ItemsSource = ds.DefaultView;
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
            if (nama.Text.Equals("")) MessageBox.Show("Mohon Isi Field Nama Jenis!");
            else if (id.Text.Equals("")) MessageBox.Show("Mohon Isi Field ID Jenis!");
            else if (id.Text.Length<3) MessageBox.Show("ID Jenis Harus 3 Huruf!");
            else
            {
                bool sukses = true;
                for (int i = 0; i < 3; i++)
                {
                    if (!nama.Text.ToUpper().Contains(id.Text[i])) sukses = false;
                }
                if (sukses)
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();
                        conn.Close();
                        cmd = new OracleCommand("insert into jenis_alat_musik values (:id,:nama)", conn);
                        cmd.Parameters.Add(":id", id.Text);
                        cmd.Parameters.Add(":nama", nama.Text);

                        conn.Close();
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        loadData();
                        reset();
                        MessageBox.Show("Jenis Alat Musik Baru Berhasil Ditambahkan!");
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else MessageBox.Show("Semua Huruf ID Jenis Harus Ada Di Nama Jenis!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (nama.Text.Equals(""))
            {
                MessageBox.Show("Mohon Isi Field Nama Jenis!");
            }
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Close();
                    cmd = new OracleCommand("update jenis_alat_musik set nama_jenis = :nama where id_jenis = :id", conn);
                    cmd.Parameters.Add(":nama", nama.Text);
                    cmd.Parameters.Add(":id", id.Text);

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    reset();
                    MessageBox.Show("Jenis Alat Musik Berhasil diUpdate!");
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
            if (dgvJenis.SelectedIndex == -1) MessageBox.Show("Mohon Pilih Jenis Alat Musik Yang Ingin Dihapus Terlebih Dahulu!");
            else
            {
                OracleCommand cmd = new OracleCommand("delete from d_jual where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = '" + id.Text + "')", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cmd = new OracleCommand("delete from d_beli where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = '" + id.Text + "')", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cmd = new OracleCommand("delete from alat_musik where id_jenis = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                cmd = new OracleCommand("delete from jenis_alat_musik where id_jenis = '" + id.Text + "'", conn);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadData();
                reset();

                MessageBox.Show("Delete Jenis Alat Musik Berhasil!");
            }
        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inserts == 1)
            {
                id.Text = id.Text.ToUpper();
                if (id.Text.Length > 3)
                {
                    string kata = "";
                    for (int i = 0; i < id.Text.Length - 1; i++)
                    {
                        kata += id.Text[i];
                    }
                    id.Text = kata;
                }
                id.SelectionStart = id.Text.Length;
            }
        }
    }
}