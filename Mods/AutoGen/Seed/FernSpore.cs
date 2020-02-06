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
    public partial class FernSporeItem : SeedItem
    {
        static FernSporeItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Fern Spore"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow ferns."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Fern"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class FernSporePackItem : SeedPackItem
    {
        static FernSporePackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Fern Spore Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow ferns."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Fern"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 0)]    
    public class FernSporeRecipe : Recipe
    {
        public FernSporeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FernSporeItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiddleheadsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FernSporeRecipe), Item.Get<FernSporeItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Fern Spore"), typeof(FernSporeRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}