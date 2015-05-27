using System;
using System.Collections.Generic;
using System.Text;

namespace NetClient
{
    public class FairyTaleProcess
    {
        private string name;
        public FairyTaleProcess(string name)
        {
            this.name = name;
        }

        public long Play()
        {
            NetworkConnection nc = new NetworkConnection();
            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            FairyTale.Builder ft = FairyTale.CreateBuilder();
            ft.SetCmd(FairyTale.Types.Type.PLAY);
            ft.Name = name;
            ft.Build();

            commands.SetType(BaseCommands.Types.Type.FAIRY_TALE);
            commands.SetFairyTale(ft);

            return_cmd = nc.TransmitCommand(commands.Build());

            return return_cmd.FairyTale.MaxPosition;
        }

        /*
         *      pause/unpause должны ли возвращать флаг успешност?
         *      пока оставлю так, там посмотрим.
         */
        public void Pause()
        {
            NetworkConnection nc = new NetworkConnection();
            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            FairyTale.Builder ft = FairyTale.CreateBuilder();
            ft.SetCmd(FairyTale.Types.Type.PAUSE);
            ft.Name = name;
            ft.Build();

            commands.SetType(BaseCommands.Types.Type.FAIRY_TALE);
            commands.SetFairyTale(ft);

            return_cmd = nc.TransmitCommand(commands.Build());

        }
        public void Unpause()
        {
            NetworkConnection nc = new NetworkConnection();
            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            FairyTale.Builder ft = FairyTale.CreateBuilder();
            ft.SetCmd(FairyTale.Types.Type.UNPAUSE);
            ft.Name = name;
            ft.Build();

            commands.SetType(BaseCommands.Types.Type.FAIRY_TALE);
            commands.SetFairyTale(ft);

            return_cmd = nc.TransmitCommand(commands.Build());

        }

        public long GetPosition()
        {
            NetworkConnection nc = new NetworkConnection();
            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            FairyTale.Builder ft = FairyTale.CreateBuilder();
            ft.SetCmd(FairyTale.Types.Type.GET_POSITION);
            ft.Name = name;
            ft.Build();

            commands.SetType(BaseCommands.Types.Type.FAIRY_TALE);
            commands.SetFairyTale(ft);

            return_cmd = nc.TransmitCommand(commands.Build());

            return return_cmd.FairyTale.Position;
        }

        public void SetPosition(int position)
        {
            NetworkConnection nc = new NetworkConnection();
            BaseCommands.Builder commands = BaseCommands.CreateBuilder();
            BaseCommands return_cmd;
            FairyTale.Builder ft = FairyTale.CreateBuilder();
            ft.SetCmd(FairyTale.Types.Type.SET_POSITION);
            ft.Position = position * 1000000;
            ft.Name = name;
            ft.Build();

            commands.SetType(BaseCommands.Types.Type.FAIRY_TALE);
            commands.SetFairyTale(ft);

            return_cmd = nc.TransmitCommand(commands.Build());
        }


    }
}
