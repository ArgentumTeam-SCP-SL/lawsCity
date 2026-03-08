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
            LabApi.Events.Handlers.ServerEvents.RoundEnding += OnRoundEnding;
        }

        public override void Disable()
        {
            config = null;
            LabApi.Events.Handlers.ServerEvents.RoundEnding -= OnRoundEnding;
        }

        private void OnRoundEnding(LabApi.Events.Arguments.ServerEvents.RoundEndingEventArgs ev)
        {
            Config.MayorId = 0;
            Config.Laws = null;
            Config.LawsNumber = 0;
        }
        

        public override string Name { get; } = "lawsCity";
        public override string Description { get; } = "laws city plugin";
        public override string Author { get; } = "AgTeam";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredApiVersion { get; } = LabApiProperties.CurrentVersion;
    }
}