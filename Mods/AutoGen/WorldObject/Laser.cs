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
    public partial class LaserItem :
        WorldObjectItem<LaserObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laser"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("AVOID DIRECT EYE EXPOSURE"); } }

        static LaserItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(6000))); } }  
    }

    [RequiresSkill(typeof(ElectronicsSkill), 1)]      
    public partial class LaserRecipe : Recipe
    {
        public LaserRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LaserItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(typeof(ElectronicsSkill), 200, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 200, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 100, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<FramedGlassItem>(typeof(ElectronicsSkill), 50, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 50;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(LaserRecipe), Item.Get<LaserItem>().UILink(), 240, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Laser"), typeof(LaserRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}