using NetMQ;

namespace ZeroMQ.Server
{
    internal class DealerServer
    {
        public void Start(NetMQContext context)
        {
            using (var serverSocket = context.CreateDealerSocket())
            {
                serverSocket.Bind("tcp://*:5556");
                serverSocket.Send("My Secret");

                while (true)
                {
                    serverSocket.Send("Too late!");
                }
            }
        }
    }
}