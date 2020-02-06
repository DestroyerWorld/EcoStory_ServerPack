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
    public partial class AcornItem : SeedItem
    {
        static AcornItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Acorn"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Oak"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class AcornPackItem : SeedPackItem
    {
        static AcornPackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Acorn Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Oak"); } }
    }

}