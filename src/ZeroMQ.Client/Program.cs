using System;
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
            using (NetMQSocket replySocket = context.CreateResponseSocket())
            using (NetMQSocket pubSocket = context.CreatePublisherSocket())
            {
                replySocket.Bind("tcp://*:5556");

                for (int i = 0; i < 256; i++)
                {
                    pubSocket.Connect(string.Format("tcp://192.168.1.{0}:5555", i));
                }
                
                
                pubSocket.Send("John 192.168.1.4:5556");

                while (true)
                {
                    var read = replySocket.ReceiveString();
                    Console.WriteLine(read);
                }
            }
        }
    }
}
