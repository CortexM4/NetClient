using System;
using System.Collections.Generic;
using System.Text;

namespace NetClient
{
    public class DeviceStateProcess
    {

        private static int cmd_port = 4444;        // Default port for commands

        private static DeviceState SetDeviceStatePropertys(DeviceState deviceState)
        {
            NetworkConnection nc = new NetworkConnection(cmd_port);

            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            DeviceState ret_dev;
            BaseCommands ret_cmd;

            commands.SetType(BaseCommands.Types.Type.DEVICE_STATE);
            commands.SetDeviceState(deviceState);

            ret_cmd = nc.TransmitCommand(commands.Build());
            ret_dev = ret_cmd.DeviceState;

            return ret_dev;
        }

        private static DeviceState GetDeviceStatePropertys(DeviceState deviceState)
        {
            NetworkConnection nc = new NetworkConnection(cmd_port);

            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            DeviceState ret_dev;
            BaseCommands ret_cmd;

            commands.SetType(BaseCommands.Types.Type.DEVICE_STATE);
            commands.SetDeviceState(deviceState);

            ret_cmd = nc.TransmitCommand(commands.Build());
            ret_dev = ret_cmd.DeviceState;

            return ret_dev;
        }

        public static bool SetVolume(float volume)
        {
            DeviceState.Builder deviceState = DeviceState.CreateBuilder();
            deviceState.SetType(DeviceState.Types.Direction.WRITE);
            deviceState.SetSound(volume);

            DeviceState dev_return = SetDeviceStatePropertys(deviceState.Build());
            if (dev_return.Sound != volume)
            {
                /* Произошла какая-то ошибка, звук не установился */
                return false;
            }

            return true;
        }

        public static float GetVolume()
        {
            DeviceState.Builder deviceState = DeviceState.CreateBuilder();
            deviceState.SetType(DeviceState.Types.Direction.READ);

            DeviceState dev_return = GetDeviceStatePropertys(deviceState.Build());
            return dev_return.Sound;
        }
    }
}
