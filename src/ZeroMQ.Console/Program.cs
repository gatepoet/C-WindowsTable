using NetMQ;

namespace ZeroMQ.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = NetMQContext.Create())
            {
                new DealerServer().Start(context);
            }
        }
    }
}
