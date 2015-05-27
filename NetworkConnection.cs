using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetClient
{
    public class NetworkConnection
    {
        private NetworkStream ns;
        private Socket client;

        private int port;
        private IPAddress ipAddress;
        private string host = "localhost";
        private static int DEFAULT_PORT = 4444;

        public NetworkConnection()
            : this(DEFAULT_PORT)
        { }

        public NetworkConnection(int port)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(host);
            ipAddress = ipHostInfo.AddressList[1];
            //ipAddress = new IPAddress(new byte[] { 192, 168, 153, 94 });
            this.port = port;
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public void OpenStreamChanel(byte[] bytes, int lenght)
        {
            NetworkStream ns;
            try
            {
                client.Connect(ipAddress, port);
                ns = new NetworkStream(client, true);
                if (ns.CanWrite)
                {
                    ns.Write(bytes, 0, lenght);
                }

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (ArgumentNullException) { throw; }
            catch (SocketException) { throw; }
            catch (Exception) { throw; }
        }

        public BaseCommands TransmitCommand(BaseCommands command)
        {
            byte[] bytes;
            BaseCommands command_return;
            try
            {
                client.Connect(ipAddress, port);
                ns = new NetworkStream(client);
                bytes = command.ToByteArray();
                ns.Write(bytes, 0, bytes.Length);

                command_return = BaseCommands.ParseFrom(ns);        // Очень узкое место, если что, то тут будет висеть вечно
               // command_return.SetType(command.Type);
               // command_return.SetDeviceState(devReturn);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
                return command_return;
            }
            catch (ArgumentNullException) { throw; }
            catch (SocketException) { throw; }
            catch (Exception) { throw; }

        }

    }
}
