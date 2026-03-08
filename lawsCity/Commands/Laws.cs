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
                response = "Player not found!";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "Использование: .laws <add 'закон'>|<clear>|<list>";
                return false;
            }

            if (arguments.At(0) == "list")
            {
                response = $"\n\t<b><size=30><color=#F4A900>ЗАКОНЫ</color></size></b>\n\n{Main.config.Laws}";
                return true;
            }
            
            if (player.PlayerId != Main.config.MayorId)
            {
                response = "Вы не являетесь мэром!";
                return false;
            }

            if (arguments.At(0) == "clear")
            {
                Main.config.LawsNumber = 0;
                Main.config.Laws = null;
                response = "Законы очищены";
                return true;
            }

            if (arguments.At(0) == "add")
            {
                if (arguments.Count == 1)
                {
                    response = "Использование: .laws <add 'закон'>|<clear>|<list>";
                    return false;
                }
                
                Main.config.LawsNumber += 1;
            
                string law1 = string.Join(" ", arguments.Skip(1));
                Main.config.Laws += $"<b><size=30><color=#FFCF40>{Main.config.LawsNumber}.</color> <color=#FADFAD>{law1}</color></size></b>\n";
                foreach (var p in Player.List)
                {
                    p.SendBroadcast(Main.config.NewLaw, 5);
                }

                response = $"Новый {Main.config.LawsNumber} закон - '{law1}' принят!\n";
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