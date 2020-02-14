namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(MechanicsSkill), 1)]      
    public partial class AsphaltRoadRecipe : Recipe
    {
        public AsphaltRoadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AsphaltRoadItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MechanicsSkill), 3, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<StoneItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(AsphaltRoadRecipe), Item.Get<AsphaltRoadItem>().UILink(), 1, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Asphalt Road"), typeof(AsphaltRoadRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed]
    [Road(1.4f)]                                          
    [UsesRamp(typeof(AsphaltRoadWorldObjectBlock))]              
    [RequiresSkill(typeof(MechanicsSkill), 0)]   
    public partial class AsphaltRoadBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(AsphaltRoadItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(10000)]      
    [MakesRoads]                                            
    public partial class AsphaltRoadItem :
    RoadItem<AsphaltRoadBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Asphalt Road"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A paved surface constructed with asphalt and concrete. It's durable and extremely efficient for any wheeled vehicle."); } }

    }

}