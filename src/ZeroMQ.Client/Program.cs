using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using System.Diagnostics;

namespace ZeroMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                Task clientTask = Task.Factory.StartNew(() => Client(context));
                Task.WaitAll(clientTask);
            }
        }


        static void Client(NetMQContext context)
        {
            using (NetMQSocket clientSocket = context.CreateRequestSocket())
            {
                clientSocket.Connect("tcp://127.0.0.1:5555");

				var count = 0;

				var stopwatch = new Stopwatch();
				stopwatch.Start();

                while (true)
                {
					//System.Console.WriteLine("Please enter your message:");
					//string message = System.Console.ReadLine();
                    clientSocket.Send("hello");

					var read = clientSocket.ReceiveString();

					//System.Console.WriteLine("Answer from server: {0}", answer);

					count++;

					if(count%100==0) {
						Console.WriteLine("running at {0} msg/sec", count/stopwatch.Elapsed.TotalSeconds);
					}
                }
            }
        }
    }
}
