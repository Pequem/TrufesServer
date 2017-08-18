using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TrufesServer
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Client> clientsList = new ObservableCollection<Client>();
        public MainWindow()
        {
            InitializeComponent();
            Client c = new Client();
            c.name = "test";
            listClient.DataContext = clientsList;
            clientsList.Add(c);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Stop();
        }
    }
}
