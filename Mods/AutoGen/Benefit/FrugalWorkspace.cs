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
    public partial class FrugalWorkspaceTalent : Talent
    {
        public override bool Base { get { return true; } }
        public override Type TalentType { get { return typeof(CraftingTalent); } } 
    }

    [Serialized]
    public partial class AdvancedBakingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: AdvancedBaking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public AdvancedBakingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedBakingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedBakingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedBakingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedBakingFrugalWorkspaceTalentGroup); } }
        public AdvancedBakingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCampfireCookingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: AdvancedCampfireCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public AdvancedCampfireCookingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCampfireCookingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCampfireCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedCampfireCookingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCampfireCookingFrugalWorkspaceTalentGroup); } }
        public AdvancedCampfireCookingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCookingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: AdvancedCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public AdvancedCookingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCookingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedCookingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCookingFrugalWorkspaceTalentGroup); } }
        public AdvancedCookingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedSmeltingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: AdvancedSmelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public AdvancedSmeltingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedSmeltingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedSmeltingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedSmeltingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedSmeltingFrugalWorkspaceTalentGroup); } }
        public AdvancedSmeltingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class BakingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public BakingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BakingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(BakingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BakingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BakingFrugalWorkspaceTalentGroup); } }
        public BakingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class BasicEngineeringFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: BasicEngineering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public BasicEngineeringFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BasicEngineeringFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(BasicEngineeringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BasicEngineeringFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BasicEngineeringFrugalWorkspaceTalentGroup); } }
        public BasicEngineeringFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class BricklayingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Bricklaying"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public BricklayingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BricklayingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(BricklayingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BricklayingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BricklayingFrugalWorkspaceTalentGroup); } }
        public BricklayingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class ButcheryFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Butchery"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public ButcheryFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ButcheryFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(ButcherySkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class ButcheryFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ButcheryFrugalWorkspaceTalentGroup); } }
        public ButcheryFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class CementFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Cement"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public CementFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CementFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(CementSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CementFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CementFrugalWorkspaceTalentGroup); } }
        public CementFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class CookingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public CookingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CookingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(CookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CookingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CookingFrugalWorkspaceTalentGroup); } }
        public CookingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class CuttingEdgeCookingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: CuttingEdgeCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public CuttingEdgeCookingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CuttingEdgeCookingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(CuttingEdgeCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CuttingEdgeCookingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CuttingEdgeCookingFrugalWorkspaceTalentGroup); } }
        public CuttingEdgeCookingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class ElectronicsFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public ElectronicsFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ElectronicsFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(ElectronicsSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class ElectronicsFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ElectronicsFrugalWorkspaceTalentGroup); } }
        public ElectronicsFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class FertilizersFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public FertilizersFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FertilizersFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(FertilizersSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class FertilizersFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FertilizersFrugalWorkspaceTalentGroup); } }
        public FertilizersFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class GlassworkingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Glassworking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public GlassworkingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GlassworkingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(GlassworkingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class GlassworkingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GlassworkingFrugalWorkspaceTalentGroup); } }
        public GlassworkingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class HewingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Hewing"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public HewingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HewingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(HewingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class HewingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HewingFrugalWorkspaceTalentGroup); } }
        public HewingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class IndustryFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Industry"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public IndustryFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(IndustryFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(IndustrySkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class IndustryFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(IndustryFrugalWorkspaceTalentGroup); } }
        public IndustryFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class LumberFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public LumberFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LumberFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(LumberSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class LumberFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LumberFrugalWorkspaceTalentGroup); } }
        public LumberFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class MechanicsFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public MechanicsFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MechanicsFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(MechanicsSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MechanicsFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MechanicsFrugalWorkspaceTalentGroup); } }
        public MechanicsFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class MillingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public MillingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MillingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(MillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MillingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MillingFrugalWorkspaceTalentGroup); } }
        public MillingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class MortaringFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Mortaring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public MortaringFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MortaringFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(MortaringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MortaringFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MortaringFrugalWorkspaceTalentGroup); } }
        public MortaringFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class OilDrillingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: OilDrilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public OilDrillingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(OilDrillingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(OilDrillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class OilDrillingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(OilDrillingFrugalWorkspaceTalentGroup); } }
        public OilDrillingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class PaperMillingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: PaperMilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public PaperMillingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(PaperMillingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(PaperMillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class PaperMillingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(PaperMillingFrugalWorkspaceTalentGroup); } }
        public PaperMillingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class SmeltingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public SmeltingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(SmeltingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(SmeltingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class SmeltingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SmeltingFrugalWorkspaceTalentGroup); } }
        public SmeltingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class TailoringFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Tailoring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public TailoringFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(TailoringFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(TailoringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class TailoringFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(TailoringFrugalWorkspaceTalentGroup); } }
        public TailoringFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    

    [Serialized]
    public partial class FarmingFrugalWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Frugal Workspace: Farming"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Lowers the tier requirement of related tables by 0.2"); } }

        public FarmingFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FarmingFrugalReqTalent), 
            
            
            };
            this.OwningSkill = typeof(FarmingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class FarmingFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FarmingFrugalWorkspaceTalentGroup); } }
        public FarmingFrugalReqTalent()
        {
            this.Value = -0.2f;
        }
    }
    
    
    
}