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
                Task listenTask = Task.Factory.StartNew(() => ListenToReplies(context));
                Task broadcastTask = Task.Factory.StartNew(() => BroadcastAddress(context));
                Task.WaitAll(listenTask, broadcastTask);
            }
        }


        static void ListenToReplies(NetMQContext context)
        {
            using (NetMQSocket replySocket = context.CreateResponseSocket())
            {
                replySocket.Bind("tcp://*:5556");

                while (true)
                {
                    var read = replySocket.ReceiveString();
                    Console.WriteLine(read);
                }
            }
        }

        static void BroadcastAddress(NetMQContext context)
        {
            using (NetMQSocket pubSocket = context.CreatePublisherSocket())
            {
                for (int i = 1; i < 255; i++)
                {
                    pubSocket.Connect(string.Format("tcp://192.168.1.{0}:5555", i));
                }

                pubSocket.Send("John 192.168.1.4:5556");
            }
        }
    }
}
