using System;
using System.Linq;
using CommandSystem;
using LabApi.Features.Wrappers;

namespace lawsCity.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Laws : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null || player.IsHost || player.IsNpc)
            {
                response = "Игрок не найден!";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "Использование: .laws <add 'закон'>|<clear>|<list>";
                return false;
            }

            if (arguments.At(0).ToLower() == "list")
            {
                response = Main.config.LawsTop.Replace("{Laws}", Main.config.Laws);
                return true;
            }
            
            if (player.UserId != Main.config.MayorId)
            {
                response = "Вы не являетесь мэром!";
                return false;
            }

            if (arguments.At(0).ToLower() == "clear")
            {
                Main.config.LawsNumber = 0;
                Main.config.Laws = null;
                response = "Законы очищены";
                return true;
            }

            if (arguments.At(0).ToLower() == "add")
            {
                if (arguments.Count == 1)
                {
                    response = "Использование: .laws <add 'закон'>|<clear>|<list>";
                    return false;
                }
                
                Main.config.LawsNumber += 1;
                string law = string.Join(" ", arguments.Skip(1));
                Main.config.Laws += Main.config.LawsList
                    .Replace("{LawNumber}", Main.config.LawsNumber.ToString())
                    .Replace("{Law}", law);
                foreach (var p in Player.List)
                {
                    p.SendBroadcast(Main.config.NewLaw, 5);
                }

                response = $"Закон №{Main.config.LawsNumber} принят!\n";
                return true;
            }

            response = "Использование: .laws <add 'закон'>|<clear>|<list>";
            return false;
        }
        

        public string Command { get; } = "laws";
        public string[] Aliases { get; } = new string[] { };
        public string Description { get; } = "Законы города. Использование: .laws <add 'закон'>|<clear>|<list>";
    }
}