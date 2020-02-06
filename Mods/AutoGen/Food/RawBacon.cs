namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Gameplay.Objects;

    [Serialized]
    [Weight(50)]                                          
    public partial class RawBaconItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Raw Bacon"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Raw Bacon"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A fatty cut of meat that happens to be inexplicably tastier than other cuts."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 9, Protein = 3, Vitamins = 0};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(ButcherySkill), 2)]    
    public partial class RawBaconRecipe : Recipe
    {
        public RawBaconRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RawBaconItem>(),
               
               new CraftingElement<ScrapMeatItem>(5) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(ButcherySkill), 20, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RawBaconRecipe), Item.Get<RawBaconItem>().UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Raw Bacon"), typeof(RawBaconRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}