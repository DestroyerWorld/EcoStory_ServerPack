namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    public partial class AggressiveAnglerTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class HuntingAggressiveAnglerTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Aggressive Angler: Hunting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public HuntingAggressiveAnglerTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HuntingAggressiveAnglerTalent), 
            
            
            };
            this.OwningSkill = typeof(HuntingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class HuntingAggressiveAnglerTalent : AggressiveAnglerTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HuntingAggressiveAnglerTalentGroup); } }
        public HuntingAggressiveAnglerTalent()
        {
            this.Value = 0.25f;
        }
    }
    
    
    
}