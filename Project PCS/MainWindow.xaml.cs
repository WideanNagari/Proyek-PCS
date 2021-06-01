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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.DataAccess.Client;

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static OracleConnection conn;
        public static String source, userId, pass;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Show_Checked(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Hidden;
            txtPass.Visibility = Visibility.Visible;
            txtPass.Text = password.Password.ToString();
        }

        private void Show_Unchecked(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Visible;
            txtPass.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtPass.Visibility = Visibility.Hidden;
            source = "widean";
            userId = "widean";
            pass = "219116863";
            try
            {
                conn = new OracleConnection("Data Source = " + source + "; User ID = " + userId + "; password = " + pass);
                conn.Open(); conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass.Visibility == Visibility.Visible) password.Password = txtPass.Text;
            if (txtId.Text.Equals("") || password.Password.ToString().Equals("")) MessageBox.Show("Mohon isi ID dan Password dengan lengkap!");
            else if (txtId.Text.ToUpper().Equals("ADMIN") && password.Password.ToString().ToUpper().Equals("ADMIN"))
            {
                Menu m = new Menu();
                this.Close();
                m.Show();
            }
            else {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select Nama_Karyawan from Karyawan where id_karyawan = :id and password = :pass";
                    cmd.Parameters.Add(":id", txtId.Text.ToUpper());
                    cmd.Parameters.Add(":pass", password.Password.ToString());
                    conn.Close();
                    conn.Open();
                    string nama = cmd.ExecuteScalar().ToString();
                    conn.Close();

                    Menu_Trans mt = new Menu_Trans(nama,txtId.Text.ToUpper());
                    this.Close();
                    mt.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data tidak valid! mohon isi ID dan Password dengan sesuai!");
                }
            }
        }
    }
}
