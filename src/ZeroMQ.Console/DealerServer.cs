using System;
using NetMQ;

namespace ZeroMQ.Server
{
    internal class DealerServer
    {
        private const int MinPort = 5500;
        private const int MaxPort = 5600;

        public void Start(NetMQContext context)
        {
            using (var serverSocket = context.CreateDealerSocket())
            {
                var port = new Random().Next(MinPort, MaxPort);
                serverSocket.Bind("tcp://*:" + port);
                serverSocket.Send("My Secret");

                while (true)
                {
                    serverSocket.Send("Too late!");
                }
            }
        }
    }
}