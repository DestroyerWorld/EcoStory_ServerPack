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
    public partial class ToolStrengthTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class LoggingToolStrengthTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Strength: Logging"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier of related tools by 1."); } }

        public LoggingToolStrengthTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LoggingToolStrengthTalent), 
            
            
            };
            this.OwningSkill = typeof(LoggingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class LoggingToolStrengthTalent : ToolStrengthTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LoggingToolStrengthTalentGroup); } }
        public LoggingToolStrengthTalent()
        {
            this.Value = 1;
        }
    }
    
    
    

    [Serialized]
    public partial class MiningToolStrengthTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Strength: Mining"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier of related tools by 1."); } }

        public MiningToolStrengthTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MiningToolStrengthTalent), 
            
            
            };
            this.OwningSkill = typeof(MiningSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MiningToolStrengthTalent : ToolStrengthTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MiningToolStrengthTalentGroup); } }
        public MiningToolStrengthTalent()
        {
            this.Value = 1;
        }
    }
    
    
    

    [Serialized]
    public partial class GatheringToolStrengthTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Strength: Gathering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier of related tools by 1."); } }

        public GatheringToolStrengthTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GatheringToolStrengthTalent), 
            
            
            };
            this.OwningSkill = typeof(GatheringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class GatheringToolStrengthTalent : ToolStrengthTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GatheringToolStrengthTalentGroup); } }
        public GatheringToolStrengthTalent()
        {
            this.Value = 1;
        }
    }
    
    
    
}