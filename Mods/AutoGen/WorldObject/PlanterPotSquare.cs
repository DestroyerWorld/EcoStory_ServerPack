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
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(FakePlantComponent))]              
    public partial class PlanterPotSquareObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PlanterPotSquareItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Housing"));                
            this.GetComponent<HousingComponent>().Set(PlanterPotSquareItem.HousingVal);                                
            this.GetComponent<FakePlantComponent>().Initialize();                                           

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class PlanterPotSquareItem :
        WorldObjectItem<PlanterPotSquareObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Sometimes you just want to bring a little bit of nature into your house."); } }

        static PlanterPotSquareItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.9f    
        };}}
        
    }

    [RequiresSkill(typeof(MortaringSkill), 3)]      
    public partial class PlanterPotSquareRecipe : Recipe
    {
        public PlanterPotSquareRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PlanterPotSquareItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClayItem>(typeof(MortaringSkill), 8, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 2;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(PlanterPotSquareRecipe), Item.Get<PlanterPotSquareItem>().UILink(), 5, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Planter Pot Square"), typeof(PlanterPotSquareRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}