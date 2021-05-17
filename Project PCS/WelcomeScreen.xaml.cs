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
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Window, INotifyPropertyChanged
    {
        private BackgroundWorker bgwork = new BackgroundWorker();
        private int _workerstate;

        public event PropertyChangedEventHandler PropertyChanged;

        public int WorkerState
        {
            get { return _workerstate; }
            set
            {
                _workerstate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
                }
            }
        }

        public WelcomeScreen()
        {
            InitializeComponent();
            DataContext = this;            
            bgwork.DoWork += (s, e) => 
            {
                for (int i = 0; i < 30; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    WorkerState = i;
                }
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Login l = new Login();
                    this.Close();
                    l.ShowDialog();
                    
                });
               
            };
            

            bgwork.RunWorkerAsync();

        }

    }
}
