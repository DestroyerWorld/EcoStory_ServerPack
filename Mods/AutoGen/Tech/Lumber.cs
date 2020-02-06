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
    [RequiresSkill(typeof(CarpenterSkill), 0)]    
    public partial class LumberSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Forming trees into more structurally sound and sturdy lumber for use in construction or production. Level by crafting related recipes."); } }

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
        public override int RequiredPoint { get { return 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 3; } }
    }

    [Serialized]
    public partial class LumberSkillBook : SkillBook<LumberSkill, LumberSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Skill Book"); } }
    }

    [Serialized]
    public partial class LumberSkillScroll : SkillScroll<LumberSkill, LumberSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Skill Scroll"); } }
    }

    [RequiresSkill(typeof(HewingSkill), 0)] 
    public partial class LumberSkillBookRecipe : Recipe
    {
        public LumberSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LumberSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(20),
                new CraftingElement<HewnLogItem>(40),
                new CraftingElement<ClothItem>(20) 
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("Lumber Skill Book"), typeof(LumberSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
