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
    public partial class SprinterTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class SelfImprovementSprinterTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sprinter: SelfImprovement"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public SelfImprovementSprinterTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(SelfImprovementSprinterSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(SelfImprovementSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class SelfImprovementSprinterSpeedTalent : SprinterTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SelfImprovementSprinterTalentGroup); } }
        public SelfImprovementSprinterSpeedTalent()
        {
            this.Value = 1.2f;
        }
    }
    
    
    
}