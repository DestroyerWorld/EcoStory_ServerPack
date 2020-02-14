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
    [RequireComponent(typeof(CustomTextComponent))]              
    [RequireComponent(typeof(MinimapComponent))]                
    public partial class SmallHangingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingStoneSignItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Sign"));                
            this.GetComponent<CustomTextComponent>().Initialize(700);                                       

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class SmallHangingStoneSignItem :
        WorldObjectItem<SmallHangingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallHangingStoneSignItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(MortaringSkill), 1)]      
    public partial class SmallHangingStoneSignRecipe : Recipe
    {
        public SmallHangingStoneSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingStoneSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 30, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 2;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmallHangingStoneSignRecipe), Item.Get<SmallHangingStoneSignItem>().UILink(), 10, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Small Hanging Stone Sign"), typeof(SmallHangingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}