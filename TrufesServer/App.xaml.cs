using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TrufesServer
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<int[]> sequencias = new List<int[]>();
        private static Task troll1;
        private static Task troll2;
        private static int idCount = 0;
        static MainWindow mw;

        public App()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            ServerStart();
        }

        public static void Stop()
        {
            Application.Current.Shutdown();
        }

        public static void ServerStart()
        {
            mw = new MainWindow();
            Server s = new Server();
            troll1 = Task.Run(() =>
            {
                s.Start(1234);
            });
            troll2 = Task.Run(() =>
            {
                s.Start(1235);
            });
        }

        public static Client RegistreClient(Client c)
        {
            c.id = idCount++;
            mw.listClient.Items.Add(c);
            mw.listClient.Items.Refresh();
            //mw.update();
            return c;
        }

        public static void RemoveClient(Client c)
        {
            mw.clientsList.Remove(c);
            //mw.update();
        }

        public static void setSequencia(string s)
        {
            int[] sequencia = new int[5];
            string[] _s = s.Split('-');
            for (int i = 0; i < 5; i++)
            {
                sequencia[i] = int.Parse(_s[i]);
            }
            sequencias.Add(sequencia);
        }
    }
}
