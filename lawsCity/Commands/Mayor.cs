using System;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;

namespace lawsCity.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Mayor : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.HasPermissions("lawsCity.createMayor"))
            {
                response = "You do not have permission to use this command. (lawsCity.createMayor)";
                return false;
            }
            
            if (arguments.Count < 1)
            {
                if (Main.Config.MayorId == null || Player.Get(Main.Config.MayorId) == null)
                {
                    response = "There is no mayor";
                    return true;
                }
                
                response = $"Mayor: {Player.Get(Main.Config.MayorId)?.Nickname}";
                return true;
            }

            if (!int.TryParse(arguments.At(0), out int id))
            {
                response = "Use: mayor <id>";
                return false;
            }

            Player player = Player.Get(id);
            
            if (player == null || player.IsNpc || player.IsHost)
            {
                response = "Player not found!";
                return false;
            }
            
            if (player.UserId == Main.Config.MayorId)
            {
                player.SendBroadcast(Main.Config.OldMayor, 5);
                Main.Config.MayorId = null;
                response = $"Success! {player.Nickname} not the mayor anymore";
                return true;
            }

            if (Main.Config.MayorId != null)
            {
                Player oldPlayer = Player.Get(Main.Config.MayorId);

                if (oldPlayer != null)
                {
                    oldPlayer.SendBroadcast(Main.Config.OldMayor, 5);
                }

                Main.Config.MayorId = player.UserId;
                player.SendBroadcast(Main.Config.NewMayor, 5);
                response = $"Success! {player.Nickname} became mayor, replacing {oldPlayer?.Nickname}";
                return true;
            }
            
            Main.Config.MayorId = player.UserId;
            
            player.SendBroadcast(Main.Config.NewMayor, 5);
            response = $"Success! {player.Nickname} became mayor";
            return true;
        }

        public string Command { get; } = "mayor";
        public string[] Aliases { get; } = new string[] { };
        public string Description { get; } = "Create a mayor who can write laws, use: mayor <id>";
    }
}