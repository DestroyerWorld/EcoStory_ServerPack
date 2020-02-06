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
    public partial class LuckyBreakTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class MiningLuckyBreakTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lucky Break: Mining"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Mining rocks no longer has a chance to create large chunks."); } }

        public MiningLuckyBreakTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MiningLuckyBreakTalent), 
            
            
            };
            this.OwningSkill = typeof(MiningSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MiningLuckyBreakTalent : LuckyBreakTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MiningLuckyBreakTalentGroup); } }
        public MiningLuckyBreakTalent()
        {
            this.Value = 1;
        }
    }
    
    
    
}