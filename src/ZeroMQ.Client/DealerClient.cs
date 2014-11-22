using System;
using NetMQ;

namespace ZeroMQ.Client
{
    internal class DealerClient
    {
        public void Start(NetMQContext context)
        {
            using (var subSocket = context.CreateDealerSocket())
            {
                for (int i = 1; i < 255; i++)
                {
                    subSocket.Connect(string.Format("tcp://192.168.1.{0}:5556", i));
                }

                while (true)
                {
                    var message = subSocket.ReceiveString();
                    if(!message.StartsWith("Too"))
                        Console.WriteLine(message);
                }
            }
        }
    }
}