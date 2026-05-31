
using DarkAndGrittyGunplay.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace DarkAndGrittyGunplay
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Dark & Gritty Gunplay";

        public override string Description => "";

        public override string Author => "icedchqi";

        public override Version Version => new Version(0, 0, 1);

        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

        public static Plugin Singleton { get; private set; }

        private EventHandlers PlayerEventHandler { get; set; }

        public override void Disable()
        {
            PlayerEventHandler.UnsubscribeEvents();

            PlayerEventHandler = null;
            Singleton = null;
        }

        public override void Enable()
        {
            Singleton = this;
            PlayerEventHandler = new EventHandlers();


            PlayerEventHandler.SubscribeEvents();
        }
    }
}
