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
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                          
    [RequireRoomMaterialTier(1.2f)]   
    public partial class MintObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MintItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Economy"));                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class MintItem :
        WorldObjectItem<MintObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Allows for the creation of currency."); } }

        static MintItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class MintRecipe : Recipe
    {
        public MintRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MintItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(SmeltingSkill), 40, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<LumberItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 20;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MintRecipe), Item.Get<MintItem>().UILink(), 60, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mint"), typeof(MintRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}