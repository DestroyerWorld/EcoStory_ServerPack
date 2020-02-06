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
    [RequireRoomMaterialTier(0.2f, typeof(FertilizersLavishReqTalent), typeof(FertilizersFrugalReqTalent))]   
    public partial class FarmersTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farmers Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FarmersTableItem); } } 



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
    public partial class FarmersTableItem :
        WorldObjectItem<FarmersTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farmers Table"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A basic table for creating farming tools and similar products."); } }

        static FarmersTableItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(FarmingSkill), 0)]      
    public partial class FarmersTableRecipe : Recipe
    {
        public FarmersTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FarmersTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(typeof(FarmingSkill), 20, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent)),
                new CraftingElement<LogItem>(typeof(FarmingSkill), 30, FarmingSkill.MultiplicativeStrategy, typeof(FarmingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 5;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(FarmersTableRecipe), Item.Get<FarmersTableItem>().UILink(), 10, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Farmers Table"), typeof(FarmersTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}