using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace ZeroMQ.Console
{
    class Program
    {
        private static int _messageCount;
        private static int MessageReceivedPerSecond;

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
            int messageCount = 0;
            var sw = new Stopwatch();
            using (NetMQSocket serverSocket = context.CreateResponseSocket())
            {
                sw.Start();
                serverSocket.Bind("tcp://*:5556");

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
