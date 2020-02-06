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

    [RequiresModule(typeof(ShaperObject))]        
    [RequiresSkill(typeof(MechanicsSkill), 0)]      
    public partial class BoilerRecipe : Recipe
    {
        public BoilerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BoilerItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronPlateItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<ScrewsItem>(typeof(MechanicsSkill), 16, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 3;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(BoilerRecipe), Item.Get<BoilerItem>().UILink(), 5, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Boiler"), typeof(BoilerRecipe));

            CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class BoilerItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Boiler"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }
    }
}