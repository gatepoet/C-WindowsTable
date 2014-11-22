using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace ZeroMQ.Server
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
                Task publisherTask = Task.Factory.StartNew(() => Publisher(context));
                Task.WaitAll(serverTask, publisherTask);
            }
        }

        static void Publisher(NetMQContext context)
        {
            int messageCount = 0;
            var sw = new Stopwatch();
            using (var serverSocket = context.CreatePublisherSocket())
            {
                sw.Start();
                serverSocket.Bind("tcp://*:5556");

                while (true)
                {
                    //string message = serverSocket.ReceiveString();

                    serverSocket.Send("127.0.0.1:5557");

                    Thread.Sleep(100);


                    //var split = message.Split(' ');
                    //var name = split[0];
                    //var address = split[1];

                    //Console.WriteLine("{0} {1}", name, address);

                    messageCount++;

                    System.Console.Clear();

                    System.Console.WriteLine("Message count : {0}", messageCount);

                    //serverSocket.Send("World");

                    //if (message == "exit")
                    //{
                    //	break;
                    //}
                }
            }
        }
        static void Server(NetMQContext context)
        {
            int messageCount = 0;
            var sw = new Stopwatch();
            using (var serverSocket = context.CreateResponseSocket())
            {
                sw.Start();
                serverSocket.Bind("tcp://*:5557");

                while (true)
                {
                    string message = serverSocket.ReceiveString();
                    Console.WriteLine(message);
                    Console.WriteLine("I was found. Exiting...");
                    Environment.Exit(0);
                }
            }
        }

    }
}
