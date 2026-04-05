using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace lawsCity
{
    public class Main : Plugin<Config>
    {
        public static Config config;
        
        public override void Enable()
        {
            config = new Config();
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers += OnWaitingForPlayers;
        }

        public override void Disable()
        {
            config = null;
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers -= OnWaitingForPlayers;
        }

        private void OnWaitingForPlayers()
        {
            config.MayorId = null;
            config.LawsNumber = 0;
            config.Laws = null;
        }
        

        public override string Name { get; } = "lawsCity";
        public override string Description { get; } = "lawsCity plugin";
        public override string Author { get; } = "AgTeam";
        public override Version Version { get; } = new Version(1, 2, 0);
        public override Version RequiredApiVersion { get; } = LabApiProperties.CurrentVersion;
    }
}