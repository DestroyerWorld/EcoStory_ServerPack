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
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class WoodenCeilingLightObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Ceiling Light"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenCeilingLightItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Lights"));                
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(WoodenCeilingLightItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WoodenCeilingLightItem :
        WorldObjectItem<WoodenCeilingLightObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Ceiling Light"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A more modern way to light up a room."); } }

        static WoodenCeilingLightItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }

    [RequiresSkill(typeof(LumberSkill), 5)]      
    public partial class WoodenCeilingLightRecipe : Recipe
    {
        public WoodenCeilingLightRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenCeilingLightItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(1), 
                new CraftingElement<LumberItem>(typeof(LumberSkill), 14, LumberSkill.MultiplicativeStrategy, typeof(LumberLavishResourcesTalent)),
                new CraftingElement<BoardItem>(typeof(LumberSkill), 30, LumberSkill.MultiplicativeStrategy, typeof(LumberLavishResourcesTalent)),
                new CraftingElement<ClothItem>(typeof(LumberSkill), 16, LumberSkill.MultiplicativeStrategy, typeof(LumberLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 3;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodenCeilingLightRecipe), Item.Get<WoodenCeilingLightItem>().UILink(), 10, typeof(LumberSkill), typeof(LumberFocusedSpeedTalent), typeof(LumberParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wooden Ceiling Light"), typeof(WoodenCeilingLightRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}