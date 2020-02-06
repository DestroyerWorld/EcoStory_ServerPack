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
    public partial class LavishWorkspaceTalent : Talent
    {
        public override bool Base { get { return true; } }
        public override Type TalentType { get { return typeof(CraftingTalent); } } 
    }

    [Serialized]
    public partial class AdvancedBakingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: AdvancedBaking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public AdvancedBakingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedBakingLavishResourcesTalent), 
            
            typeof(AdvancedBakingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(AdvancedBakingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedBakingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedBakingLavishWorkspaceTalentGroup); } }
        public AdvancedBakingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class AdvancedBakingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedBakingLavishWorkspaceTalentGroup); } }
        public AdvancedBakingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class AdvancedCampfireCookingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: AdvancedCampfireCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public AdvancedCampfireCookingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCampfireCookingLavishResourcesTalent), 
            
            typeof(AdvancedCampfireCookingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(AdvancedCampfireCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedCampfireCookingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCampfireCookingLavishWorkspaceTalentGroup); } }
        public AdvancedCampfireCookingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class AdvancedCampfireCookingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCampfireCookingLavishWorkspaceTalentGroup); } }
        public AdvancedCampfireCookingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class AdvancedCookingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: AdvancedCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public AdvancedCookingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedCookingLavishResourcesTalent), 
            
            typeof(AdvancedCookingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(AdvancedCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedCookingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCookingLavishWorkspaceTalentGroup); } }
        public AdvancedCookingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class AdvancedCookingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedCookingLavishWorkspaceTalentGroup); } }
        public AdvancedCookingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class AdvancedSmeltingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: AdvancedSmelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public AdvancedSmeltingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(AdvancedSmeltingLavishResourcesTalent), 
            
            typeof(AdvancedSmeltingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(AdvancedSmeltingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class AdvancedSmeltingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedSmeltingLavishWorkspaceTalentGroup); } }
        public AdvancedSmeltingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class AdvancedSmeltingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(AdvancedSmeltingLavishWorkspaceTalentGroup); } }
        public AdvancedSmeltingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class BakingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public BakingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BakingLavishResourcesTalent), 
            
            typeof(BakingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(BakingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BakingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BakingLavishWorkspaceTalentGroup); } }
        public BakingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class BakingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BakingLavishWorkspaceTalentGroup); } }
        public BakingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class BasicEngineeringLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: BasicEngineering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public BasicEngineeringLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BasicEngineeringLavishResourcesTalent), 
            
            typeof(BasicEngineeringLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(BasicEngineeringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BasicEngineeringLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BasicEngineeringLavishWorkspaceTalentGroup); } }
        public BasicEngineeringLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class BasicEngineeringLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BasicEngineeringLavishWorkspaceTalentGroup); } }
        public BasicEngineeringLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class BricklayingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Bricklaying"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public BricklayingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(BricklayingLavishResourcesTalent), 
            
            typeof(BricklayingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(BricklayingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class BricklayingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BricklayingLavishWorkspaceTalentGroup); } }
        public BricklayingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class BricklayingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(BricklayingLavishWorkspaceTalentGroup); } }
        public BricklayingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class ButcheryLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Butchery"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public ButcheryLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ButcheryLavishResourcesTalent), 
            
            typeof(ButcheryLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(ButcherySkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class ButcheryLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ButcheryLavishWorkspaceTalentGroup); } }
        public ButcheryLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class ButcheryLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ButcheryLavishWorkspaceTalentGroup); } }
        public ButcheryLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class CementLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Cement"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public CementLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CementLavishResourcesTalent), 
            
            typeof(CementLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(CementSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CementLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CementLavishWorkspaceTalentGroup); } }
        public CementLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class CementLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CementLavishWorkspaceTalentGroup); } }
        public CementLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class CookingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public CookingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CookingLavishResourcesTalent), 
            
            typeof(CookingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(CookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CookingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CookingLavishWorkspaceTalentGroup); } }
        public CookingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class CookingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CookingLavishWorkspaceTalentGroup); } }
        public CookingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class CuttingEdgeCookingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: CuttingEdgeCooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public CuttingEdgeCookingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(CuttingEdgeCookingLavishResourcesTalent), 
            
            typeof(CuttingEdgeCookingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(CuttingEdgeCookingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class CuttingEdgeCookingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CuttingEdgeCookingLavishWorkspaceTalentGroup); } }
        public CuttingEdgeCookingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class CuttingEdgeCookingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(CuttingEdgeCookingLavishWorkspaceTalentGroup); } }
        public CuttingEdgeCookingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class ElectronicsLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public ElectronicsLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(ElectronicsLavishResourcesTalent), 
            
            typeof(ElectronicsLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(ElectronicsSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class ElectronicsLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ElectronicsLavishWorkspaceTalentGroup); } }
        public ElectronicsLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class ElectronicsLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(ElectronicsLavishWorkspaceTalentGroup); } }
        public ElectronicsLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class FertilizersLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public FertilizersLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FertilizersLavishResourcesTalent), 
            
            typeof(FertilizersLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(FertilizersSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class FertilizersLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FertilizersLavishWorkspaceTalentGroup); } }
        public FertilizersLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class FertilizersLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FertilizersLavishWorkspaceTalentGroup); } }
        public FertilizersLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class GlassworkingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Glassworking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public GlassworkingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(GlassworkingLavishResourcesTalent), 
            
            typeof(GlassworkingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(GlassworkingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class GlassworkingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GlassworkingLavishWorkspaceTalentGroup); } }
        public GlassworkingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class GlassworkingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(GlassworkingLavishWorkspaceTalentGroup); } }
        public GlassworkingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class HewingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Hewing"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public HewingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(HewingLavishResourcesTalent), 
            
            typeof(HewingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(HewingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class HewingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HewingLavishWorkspaceTalentGroup); } }
        public HewingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class HewingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(HewingLavishWorkspaceTalentGroup); } }
        public HewingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class IndustryLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Industry"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public IndustryLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(IndustryLavishResourcesTalent), 
            
            typeof(IndustryLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(IndustrySkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class IndustryLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(IndustryLavishWorkspaceTalentGroup); } }
        public IndustryLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class IndustryLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(IndustryLavishWorkspaceTalentGroup); } }
        public IndustryLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class LumberLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public LumberLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(LumberLavishResourcesTalent), 
            
            typeof(LumberLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(LumberSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class LumberLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LumberLavishWorkspaceTalentGroup); } }
        public LumberLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class LumberLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LumberLavishWorkspaceTalentGroup); } }
        public LumberLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class MechanicsLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public MechanicsLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MechanicsLavishResourcesTalent), 
            
            typeof(MechanicsLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(MechanicsSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MechanicsLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MechanicsLavishWorkspaceTalentGroup); } }
        public MechanicsLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class MechanicsLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MechanicsLavishWorkspaceTalentGroup); } }
        public MechanicsLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class MillingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public MillingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MillingLavishResourcesTalent), 
            
            typeof(MillingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(MillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MillingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MillingLavishWorkspaceTalentGroup); } }
        public MillingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class MillingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MillingLavishWorkspaceTalentGroup); } }
        public MillingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class MortaringLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Mortaring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public MortaringLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(MortaringLavishResourcesTalent), 
            
            typeof(MortaringLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(MortaringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class MortaringLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MortaringLavishWorkspaceTalentGroup); } }
        public MortaringLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class MortaringLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(MortaringLavishWorkspaceTalentGroup); } }
        public MortaringLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class OilDrillingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: OilDrilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public OilDrillingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(OilDrillingLavishResourcesTalent), 
            
            typeof(OilDrillingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(OilDrillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class OilDrillingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(OilDrillingLavishWorkspaceTalentGroup); } }
        public OilDrillingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class OilDrillingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(OilDrillingLavishWorkspaceTalentGroup); } }
        public OilDrillingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class PaperMillingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: PaperMilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public PaperMillingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(PaperMillingLavishResourcesTalent), 
            
            typeof(PaperMillingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(PaperMillingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class PaperMillingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(PaperMillingLavishWorkspaceTalentGroup); } }
        public PaperMillingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class PaperMillingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(PaperMillingLavishWorkspaceTalentGroup); } }
        public PaperMillingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class SmeltingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public SmeltingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(SmeltingLavishResourcesTalent), 
            
            typeof(SmeltingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(SmeltingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class SmeltingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SmeltingLavishWorkspaceTalentGroup); } }
        public SmeltingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class SmeltingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(SmeltingLavishWorkspaceTalentGroup); } }
        public SmeltingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class TailoringLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Tailoring"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public TailoringLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(TailoringLavishResourcesTalent), 
            
            typeof(TailoringLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(TailoringSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class TailoringLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(TailoringLavishWorkspaceTalentGroup); } }
        public TailoringLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class TailoringLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(TailoringLavishWorkspaceTalentGroup); } }
        public TailoringLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    

    [Serialized]
    public partial class FarmingLavishWorkspaceTalentGroup : TalentGroup
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lavish Workspace: Farming"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Increases the tier requirement of tables by 0.2, but reduces the resources needed by 10 percent."); } }

        public FarmingLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
            
            typeof(FarmingLavishResourcesTalent), 
            
            typeof(FarmingLavishReqTalent), 
            
            };
            this.OwningSkill = typeof(FarmingSkill);
            this.Level = 6;
        }
    }

    
    [Serialized]
    public partial class FarmingLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FarmingLavishWorkspaceTalentGroup); } }
        public FarmingLavishResourcesTalent()
        {
            this.Value = 0.9f;
        }
    }
    
    [Serialized]
    public partial class FarmingLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(FarmingLavishWorkspaceTalentGroup); } }
        public FarmingLavishReqTalent()
        {
            this.Value = 0.2f;
        }
    }
    
    
}