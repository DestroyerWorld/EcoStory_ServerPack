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
    [RequireRoomMaterialTier(0.8f, typeof(SmeltingLavishReqTalent), typeof(SmeltingFrugalReqTalent))]   
    public partial class AnvilObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Anvil"); } } 

        public virtual Type RepresentedItemType { get { return typeof(AnvilItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                
            this.GetComponent<HousingComponent>().Set(AnvilItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class AnvilItem :
        WorldObjectItem<AnvilObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Anvil"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A solid shaped piece of metal used to hammer ingots into tools and other useful things."); } }

        static AnvilItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
    }

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class AnvilRecipe : Recipe
    {
        public AnvilRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AnvilItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 10;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(AnvilRecipe), Item.Get<AnvilItem>().UILink(), 20, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Anvil"), typeof(AnvilRecipe));
            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }
}