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
    [Yield(typeof(CamasBulbItem), typeof(GatheringSkill), new float[] { 1f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f })]  
    [Crop]  
    [Weight(50)]  
    [StartsDiscovered]
    public partial class CamasBulbItem : SeedItem
    {
        static CamasBulbItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 1, Fat = 5, Protein = 2, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Camas Bulb"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a camas plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Camas"); } }

        public override float Calories { get { return 120; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class CamasBulbPackItem : SeedPackItem
    {
        static CamasBulbPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Camas Bulb Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a camas plant."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Camas"); } }
    }

}