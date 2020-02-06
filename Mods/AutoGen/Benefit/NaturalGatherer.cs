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
    public partial class NaturalGathererTalent : Talent
    {
        public override bool Base { get { return true; } }
    }

    [Serialized]
    public partial class GatheringNaturalGathererTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Natural Gatherer: Gathering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the yield of natural plants by 20 percent."); } }

        public GatheringNaturalGathererTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GatheringNaturalGathererTalent), 
            
            
            };
            this.OwningSkill = typeof(GatheringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class GatheringNaturalGathererTalent : NaturalGathererTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GatheringNaturalGathererTalentGroup); } }
        public GatheringNaturalGathererTalent()
        {
            this.Value = 2;
        }
    }
    
    
    
}