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
                /* Task findTask = Task.Factory.StartNew(() => FindServers(context));
                 Task.WaitAll(findTask);*/
                FindServers(context);
            }
        }


        static void FindServers(NetMQContext context)
        {
            using (var subSocket = context.CreateSubscriberSocket())
            {
                subSocket.Options.SendTimeout = TimeSpan.FromMilliseconds(200);
                for (int i = 1; i < 6; i++)
                {
                    subSocket.Connect(string.Format("tcp://192.168.1.{0}:5556", i));
                    subSocket.Subscribe("");
                }

                using (var foundSocket = context.CreateRequestSocket())
                {
                    while (true)
                    {
                        var read = subSocket.ReceiveString();

                        Console.WriteLine(read);

                        foundSocket.Connect(string.Format("tcp://{0}", read));
                        foundSocket.Send(string.Format("I [John] found you [{0}].", read));
                    }

                }
            }
        }
    }
}
