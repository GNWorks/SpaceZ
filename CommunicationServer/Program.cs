using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var _server = new CommunicationService();
            using (var host = new ServiceHost(_server))
            {
                host.Open();
                Console.WriteLine("Server is running .....");
                Console.ReadLine();
            }
        }
    }
}
