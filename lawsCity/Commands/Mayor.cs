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
                response = "У вас нет прав для использования этой команды. (create.mayor)";
                return false;
            }
            
            if (arguments.Count < 1)
            {
                if (Main.Config.MayorId == null || Player.Get(Main.Config.MayorId) == null)
                {
                    response = "Мэра нету";
                    return true;
                }
                
                response = $"Мэр: {Player.Get(Main.Config.MayorId)?.Nickname}";
                return true;
            }

            if (!int.TryParse(arguments.At(0), out int id))
            {
                response = "Использование: mayor <id>";
                return false;
            }

            Player player = Player.Get(id);
            
            if (player == null || player.IsNpc || player.IsHost)
            {
                response = "Игрок не найден!";
                return false;
            }
            
            if (player.UserId == Main.Config.MayorId)
            {
                player.SendBroadcast(Main.Config.OldMayor, 5);
                Main.Config.MayorId = null;
                response = $"Успешно! {player.Nickname} больше не мэр";
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
                response = $"Успешно! {player.Nickname} стал мэром, заменив {oldPlayer?.Nickname}";
                return true;
            }
            
            Main.Config.MayorId = player.UserId;
            
            player.SendBroadcast(Main.Config.NewMayor, 5);
            response = $"Успешно! {player.Nickname} стал мэром";
            return true;
        }

        public string Command { get; } = "mayor";
        public string[] Aliases { get; } = new string[] { };
        public string Description { get; } = "Создать мэра который сможет писать законы, использование: mayor <id>";
    }
}