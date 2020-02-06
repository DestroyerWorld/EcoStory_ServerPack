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
    [Weight(8)]                                          
    [Yield(typeof(TomatoItem), typeof(GatheringSkill), new float[] {1f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f})][Tag("Crop")]      
    [Crop]                                                      
    public partial class TomatoItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Tomato"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Intelligence is knowing this is a fruit; wisdom is not putting it in a fruit salad."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 4, Fat = 0, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 240; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}