using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrufesServer
{
    class Server
    {
        TcpListener listener;
        List<Task> socs = new List<Task>();
        private const int timeLimit = 10000;
        public void Start(int port)
        {
            listener = new TcpListener(port);
            listener.Start();
            AcceptClient();
        }

        public async Task AcceptClient()
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Task.Run(() => {
                    socs.Add(service(client));
                });
            }
        }

        private async Task service(TcpClient client)
        {
            Console.WriteLine("Task start");
            StreamReader sr = new StreamReader(client.GetStream());
            StreamWriter sw = new StreamWriter(client.GetStream());
            int time = 0;
            Client c = new Client();
            c.name = "test";
            c = App.RegistreClient(c);
            while (client.Connected)
            {
                if (client.Available > 0)
                {
                    string t = "";
                    t = sr.ReadLine();
                    Message m = JsonConvert.DeserializeObject<Message>(t);
                    if (m.type == Message.sendType) {
                        if (m.id == Message.sequenceId)
                        {
                            App.setSequencia(m.message);
                            Message _m = new Message();
                            _m.type = Message.responseType;
                            _m.id = Message.sequenceId;
                            _m.status = true;
                            sw.Write(JsonConvert.SerializeObject(_m) + "\n");
                            sw.Flush();
                        }
                    }
                    time = 0;
                    Console.WriteLine(t);
                }
                if (!client.Connected)
                {
                    break;
                }
                if(time > timeLimit)
                {
                    break;
                }
                time += 100;
                await Task.Delay(100);
            }
            App.RemoveClient(c);
            Console.WriteLine("Task stop");
            client.Close();
        }
    }
}
