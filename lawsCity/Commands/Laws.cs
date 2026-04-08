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
                response = "Use: .laws <add 'law'>|<clear>|<list>";
                return false;
            }

            if (arguments.At(0).ToLower() == "list")
            {
                response = Main.Config.LawsTop.Replace("%Laws%", Main.Config.Laws);
                return true;
            }
            
            if (player.UserId != Main.Config.MayorId)
            {
                response = "You are not the mayor.!";
                return false;
            }

            if (arguments.At(0).ToLower() == "clear")
            {
                Main.Config.LawsNumber = 0;
                Main.Config.Laws = null;
                response = "The laws are cleared";
                return true;
            }

            if (arguments.At(0).ToLower() == "add")
            {
                if (arguments.Count == 1)
                {
                    response = "Use: .laws <add 'law'>|<clear>|<list>";
                    return false;
                }
                
                Main.Config.LawsNumber += 1;
                string law = string.Join(" ", arguments.Skip(1));
                Main.Config.Laws += Main.Config.LawsList
                    .Replace("%LawNumber%", Main.Config.LawsNumber.ToString())
                    .Replace("%Law%", law);
                foreach (var p in Player.List)
                {
                    p.SendBroadcast(Main.Config.NewLaw, 5);
                }

                response = $"Law №{Main.Config.LawsNumber} adopted!\n";
                return true;
            }

            response = "Use: .laws <add 'law'>|<clear>|<list>";
            return false;
        }
        

        public string Command { get; } = "laws";
        public string[] Aliases { get; } = new string[] { };
        public string Description { get; } = "Laws of the city. Use: .laws <add 'law'>|<clear>|<list>";
    }
}