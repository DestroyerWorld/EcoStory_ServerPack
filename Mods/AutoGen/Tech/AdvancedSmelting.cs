namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;

    [Serialized]
    [RequiresSkill(typeof(SmithSkill), 0)]    
    public partial class AdvancedSmeltingSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Advanced Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Advanced smelting aids in the production of steel - a key ingredient in the progress of any group. Level by crafting advanced smelting recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1,
                
                1 - 0.5f,
                
                1 - 0.55f,
                
                1 - 0.6f,
                
                1 - 0.65f,
                
                1 - 0.7f,
                
                1 - 0.75f,
                
                1 - 0.8f,
                
            });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0,
                
                0.5f,
                
                0.55f,
                
                0.6f,
                
                0.65f,
                
                0.7f,
                
                0.75f,
                
                0.8f,
                
            });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public static int[] SkillPointCost = {
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
        };
        public override int RequiredPoint { get { return this.Level < SkillPointCost.Length ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < SkillPointCost.Length ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 4; } }
    }

    [Serialized]
    public partial class AdvancedSmeltingSkillBook : SkillBook<AdvancedSmeltingSkill, AdvancedSmeltingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Smelting Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedSmeltingSkillScroll : SkillScroll<AdvancedSmeltingSkill, AdvancedSmeltingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Smelting Skill Scroll"); } }
    }

    [RequiresSkill(typeof(SmeltingSkill), 0)] 
    public partial class AdvancedSmeltingSkillBookRecipe : Recipe
    {
        public AdvancedSmeltingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedSmeltingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CoalItem>(200),
                new CraftingElement<IronIngotItem>(100),
                new CraftingElement<BrickItem>(100) 
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.DoStr("Advanced Smelting Skill Book"), typeof(AdvancedSmeltingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
