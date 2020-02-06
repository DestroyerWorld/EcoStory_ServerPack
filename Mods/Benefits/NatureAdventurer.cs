namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Players;
    using Eco.Simulation.WorldLayers;


    public partial class NatureAdventurerTalent : Talent
    {
        public override bool HasActiveRequirements { get { return true; } }
        public override bool Active(object obj, User user = null)
        {
            var playerActivity = WorldLayerManager.GetLayer(LayerNames.PlayerActivity);
            if(playerActivity.EntryWorldPos(user.Position.XZi) < 0.2)
                return true;
            return false;
        }
    }
}