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
    [Weight(100)]                                          
    public partial class CharredCamasBulbItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Camas Bulb"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A fibrous and sweet treat much like a sweet potato, though slightly blackened over the heat of a campfire."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 2, Fat = 7, Protein = 3, Vitamins = 1};
        public override float Calories                          { get { return 510; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class CharredCamasBulbRecipe : Recipe
    {
        public CharredCamasBulbRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredCamasBulbItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CamasBulbItem>(1)    
            };
            this.CraftMinutes = new ConstantValue(2);     
            this.Initialize(Localizer.DoStr("Charred Camas Bulb"), typeof(CharredCamasBulbRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}