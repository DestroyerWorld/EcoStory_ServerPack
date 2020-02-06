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
    public partial class AgaveSeedItem : SeedItem
    {
        static AgaveSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Agave Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an agave plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Agave"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class AgaveSeedPackItem : SeedPackItem
    {
        static AgaveSeedPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Agave Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an agave plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Agave"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 0)]    
    public class AgaveSeedRecipe : Recipe
    {
        public AgaveSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AgaveSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AgaveLeavesItem>(typeof(FarmingSkill), 3, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(AgaveSeedRecipe), Item.Get<AgaveSeedItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Agave Seed"), typeof(AgaveSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}