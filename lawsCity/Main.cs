using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace lawsCity
{
    public class Main : Plugin<Config>
    {
        public new static Config Config;
        
        public override void Enable()
        {
            Config = new Config();
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers += OnWaitingForPlayers;
        }

        public override void Disable()
        {
            Config = null;
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers -= OnWaitingForPlayers;
        }

        private void OnWaitingForPlayers()
        {
            Config.MayorId = null;
            Config.LawsNumber = 0;
            Config.Laws = null;
        }
        

        public override string Name { get; } = "lawsCity";
        public override string Description { get; } = "lawsCity plugin";
        public override string Author { get; } = "AgTeam";
        public override Version Version { get; } = new Version(1, 2, 2);
        public override Version RequiredApiVersion { get; } = LabApiProperties.CurrentVersion;
    }
}