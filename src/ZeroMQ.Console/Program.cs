using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

namespace ZeroMQ.Console
{
    class Program
    {
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
            using (NetMQSocket serverSocket = context.CreateResponseSocket())
            {
                serverSocket.Bind("tcp://*:5555");

                while (true)
                {
                    string message = serverSocket.ReceiveString();

                    System.Console.WriteLine("Receive message {0}", message);

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
