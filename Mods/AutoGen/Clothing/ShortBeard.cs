namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Category("Hidden")]  
    [Weight(100)]      
    public partial class ShortBeardItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Short Beard"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr(""); } }
        public override string Slot             { get { return ClothingSlot.FacialHair; } }             
        public override bool Starter            { get { return true ; } }                       

    }

}