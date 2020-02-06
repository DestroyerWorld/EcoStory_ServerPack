namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.SearchAndSelect;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Weight(50)]  
    [StartsDiscovered]
    public partial class FireweedSeedItem : SeedItem
    {
        static FireweedSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Fireweed Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow fireweed."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Fireweed"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class FireweedSeedPackItem : SeedPackItem
    {
        static FireweedSeedPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Fireweed Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow fireweed."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Fireweed"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 0)]    
    public class FireweedSeedRecipe : Recipe
    {
        public FireweedSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FireweedSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FireweedShootsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FireweedSeedRecipe), Item.Get<FireweedSeedItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Fireweed Seed"), typeof(FireweedSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}