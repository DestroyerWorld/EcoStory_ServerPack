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
    public partial class FruitSaladItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Fruit Salad"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("While tomatoes are fruits, you don't usually put them in fruit salads."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 3, Protein = 4, Vitamins = 19};
        public override float Calories                          { get { return 900; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}