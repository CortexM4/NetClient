using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace NetClient
{
    public class StreamSoundProcess
    {

        public static void Stream(string file_name, bool reload)
        {
            int port = SendStreamCommand(reload);

            if (port == -1) /* Если эта функция возвращает -1, то произошла какая-та ошибка на сервере */ 
            {
                throw new System.ApplicationException("Server cannot create socket");
            }

            NetworkConnection nc = new NetworkConnection(port);
            try
            {
                FileStream file = File.OpenRead(file_name);
                byte[] buffer = new byte[file.Length];
                int read;
                read = file.Read(buffer, 0, (int)file.Length);
                nc.OpenStreamChanel(buffer, read);
            }
            catch (Exception) { throw; }

        }

        private static int SendStreamCommand(bool reload)
        {
            NetworkConnection nc = new NetworkConnection();

            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            StreamSound.Builder ss = StreamSound.CreateBuilder();
            ss.Reload = reload;
            ss.Build();

            commands.SetType(BaseCommands.Types.Type.STREAM_SOUND);
            commands.SetStreamSound(ss);

            return_cmd = nc.TransmitCommand(commands.Build());

            return return_cmd.StreamSound.Port;
        }
    }
}
