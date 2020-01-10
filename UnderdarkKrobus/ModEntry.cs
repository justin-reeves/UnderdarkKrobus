using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace UnderdarkSewer
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.Player.Warped += this.OnWarped;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Invoked after a new day starts.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.ApplyChanges();
        }

        /// <summary>Invoked after a new day starts.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnWarped(object sender, WarpedEventArgs e)
        {
            // farmhands in multiplayer may not have Krobus in a synced location, so we need to
            // apply the changes when they warp into the same location.
            if (!Context.IsMainPlayer)
                this.ApplyChanges();
        }

        /// <summary>Apply the changes to Krobus' sprite.</summary>
        private void ApplyChanges()
        {
            NPC krobus = Game1.getCharacterFromName("Krobus");
            if (krobus?.Sprite != null && krobus.Sprite.SpriteHeight != 32)
                krobus.Sprite = new AnimatedSprite("Characters\\Krobus", 0, 16, 32);
        }
    }
}
