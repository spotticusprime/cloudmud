using CloudMud.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMud.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer(8080);
            server.Start();
        }
    }
}
