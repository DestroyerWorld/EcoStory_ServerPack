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
    public partial class ParallelProcessingTalent : Talent
    {
        public override bool Base { get { return true; } }
        public override Type TalentType { get { return typeof(CraftingTalent); } } 
    }

    [Serialized]
    public partial class AdvancedBakingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: AdvancedBaking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public AdvancedBakingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedBakingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedBakingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedBakingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedBakingParallelProcessingTalentGroup); } }
        public AdvancedBakingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCampfireCookingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: AdvancedCampfireCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public AdvancedCampfireCookingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCampfireCookingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCampfireCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedCampfireCookingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCampfireCookingParallelProcessingTalentGroup); } }
        public AdvancedCampfireCookingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCookingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: AdvancedCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public AdvancedCookingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCookingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedCookingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCookingParallelProcessingTalentGroup); } }
        public AdvancedCookingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedSmeltingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: AdvancedSmelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public AdvancedSmeltingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedSmeltingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedSmeltingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedSmeltingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedSmeltingParallelProcessingTalentGroup); } }
        public AdvancedSmeltingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class BakingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public BakingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BakingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BakingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BakingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BakingParallelProcessingTalentGroup); } }
        public BakingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class BasicEngineeringParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: BasicEngineering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public BasicEngineeringParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BasicEngineeringParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BasicEngineeringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BasicEngineeringParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BasicEngineeringParallelProcessingTalentGroup); } }
        public BasicEngineeringParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class BricklayingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Bricklaying"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public BricklayingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BricklayingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BricklayingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BricklayingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BricklayingParallelProcessingTalentGroup); } }
        public BricklayingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class ButcheryParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Butchery"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public ButcheryParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ButcheryParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(ButcherySkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class ButcheryParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ButcheryParallelProcessingTalentGroup); } }
        public ButcheryParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class CementParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Cement"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public CementParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CementParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CementSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CementParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CementParallelProcessingTalentGroup); } }
        public CementParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class CookingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public CookingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CookingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CookingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CookingParallelProcessingTalentGroup); } }
        public CookingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class CuttingEdgeCookingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: CuttingEdgeCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public CuttingEdgeCookingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CuttingEdgeCookingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CuttingEdgeCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CuttingEdgeCookingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CuttingEdgeCookingParallelProcessingTalentGroup); } }
        public CuttingEdgeCookingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class ElectronicsParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public ElectronicsParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ElectronicsParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(ElectronicsSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class ElectronicsParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ElectronicsParallelProcessingTalentGroup); } }
        public ElectronicsParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class FertilizersParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public FertilizersParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FertilizersParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(FertilizersSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class FertilizersParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FertilizersParallelProcessingTalentGroup); } }
        public FertilizersParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class GlassworkingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Glassworking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public GlassworkingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GlassworkingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(GlassworkingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class GlassworkingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GlassworkingParallelProcessingTalentGroup); } }
        public GlassworkingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class HewingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Hewing"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public HewingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HewingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(HewingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class HewingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HewingParallelProcessingTalentGroup); } }
        public HewingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class IndustryParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Industry"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public IndustryParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(IndustryParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(IndustrySkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class IndustryParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(IndustryParallelProcessingTalentGroup); } }
        public IndustryParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class LumberParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public LumberParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LumberParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(LumberSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class LumberParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LumberParallelProcessingTalentGroup); } }
        public LumberParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class MechanicsParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public MechanicsParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MechanicsParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MechanicsSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MechanicsParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MechanicsParallelProcessingTalentGroup); } }
        public MechanicsParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class MillingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public MillingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MillingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MillingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MillingParallelProcessingTalentGroup); } }
        public MillingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class MortaringParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Mortaring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public MortaringParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MortaringParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MortaringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MortaringParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MortaringParallelProcessingTalentGroup); } }
        public MortaringParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class OilDrillingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: OilDrilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public OilDrillingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(OilDrillingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(OilDrillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class OilDrillingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(OilDrillingParallelProcessingTalentGroup); } }
        public OilDrillingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class PaperMillingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: PaperMilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public PaperMillingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(PaperMillingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(PaperMillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class PaperMillingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(PaperMillingParallelProcessingTalentGroup); } }
        public PaperMillingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class SmeltingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public SmeltingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(SmeltingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(SmeltingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class SmeltingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SmeltingParallelProcessingTalentGroup); } }
        public SmeltingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class TailoringParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Tailoring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public TailoringParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(TailoringParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(TailoringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class TailoringParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(TailoringParallelProcessingTalentGroup); } }
        public TailoringParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    

    [Serialized]
    public partial class FarmingParallelProcessingTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Parallel Processing: Farming"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the crafting speed of related tables when they share a room with the same tables by 20 percent."); } }

        public FarmingParallelProcessingTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FarmingParallelSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(FarmingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class FarmingParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FarmingParallelProcessingTalentGroup); } }
        public FarmingParallelSpeedTalent()
        {
            this.Value = 0.8f;
        }
    }
    
    
    
}