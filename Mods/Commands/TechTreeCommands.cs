// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Players;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Gameplay.Items.Actionbar;
    using Eco.Shared.Utils;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Core.Tests;

    public class TechTreePath
    {
        public Dictionary<Type, int> Items = new Dictionary<Type, int>();
        public Dictionary<Type, int> BaseItems = new Dictionary<Type, int>();
        public Dictionary<Type, int> Skills = new Dictionary<Type, int>();
        public string Print()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("");
            text.AppendLine("ITEMS");
            foreach (var item in this.Items)
                text.AppendLine(Item.Get(item.Key).DisplayName + ": " + item.Value);
            text.AppendLine("");
            text.AppendLine("BASE ITEMS");
            foreach (var item in this.BaseItems)
                text.AppendLine(Item.Get(item.Key).DisplayName + ": " + item.Value + "(approx " + Math.Ceiling(item.Value * 0.2) + " min)");
            text.AppendLine("");
            text.AppendLine("SKILLS");
            foreach (var skill in this.Skills)
                text.AppendLine(Item.Get(skill.Key).DisplayName + ": " + skill.Value);
            return text.ToString();
        }
    }

    public class TechTreeCommands : IChatCommandHandler
    {
        [CITest(clientDependent: true)]
        [ChatCommand("Level up a user by one.  Default levels you up.", ChatAuthorizationLevel.Admin)]
        public static void LevelUpUser(User user, string name = null)
        {
            var targetUser = (name == null) ? user : UserManager.Users.FirstOrDefault(x=>x.Name.ContainsCaseInsensitive(name));
            if (targetUser == null) 
            {
                user.Player.SendTemporaryError(Localizer.Format("User not found with name containing '{0}'.", name));
                return;
            }
            targetUser.AddExperience(1 + (targetUser.GetSpecialtyCost() - targetUser.XP)); //Add one to get rid of rounding problems.
            user.Player.SendTemporaryMessage(Localizer.Format("{0} leveled up to level {1}", targetUser.UILink(), targetUser.SpecialtyPoints));
        }

        [CITest(clientDependent: true)]
        [ChatCommand("Levels all skills up 1 level at a time (no chunks).", ChatAuthorizationLevel.Developer)]
        public static void LevelUpAll(User user, int num = 1)
        {
            for(int i = 0; i < num; i++)
                foreach (var specialty in user.Skillset.Skills)
                    if(specialty.Level > 0)
                        user.Skillset.AddExperience(specialty.Type, specialty.ExperienceToLevel, Localizer.DoStr("dev command"));
        }

        [ChatCommand("Remove restrictions and gives you every skill at max level.", ChatAuthorizationLevel.Developer)]
        public static void Creative(User user)
        {
            Skill.AllItems.OfType<SkillScroll>().ForEach(x =>
            {
                user.Inventory.AddItem(x, user);
                x.OnUsed(user.Player, user.Inventory.NonEmptyStacks.Where(y => y.Item.TypeID == x.TypeID).First());
                user.Inventory.TryRemoveItem(x.Type, user);
            });
            user.Skillset.Skills.ForEach(x => x.ForceSetLevel(x.MaxLevel));
            user.Inventory.ToolbarBackpack.ClearRestrictions();
        }

        [ChatCommand("Adds random items to the user's inventory.", ChatAuthorizationLevel.Developer)]
        public static void FillMeUp(User user)
        {
            Item.AllItems.Where(x => !x.IsCarried).Shuffle().ForEach(x =>
            {
                if (user.Inventory.NonEmptyStacks.Count() < user.Inventory.Stacks.Count())
                    user.Inventory.TryAddItem(x);
            });
        }

        [CITest]
        [ChatCommand("Lists the items that cannot be crafted from some set of other items.  Note that many of these are not meant to be crafted, e.g. hips and stone.", ChatAuthorizationLevel.Developer)]
        public static void ListUnobtainableItems(User user)
        {
            HashSet<Type> allBaseItems = new HashSet<Type>();
            foreach (Item item in Item.AllItems)
            {
                if (!ShouldBeCraftable(item)) continue;

                TechTreePath path = new TechTreePath();
                GetPathToItemRec(item, true, ref path, 1);
                foreach (Type baseItemType in path.BaseItems.Keys)
                {
                    if (ShouldBeCraftable(Item.Create(baseItemType)))
                    {
                        allBaseItems.Add(baseItemType);
                    }
                }
            }

            foreach (Type unobtainableItem in allBaseItems)
            {
                ChatManager.SendChat(Item.Create(unobtainableItem).Name(1), user);
            }

            ChatManager.SendChat(allBaseItems.Count + " total unobtainable items.", user);
        }

        private static bool ShouldBeCraftable(Item item)
        {
            // a bunch of things that aren't really 'items' per se
            if (item is Skill) return false;
            if (item is ActionbarItem) return false;
            if (item is SkillScroll) return false;
            if (item.Hidden) return false;

            // items that you have other means of acquiring and therefore needn't be craftable
            if (OrganismItemManager.SourceSpeciesForItem(item.Type).Any()) return false;
            if (item is SkillBook && PlayerDefaults.GetDefaultSkills().Contains((item as SkillBook).SkillType)) return false;

            return true;
        }

        [ChatCommand("Gets the resources needed to craft something", ChatAuthorizationLevel.Developer)]
        public static void GetPathToItem(User user, string target)
        {
            var item = Item.GetItemByString(user, target);
            if (item == null)
                return;
            TechTreePath path = new TechTreePath();
            GetPathToItemRec(item, true, ref path, 1);
            //probably not the right place to send this, but for now I want to test it
            ChatManager.SendChat(path.Print(), user);
        }

        public static void GetPathToSkillRec(Skill target, bool includeItems, ref TechTreePath path, int level = 0)
        {
            if (!path.Skills.ContainsKey(target.GetType()))
            {
                //add skill to path
                path.Skills.Add(target.GetType(), target.Level);
                //if skill had a book and you're including items, path to it
                var rootTree = SkillTree.RootTreeFromSkill(target.GetType());
                if (rootTree != null)
                {
                    if(includeItems && SkillTree.SkillToSkillBook.ContainsKey(rootTree.GetType()))
                        GetPathToItemRec(Item.Get(SkillTree.SkillToSkillBook[rootTree.GetType()]), true, ref path);
                    //get skill tree root skill
                    GetPathToSkillRec(rootTree.StaticSkill, includeItems, ref path, 1);
                }
            }
            else if (path.Skills.ContainsKey(target.GetType()) && path.Skills[target.GetType()] < level)
                path.Skills[target.GetType()] = level;
        }

        public static void GetPathToItemRec(Item target, bool includeSkills, ref TechTreePath path, int number = 1)
        {
            var recipes = CraftingComponent.RecipesForItem(target.GetType());
            if (recipes.Any())
            {
                if (path.Items.ContainsKey(target.GetType()))
                    path.Items[target.GetType()] += number;
                else
                    path.Items.Add(target.GetType(), number);
                foreach (RequiresSkillAttribute skill in recipes.First().RequiredSkills)
                {
                    if (skill.Level <= skill.SkillItem.MaxLevel)
                    {
                        GetPathToSkillRec(skill.SkillItem, includeSkills, ref path, skill.Level);
                    }
                }
                foreach (var ingredient in recipes.First().Ingredients)
                    GetPathToItemRec(ingredient.Item, includeSkills, ref path, (int)ingredient.Quantity.GetBaseValue * number);
            }
            else
            {
                if (path.BaseItems.ContainsKey(target.GetType()))
                    path.BaseItems[target.GetType()] += number;
                else
                    path.BaseItems.Add(target.GetType(), number);
            }
        }
    }
}