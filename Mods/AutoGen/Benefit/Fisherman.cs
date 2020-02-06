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
    public partial class FishermanTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class HuntingFishermanTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fisherman: Hunting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public HuntingFishermanTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HuntingFishermanTalent), 
            
            
            };
            this.OwningSkill = typeof(HuntingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class HuntingFishermanTalent : FishermanTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HuntingFishermanTalentGroup); } }
        public HuntingFishermanTalent()
        {
            this.Value = 2;
        }
    }
    
    
    
}