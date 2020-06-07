using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(MemoryGameService)))
            {
                host.Open();
                Console.WriteLine("Server odstartovany");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
