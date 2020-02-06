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
    public partial class DeadeyeTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class HuntingDeadeyeTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Deadeye: Hunting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the headshot damage multiplier."); } }

        public HuntingDeadeyeTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HuntingDeadeyeTalent), 
            
            
            };
            this.OwningSkill = typeof(HuntingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class HuntingDeadeyeTalent : DeadeyeTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HuntingDeadeyeTalentGroup); } }
        public HuntingDeadeyeTalent()
        {
            this.Value = 1;
        }
    }
    
    
    
}