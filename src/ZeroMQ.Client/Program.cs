using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

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

                while (true)
                {
                    System.Console.WriteLine("Please enter your message:");
                    string message = System.Console.ReadLine();
                    clientSocket.Send(message);

                    string answer = clientSocket.ReceiveString();

                    System.Console.WriteLine("Answer from server: {0}", answer);

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }
    }
}
