// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Stats
{
    using System.Collections.Generic;
    using Core.Utils;
    using Eco.Stats;
    using Eco.Stats.Playstyles;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Stats;
    using Gameplay.Stats.ConcretePlayerActions;
    using Shared.Utils;
    using TechTree;
    using LiteDB;

    public static class PlaystyleHelper
    {
        public static WeightedStat Stat(IPlayerActionManager manager, float weight, string subject, string obj)
        {
            IPlayerStatManager statManager = PlayerActions.StatManager(manager);
            return new WeightedStat(statManager, 1, statManager.GetSummaryString);
        }

        public static WeightedStat GainSkillStat<T>(int weight) where T : Skill
        {
            return new WeightedStat(
                PlayerActions.StatManager(PlayerActions.LearnSpecialty),
                weight,
                (count) => string.Format("gained {0} skill {1} in {2}", Text.Int(count), "level".Pluralize((int)count), Item.Get<T>().DisplayName),
                Query.EQ(PlayerActionExtensions.SkillTypeNameField, typeof(T).Name));
        }

        public static WeightedStat ItemStat<T, A>(PlayerActionManager<A> manager, int weight, string itemName)
            where T : Item
            where A : IPlayerAction, IHasItemContext, new()
        {
            return new WeightedStat(
                PlayerActions.StatManager(manager),
                weight,
                (count) => string.Format("crafted {0} {1}", Text.Int(count), count == 1 ? itemName : itemName.Pluralize()),
                Query.Where(PlayerActionExtensions.ItemTypeNameField, (typeName) => Item.Get((string)typeName) is T));
        }
    }

    internal class LaborPlaystyleRater : VectorPlaystyleRater
    {
        public LaborPlaystyleRater() : base(Playstyle.Labor) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.GainSkillStat<SurvivalistSkill>(10),
                PlaystyleHelper.ItemStat<BlockItem, SellAction>(PlayerActions.Sell, 1, "block"),
            };
        }
    }

    internal class TradePlaystyleRater : VectorPlaystyleRater
    {
        public TradePlaystyleRater() : base(Playstyle.Trade) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.ItemStat<StoreItem, CraftAction>(PlayerActions.Craft, 10, "store"),
                PlaystyleHelper.Stat(PlayerActions.Sell, 1, "sold", "item"),
            };
        }
    }

    internal class GovernmentPlaystyleRater : VectorPlaystyleRater
    {
        public GovernmentPlaystyleRater() : base(Playstyle.Government) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.Stat(PlayerActions.Message, 0.1f, "sent", "message"),
                PlaystyleHelper.Stat(PlayerActions.Vote, 1, "voted", "time"),
                PlaystyleHelper.Stat(PlayerActions.ProposeVote, 5, "proposed", "vote"),
                PlaystyleHelper.Stat(PlayerActions.GetElected, 10, "got elected", "time"),
            };
        }
    }

    internal class ConstructionPlaystyleRater : VectorPlaystyleRater
    {
        public ConstructionPlaystyleRater() : base(Playstyle.Construction) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.Stat(PlayerActions.Place, 1, "placed", "block"),
                PlaystyleHelper.ItemStat<BlockItem, CraftAction>(PlayerActions.Craft, 1, "block"),
                PlaystyleHelper.GainSkillStat<CarpenterSkill>(10),
                PlaystyleHelper.GainSkillStat<MasonSkill>(10),
            };
        }
    }

    internal class NutritionPlaystyleRater : VectorPlaystyleRater
    {
        public NutritionPlaystyleRater() : base(Playstyle.Nutrition) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.Stat(PlayerActions.Harvest, 1, "harvested", "organism"),
                PlaystyleHelper.ItemStat<FoodItem, CraftAction>(PlayerActions.Craft, 2, "food item"),
                PlaystyleHelper.GainSkillStat<CookingSkill>(10),
                PlaystyleHelper.GainSkillStat<FarmerSkill>(10),
            };
        }
    }

    internal class CraftingPlaystyleRater : VectorPlaystyleRater
    {
        public CraftingPlaystyleRater() : base(Playstyle.Craftsmanship) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.Stat(PlayerActions.Craft, 1, "crafted", "item"),
                PlaystyleHelper.Stat(PlayerActions.Sell, 1, "sold", "item"),
                PlaystyleHelper.GainSkillStat<EngineerSkill>(4),
                PlaystyleHelper.GainSkillStat<SmithSkill>(4),
            };
        }
    }

    internal class ResearchPlaystyleRater : VectorPlaystyleRater
    {
        public ResearchPlaystyleRater() : base(Playstyle.Research) { }

        protected override IEnumerable<WeightedStat> GetWeightedStats()
        {
            return new List<WeightedStat>()
            {
                PlaystyleHelper.ItemStat<SkillBook, CraftAction>(PlayerActions.Craft, 4, "skill book"),
                PlaystyleHelper.ItemStat<SkillScroll, SellAction>(PlayerActions.Sell, 1, "skill scroll"),
            };
        }
    }
}
