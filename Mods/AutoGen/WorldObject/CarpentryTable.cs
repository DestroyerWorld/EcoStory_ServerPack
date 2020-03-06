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
    [RequireRoomMaterialTier(0.2f, typeof(HewingLavishReqTalent), typeof(HewingFrugalReqTalent))]   
    public partial class CarpentryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CarpentryTableItem); } } 



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
    public partial class CarpentryTableItem :
        WorldObjectItem<CarpentryTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A table for basic wooden crafts for home improvement and progress."); } }

        static CarpentryTableItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(HewingSkill), 1)]      
    public partial class CarpentryTableRecipe : Recipe
    {
        public CarpentryTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CarpentryTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 30, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),
                new CraftingElement<StoneItem>(typeof(HewingSkill), 30, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 10;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CarpentryTableRecipe), Item.Get<CarpentryTableItem>().UILink(), 1, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Carpentry Table"), typeof(CarpentryTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}