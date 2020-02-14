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
    public partial class GearRecipe : Recipe
    {
        public GearRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GearItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 3, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(GearRecipe), Item.Get<GearItem>().UILink(), 1, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Gear"), typeof(GearRecipe));

            CraftingComponent.AddRecipe(typeof(ShaperObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class GearItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Iron Gear"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Iron Gears"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A toothed machine part that interlocks with others."); } }
    }
}