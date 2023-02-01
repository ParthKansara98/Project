using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Scheduller_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of a channel
            TcpChannel channel = new TcpChannel(8189);
            ChannelServices.RegisterChannel(channel, false);

            // Register as an available service with the name HelloWorld
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Scheduller_Server.Remote_Sheduller),
                "RemoteScheduller",
                WellKnownObjectMode.Singleton);

            System.Console.WriteLine("Press the enter key to exit...");
            System.Console.ReadLine();
            channel = null;
        }
    }
}
