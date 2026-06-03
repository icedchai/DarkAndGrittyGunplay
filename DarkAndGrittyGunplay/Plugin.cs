using DarkAndGrittyGunplay.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace DarkAndGrittyGunplay;

public class Plugin : Plugin<Config>
{
    /// <inheritdoc/>
    public override string Name => "Dark & Gritty Gunplay";

    /// <inheritdoc/>
    public override string Description => string.Empty;

    /// <inheritdoc/>
    public override string Author => "icedchqi";

    /// <inheritdoc/>
    public override Version Version => new Version(1, 1, 1);

    /// <inheritdoc/>
    public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

    public static Plugin Singleton { get; private set; }

    private EventHandlers PlayerEventHandler { get; set; }

    /// <inheritdoc/>
    public override void Disable()
    {
        PlayerEventHandler.UnsubscribeEvents();

        PlayerEventHandler = null;
        Singleton = null;
    }

    /// <inheritdoc/>
    public override void Enable()
    {
        Singleton = this;
        PlayerEventHandler = new EventHandlers();

        PlayerEventHandler.SubscribeEvents();
    }
}
