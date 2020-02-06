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
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    public partial class StoneScytheRecipe : Recipe
    {
        public StoneScytheRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<StoneScytheItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(3),
                new CraftingElement<StoneItem>(10)    
            };
            this.CraftMinutes = new ConstantValue(0.5f);
            this.Initialize(Localizer.DoStr("Stone Scythe"), typeof(StoneScytheRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
    [Serialized]
    [ItemTier(1)] 
    [Weight(1000)]
    [Category("Tool")]
    public partial class StoneScytheItem : ScytheItem
    {

        public override LocString DisplayName { get { return Localizer.DoStr("Stone Scythe"); } }
        private static IDynamicValue caloriesBurn = CreateCalorieValue(20, typeof(GatheringSkill), typeof(StoneScytheItem), new StoneScytheItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Type ExperienceSkill { get { return typeof(GatheringSkill); } }
        private static IDynamicValue exp = new ConstantValue(1);
        public override IDynamicValue ExperienceRate { get { return exp; } }
        private static IDynamicValue tier = new MultiDynamicValue(MultiDynamicOps.Sum, new ConstantValue(1), new TalentModifiedValue(typeof(GatheringToolStrengthTalent), 0)); 
        public override IDynamicValue Tier { get { return tier; } }


        private static IDynamicValue skilledRepairCost = new ConstantValue(5);  
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }


        public override float DurabilityRate { get { return DurabilityMax / 100f; } }
        
        public override Item RepairItem         {get{ return Item.Get<StoneItem>(); } }
        public override int FullRepairAmount    {get{ return 5; } }
    }
}