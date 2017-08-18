using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrufesServer
{
    class Message
    {
        public const int sendType = 0;
        public const int responseType = 1;
        public const int KeepAliveId = 0;
        public const int sequenceId = 1;


        public int type { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }
}
