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

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for Menu_Trans.xaml
    /// </summary>
    public partial class Menu_Trans : Window
    {
        public Menu_Trans()
        {
            InitializeComponent();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login log = new Login();
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

        private void Btn_trans_beli_Click(object sender, RoutedEventArgs e)
        {
            TransBeli tb = new TransBeli();
            this.Close();
            tb.Show();
        }

        private void Btn_trans_jual_Click(object sender, RoutedEventArgs e)
        {
            TransJual tj = new TransJual();
            this.Close();
            tj.Show();
        }
    }
}
