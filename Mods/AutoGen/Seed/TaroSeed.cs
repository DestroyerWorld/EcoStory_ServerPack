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
    public partial class TaroSeedItem : SeedItem
    {
        static TaroSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Taro Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow taro."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Taro"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class TaroSeedPackItem : SeedPackItem
    {
        static TaroSeedPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Taro Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow taro."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Taro"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]    
    public class TaroSeedRecipe : Recipe
    {
        public TaroSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TaroSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TaroRootItem>(typeof(FarmingSkill), 3, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TaroSeedRecipe), Item.Get<TaroSeedItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Taro Seed"), typeof(TaroSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}