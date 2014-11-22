using NetMQ;

namespace ZeroMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                new DealerClient().Start(context);
            }
        }
    }
}
