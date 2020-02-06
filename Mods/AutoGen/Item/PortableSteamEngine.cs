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
    public partial class PortableSteamEngineRecipe : Recipe
    {
        public PortableSteamEngineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<ScrewsItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<IronPlateItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<GearItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<BoilerItem>(typeof(MechanicsSkill), 8, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 10;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(PortableSteamEngineRecipe), Item.Get<PortableSteamEngineItem>().UILink(), 20, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Portable Steam Engine"), typeof(PortableSteamEngineRecipe));

            CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(5000)]      
    [Currency]              
    public partial class PortableSteamEngineItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Portable Steam Engine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An engine that generates mechanical power through steam."); } }
    }
}