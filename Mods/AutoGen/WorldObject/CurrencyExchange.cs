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
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.8f)]   
    public partial class CurrencyExchangeObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Currency Exchange"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CurrencyExchangeItem); } } 



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
    public partial class CurrencyExchangeItem :
        WorldObjectItem<CurrencyExchangeObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Currency Exchange"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Allows players to exchange currency."); } }

        static CurrencyExchangeItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class CurrencyExchangeRecipe : Recipe
    {
        public CurrencyExchangeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CurrencyExchangeItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 40, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<BrickItem>(typeof(SmeltingSkill), 40, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<LumberItem>(typeof(SmeltingSkill), 40, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 25;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CurrencyExchangeRecipe), Item.Get<CurrencyExchangeItem>().UILink(), 30, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Currency Exchange"), typeof(CurrencyExchangeRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}