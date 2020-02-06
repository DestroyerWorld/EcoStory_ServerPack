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

    [RequiresSkill(typeof(ElectronicsSkill), 0)]      
    public partial class GoldFlakesRecipe : Recipe
    {
        public GoldFlakesRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldFlakesItem>(4),  
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(typeof(ElectronicsSkill), 4, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(GoldFlakesRecipe), Item.Get<GoldFlakesItem>().UILink(), 2, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Gold Flakes"), typeof(GoldFlakesRecipe));

            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class GoldFlakesItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gold Flakes"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Gold Flakes"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A highly efficient conductor for delicate electronics."); } }
    }
}