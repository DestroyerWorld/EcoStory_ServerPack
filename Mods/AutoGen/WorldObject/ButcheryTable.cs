namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.8f, typeof(ButcheryLavishReqTalent), typeof(ButcheryFrugalReqTalent))]   
    public partial class ButcheryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ButcheryTableItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Cooking"));                
            this.GetComponent<HousingComponent>().Set(ButcheryTableItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ButcheryTableItem :
        WorldObjectItem<ButcheryTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A block and cleaver to process raw meat into fancier dishes."); } }

        static ButcheryTableItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
    }

    [RequiresSkill(typeof(HewingSkill), 1)]      
    public partial class ButcheryTableRecipe : Recipe
    {
        public ButcheryTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ButcheryTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 40, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),
                new CraftingElement<LogItem>(typeof(HewingSkill), 40, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 10;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcheryTableRecipe), Item.Get<ButcheryTableItem>().UILink(), 5, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Butchery Table"), typeof(ButcheryTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}