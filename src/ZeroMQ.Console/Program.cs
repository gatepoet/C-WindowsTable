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
			if(args.Length == 0) {
				throw new ArgumentException("missing ipaddress parameter");
			}

            using (NetMQContext context = NetMQContext.Create())
            {
                Task serverTask = Task.Factory.StartNew(() => Server(context,args[0]));
                Task.WaitAll(serverTask);
            }
        }

		const int ResponsePort = 5557;

        static void Server(NetMQContext context, string ipaddress)
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
                    
					serverSocket.Send(string.Format("{0}:{1}", ipaddress,ResponsePort));

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

    }
}
