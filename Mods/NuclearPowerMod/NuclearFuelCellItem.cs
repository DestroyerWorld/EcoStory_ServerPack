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

    [RequiresModule(typeof(ComputerLabObject))]
    [RequiresSkill(typeof(NuclearTechnitionSkill), 1)]      
    public partial class NuclearFuelCellRecipe : Recipe
    {
        public NuclearFuelCellRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<NuclearFuelCellItem>(1),
                new CraftingElement<CopperIngotItem>(100),
                new CraftingElement<TailingsItem>(100),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BarrelItem>(1),
				new CraftingElement<LiquidNitrogenItem>(100),
                new CraftingElement<CopperOreItem>(typeof(NuclearTechnitionSkill), 500, NuclearTechnitionSkill.MultiplicativeStrategy),
            };
            this.ExperienceOnCraft = 20f;
            
            this.Initialize(Localizer.DoStr("Ядерный Топливный Контейнер"), typeof(NuclearFuelCellRecipe));

            this.CraftMinutes = CreateCraftTimeValue(typeof(NuclearFuelCellRecipe), Item.Get<NuclearFuelCellItem>().UILink(), 240f, typeof(NuclearTechnitionSkill));

            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
 
    [Serialized]                                   
    [Weight(30000)]      
    [Fuel(100000000)][Tag("Топливо")]
    [MaxStackSize(10)]
    [Currency]              
    public partial class NuclearFuelCellItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ядерный Топливный Контейнер"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Ядерный Топливный Контейнер"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Ядерные топливные контейнеры содержат большое количество энергии. Они могут высвобождать эту энергию в течение большого периода времени в ядерном реакторе"); } }

    }

}