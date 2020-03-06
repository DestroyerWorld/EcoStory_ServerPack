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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]      
    public partial class QuicklimeRecipe : Recipe
    {
        public QuicklimeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<QuicklimeItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LimestoneItem>(typeof(AdvancedSmeltingSkill), 8, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(QuicklimeRecipe), Item.Get<QuicklimeItem>().UILink(), 0.5f, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Quicklime"), typeof(QuicklimeRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class QuicklimeItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Quicklime"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A chemical compound used in steel production."); } }
    }
}