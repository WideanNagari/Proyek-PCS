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
        
        //ImageViewer1.Source = new BitmapImage(new Uri(@"Images/KH001.jpg", UriKind.Relative));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //source = dataSource.Text;
            //userId = username.Text;
            //pass = password.Text;

            source = "widean";
            userId = "ifm";
            pass = "219116863";

            try
            {
                conn = new OracleConnection("Data Source = widean; User ID = widean; password = 219116863");
                conn.Open();
                conn.Close();
                Menu menu = new Menu();
                this.Hide();
                menu.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
