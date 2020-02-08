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
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.8f, typeof(BasicEngineeringLavishReqTalent), typeof(BasicEngineeringFrugalReqTalent))]   
    public partial class WainwrightTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WainwrightTableItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WainwrightTableItem :
        WorldObjectItem<WainwrightTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A set of smoothing and woodworking tools that assist in creating wheels and transportation."); } }

        static WainwrightTableItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(HewingSkill), 0)]      
    public partial class WainwrightTableRecipe : Recipe
    {
        public WainwrightTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WainwrightTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 60, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 60, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 5;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(WainwrightTableRecipe), Item.Get<WainwrightTableItem>().UILink(), 10, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wainwright Table"), typeof(WainwrightTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}