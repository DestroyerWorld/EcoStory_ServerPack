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
    [Weight(10)]                                          
    public partial class AgaveLeavesItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Agave Leaves"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr(""); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 4, Fat = 1, Protein = 0, Vitamins = 1};
        public override float Calories                          { get { return 200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}