using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections;
using TurakasServiceLibrary;

namespace ConsoleApplication1
{
    class GameServer
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(TurakasServiceLibrary.ServiceInterface),
                                      new Uri("net.tcp://localhost:9000/IGameService")))
            {
                host.Open();
                printHelp();
                bool running = true;
                while (running)
                {
                    string line = Console.ReadLine();
                    switch (line.Trim())
                    {
                        case "ls":
                            listSubscribers();
                            break;

                        case "exit":
                            Console.WriteLine("Server stopped!");
                            running = false;
                            break;

                        default:
                            printHelp();
                            break;
                    }
                }
                host.Close();
            }
        }

        static void listSubscribers()
        {
            List<Subscriber> subscribers = ServiceInterface.getSubscribers();
            lock (subscribers)
            {
                int ctr = 1;
                foreach (Subscriber s in subscribers)
                {
                    Console.WriteLine("{0}. ID: {1}, Nickname: {2}, conn: {3}", ctr++, s.Id, s.nickname, s.callback);
                }
            }
        
        }

        static void printHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  ls      - List subscribers.");
            Console.WriteLine("  exit    - Stop server.");
            Console.WriteLine("  help    - Print this message.");
        }
    }
}
