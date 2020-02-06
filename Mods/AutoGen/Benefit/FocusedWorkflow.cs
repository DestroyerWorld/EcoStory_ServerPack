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
    public partial class FocusedWorkflowTalent : Talent
    {
        public override bool Base { get { return true; } }
        public override Type TalentType { get { return typeof(CraftingTalent); } } 
    }

    [Serialized]
    public partial class AdvancedBakingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: AdvancedBaking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public AdvancedBakingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedBakingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedBakingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedBakingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedBakingFocusedWorkflowTalentGroup); } }
        public AdvancedBakingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCampfireCookingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: AdvancedCampfireCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public AdvancedCampfireCookingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCampfireCookingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCampfireCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedCampfireCookingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCampfireCookingFocusedWorkflowTalentGroup); } }
        public AdvancedCampfireCookingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedCookingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: AdvancedCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public AdvancedCookingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCookingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedCookingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCookingFocusedWorkflowTalentGroup); } }
        public AdvancedCookingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class AdvancedSmeltingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: AdvancedSmelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public AdvancedSmeltingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedSmeltingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(AdvancedSmeltingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class AdvancedSmeltingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedSmeltingFocusedWorkflowTalentGroup); } }
        public AdvancedSmeltingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class BakingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public BakingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BakingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BakingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BakingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BakingFocusedWorkflowTalentGroup); } }
        public BakingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class BasicEngineeringFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: BasicEngineering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public BasicEngineeringFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BasicEngineeringFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BasicEngineeringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BasicEngineeringFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BasicEngineeringFocusedWorkflowTalentGroup); } }
        public BasicEngineeringFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class BricklayingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Bricklaying"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public BricklayingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BricklayingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(BricklayingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class BricklayingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BricklayingFocusedWorkflowTalentGroup); } }
        public BricklayingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class ButcheryFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Butchery"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public ButcheryFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ButcheryFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(ButcherySkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class ButcheryFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ButcheryFocusedWorkflowTalentGroup); } }
        public ButcheryFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class CementFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Cement"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public CementFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CementFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CementSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CementFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CementFocusedWorkflowTalentGroup); } }
        public CementFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class CookingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public CookingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CookingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CookingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CookingFocusedWorkflowTalentGroup); } }
        public CookingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class CuttingEdgeCookingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: CuttingEdgeCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public CuttingEdgeCookingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CuttingEdgeCookingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(CuttingEdgeCookingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class CuttingEdgeCookingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CuttingEdgeCookingFocusedWorkflowTalentGroup); } }
        public CuttingEdgeCookingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class ElectronicsFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public ElectronicsFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ElectronicsFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(ElectronicsSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class ElectronicsFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ElectronicsFocusedWorkflowTalentGroup); } }
        public ElectronicsFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class FertilizersFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public FertilizersFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FertilizersFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(FertilizersSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class FertilizersFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FertilizersFocusedWorkflowTalentGroup); } }
        public FertilizersFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class GlassworkingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Glassworking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public GlassworkingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GlassworkingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(GlassworkingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class GlassworkingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GlassworkingFocusedWorkflowTalentGroup); } }
        public GlassworkingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class HewingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Hewing"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public HewingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HewingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(HewingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class HewingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HewingFocusedWorkflowTalentGroup); } }
        public HewingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class IndustryFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Industry"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public IndustryFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(IndustryFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(IndustrySkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class IndustryFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(IndustryFocusedWorkflowTalentGroup); } }
        public IndustryFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class LumberFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public LumberFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LumberFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(LumberSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class LumberFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LumberFocusedWorkflowTalentGroup); } }
        public LumberFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class MechanicsFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public MechanicsFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MechanicsFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MechanicsSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MechanicsFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MechanicsFocusedWorkflowTalentGroup); } }
        public MechanicsFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class MillingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public MillingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MillingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MillingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MillingFocusedWorkflowTalentGroup); } }
        public MillingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class MortaringFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Mortaring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public MortaringFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MortaringFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(MortaringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class MortaringFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MortaringFocusedWorkflowTalentGroup); } }
        public MortaringFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class OilDrillingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: OilDrilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public OilDrillingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(OilDrillingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(OilDrillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class OilDrillingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(OilDrillingFocusedWorkflowTalentGroup); } }
        public OilDrillingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class PaperMillingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: PaperMilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public PaperMillingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(PaperMillingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(PaperMillingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class PaperMillingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(PaperMillingFocusedWorkflowTalentGroup); } }
        public PaperMillingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class SmeltingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public SmeltingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(SmeltingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(SmeltingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class SmeltingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SmeltingFocusedWorkflowTalentGroup); } }
        public SmeltingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class TailoringFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Tailoring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public TailoringFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(TailoringFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(TailoringSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class TailoringFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(TailoringFocusedWorkflowTalentGroup); } }
        public TailoringFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    

    [Serialized]
    public partial class FarmingFocusedWorkflowTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Focused Workflow: Farming"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Doubles the speed of related tables when alone."); } }

        public FarmingFocusedWorkflowTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FarmingFocusedSpeedTalent), 
            
            
            };
            this.OwningSkill = typeof(FarmingSkill);
            this.Level = 3;
        }
    }

    
    [Serialized]
    public partial class FarmingFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FarmingFocusedWorkflowTalentGroup); } }
        public FarmingFocusedSpeedTalent()
        {
            this.Value = 0.5f;
        }
    }
    
    
    
}