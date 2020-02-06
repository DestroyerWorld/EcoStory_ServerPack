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
    [Weight(0)]                                          
    public partial class EcoylentItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Ecoylent"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A complete meal replacement solution."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 500, Fat = 500, Protein = 500, Vitamins = 500};
        public override float Calories                          { get { return 1500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}