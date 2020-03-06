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
    public partial class CookeinaMushroomSporesItem : SeedItem
    {
        static CookeinaMushroomSporesItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Cookeina Mushroom Spores"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow cookeina mushrooms."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("CookeinaMushroom"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class CookeinaMushroomSporesPackItem : SeedPackItem
    {
        static CookeinaMushroomSporesPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Cookeina Mushroom Spores Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow cookeina mushrooms."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("CookeinaMushroom"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]    
    public class CookeinaMushroomSporesRecipe : Recipe
    {
        public CookeinaMushroomSporesRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CookeinaMushroomSporesItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CookeinaMushroomsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent))   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CookeinaMushroomSporesRecipe), Item.Get<CookeinaMushroomSporesItem>().UILink(), 1, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Cookeina Mushroom Spores"), typeof(CookeinaMushroomSporesRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}