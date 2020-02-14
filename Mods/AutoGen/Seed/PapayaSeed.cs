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
    public partial class PapayaSeedItem : SeedItem
    {
        static PapayaSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Papaya Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a papaya plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Papaya"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class PapayaSeedPackItem : SeedPackItem
    {
        static PapayaSeedPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Papaya Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a papaya plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Papaya"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]    
    public class PapayaSeedRecipe : Recipe
    {
        public PapayaSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PapayaSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PapayaItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PapayaSeedRecipe), Item.Get<PapayaSeedItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Papaya Seed"), typeof(PapayaSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}