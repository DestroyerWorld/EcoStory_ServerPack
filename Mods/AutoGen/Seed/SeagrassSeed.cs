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
    public partial class SeagrassSeedItem : SeedItem
    {
        static SeagrassSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Seagrass Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow seagrass."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Seagrass"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class SeagrassSeedPackItem : SeedPackItem
    {
        static SeagrassSeedPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Seagrass Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow seagrass."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Seagrass"); } }
    }

}