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
    public partial class ArrowRecoveryTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class HuntingArrowRecoveryTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Arrow Recovery: Hunting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows you to recover arrows shot into animals when harvesting them."); } }

        public HuntingArrowRecoveryTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HuntingArrowRecoveryTalent), 
            
            
            };
            this.OwningSkill = typeof(HuntingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class HuntingArrowRecoveryTalent : ArrowRecoveryTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HuntingArrowRecoveryTalentGroup); } }
        public HuntingArrowRecoveryTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    
}