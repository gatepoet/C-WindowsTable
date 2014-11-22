using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NetMQ;

namespace ZeroMQ.Server
{
    class Program
    {
        private const int MinPort = 5500;
        private const int MaxPort = 5600;

        static void Main(string[] args)
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                Task serverTask = Task.Factory.StartNew(() => Server(context));
                Task.WaitAll(serverTask);
            }
        }

        static void Server(NetMQContext context)
        {
            var port = new Random().Next(MinPort, MaxPort);

            int messageCount = 0;
            var sw = new Stopwatch();
            using (NetMQSocket serverSocket = context.CreateResponseSocket())
            {
                sw.Start();
                serverSocket.Bind("tcp://*:" + port);

                while (true)
                {
                    string message = serverSocket.ReceiveString();
                    
                    messageCount++;

                    System.Console.Clear();
                    System.Console.WriteLine("Latest message {0}", message);
                    System.Console.WriteLine("Message count : {0}", messageCount);                    

                    serverSocket.Send("World");

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }

    }
}
