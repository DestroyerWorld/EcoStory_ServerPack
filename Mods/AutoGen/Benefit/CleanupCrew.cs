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
    public partial class CleanupCrewTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class LoggingCleanupCrewTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cleanup Crew: Logging"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Destroying tree debris no longer costs additional calories."); } }

        public LoggingCleanupCrewTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LoggingCleanupCrewTalent), 
            
            
            };
            this.OwningSkill = typeof(LoggingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class LoggingCleanupCrewTalent : CleanupCrewTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LoggingCleanupCrewTalentGroup); } }
        public LoggingCleanupCrewTalent()
        {
            this.Value = 0.66f;
        }
    }
    
    
    
}