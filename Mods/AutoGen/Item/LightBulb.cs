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

    [RequiresSkill(typeof(ElectronicsSkill), 1)]      
    public partial class LightBulbRecipe : Recipe
    {
        public LightBulbRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassItem>(typeof(ElectronicsSkill), 8, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<CopperWiringItem>(typeof(ElectronicsSkill), 18, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(LightBulbRecipe), Item.Get<LightBulbItem>().UILink(), 5, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Light Bulb"), typeof(LightBulbRecipe));

            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }

    [Serialized]
    [Weight(50)]      
    [Currency]              
    public partial class LightBulbItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Light Bulb"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An electric light with a wire filament."); } }
    }
}