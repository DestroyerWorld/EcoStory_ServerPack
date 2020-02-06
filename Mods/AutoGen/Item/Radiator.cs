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

    [RequiresSkill(typeof(MechanicsSkill), 0)]      
    public partial class RadiatorRecipe : Recipe
    {
        public RadiatorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RadiatorItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HeatSinkItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<CopperWiringItem>(typeof(MechanicsSkill), 18, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 3;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(RadiatorRecipe), Item.Get<RadiatorItem>().UILink(), 4, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Radiator"), typeof(RadiatorRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricStampingPressObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class RadiatorItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Radiator"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A heat sink that uses liquid running through copper fins to disperse heat build-up."); } }
    }
}