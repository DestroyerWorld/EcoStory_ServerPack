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
    public partial class ToolEfficiencyTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class LoggingToolEfficiencyTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Efficiency: Logging"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the calorie cost of using related tool by 20 percent."); } }

        public LoggingToolEfficiencyTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LoggingToolEfficiencyTalent), 
            
            
            };
            this.OwningSkill = typeof(LoggingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class LoggingToolEfficiencyTalent : ToolEfficiencyTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LoggingToolEfficiencyTalentGroup); } }
        public LoggingToolEfficiencyTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class MiningToolEfficiencyTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Efficiency: Mining"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the calorie cost of using related tool by 20 percent."); } }

        public MiningToolEfficiencyTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MiningToolEfficiencyTalent), 
            
            
            };
            this.OwningSkill = typeof(MiningSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MiningToolEfficiencyTalent : ToolEfficiencyTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MiningToolEfficiencyTalentGroup); } }
        public MiningToolEfficiencyTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class GatheringToolEfficiencyTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tool Efficiency: Gathering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the calorie cost of using related tool by 20 percent."); } }

        public GatheringToolEfficiencyTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GatheringToolEfficiencyTalent), 
            
            
            };
            this.OwningSkill = typeof(GatheringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class GatheringToolEfficiencyTalent : ToolEfficiencyTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GatheringToolEfficiencyTalentGroup); } }
        public GatheringToolEfficiencyTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    
}