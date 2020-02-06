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
    [Weight(300)]                                          
    public partial class BasicSaladItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Basic Salad"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A seemingly random assortment of wild plants that form a sort of salad."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 13, Fat = 6, Protein = 6, Vitamins = 13};
        public override float Calories                          { get { return 800; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}