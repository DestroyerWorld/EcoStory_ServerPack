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
    public partial class SweepingHandsTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class MiningSweepingHandsTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sweeping Hands: Mining"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Picking up rocks also attempts to pick up similar rocks in an area."); } }

        public MiningSweepingHandsTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MiningSweepingHandsTalent), 
            
            
            };
            this.OwningSkill = typeof(MiningSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MiningSweepingHandsTalent : SweepingHandsTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MiningSweepingHandsTalentGroup); } }
        public MiningSweepingHandsTalent()
        {
            this.Value = 1;
        }
    }
    
    
    
}