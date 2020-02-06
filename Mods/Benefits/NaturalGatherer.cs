namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Items;
    using Eco.Simulation.Agents;

    public partial class NaturalGathererTalent : Talent
    {
        public override void RegisterTalent(User user)
        {
            user.Talentset.OnPlantHarvest += ApplyModifier;
        }

        public ItemStack ApplyModifier(Plant plant, ItemStack item, User user)
        {
            var newQuantity = item.Quantity * (plant.Tended ? 1 : this.Value);
            return new ItemStack(item.Item, (int)newQuantity);
        }
    }
}