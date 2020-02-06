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
    using System.IO;
    using Eco.Mods.TechTree;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.Core.Tests;

    public class TechTreeSimData
    {
        public List<Type> curSkills = new List<Type>();
        public List<Recipe> curRecipes = new List<Recipe>();

        public List<Recipe> unusableRecipes = new List<Recipe>();
        public List<Type> craftableItems = new List<Type>();
        public List<Type> craftingTables = new List<Type>();
        public Queue<Type> skillsToEvaluate = new Queue<Type>();

        public void AddSkill(StreamWriter sw, Type skill)
        {
            if ((Item.Get(skill) as Skill).IsSpecialty && !(Item.Get(skill) as Skill).IsRoot)
            {
                skillsToEvaluate.Enqueue(skill);
                foreach (var tree in SkillTree.SpecialtyTreeFromSkill(skill).ChildrenRecursive())
                    skillsToEvaluate.Enqueue(tree.StaticSkill.Type);
            }
        }
    }

    public class TechTreeSim : IChatCommandHandler
    {
        [CITest]
        [ChatCommand("Simulates the tech tree", ChatAuthorizationLevel.Developer)]
        public static void TechTreeSimulation(User user)
        {
            using (StreamWriter sw = new StreamWriter("TechTreeSimulation.txt"))
            {
                var data = new TechTreeSimData();
                sw.WriteLine("Starting Tech Tree Sim");
                
                sw.WriteLine("Getting Starting Skills...");
                var introSkills = PlayerDefaults.GetDefaultSkills().ToList();
                introSkills.ForEach(x => data.AddSkill(sw, x));
                
                sw.WriteLine("Getting Initial Tables...");
                data.craftingTables.Add(typeof(CampsiteObject));
                
                sw.WriteLine("Getting Recipes with No Skills...");
                var temp = Recipe.AllRecipes.Where(x => !x.RequiredSkills.Any() && x.Ingredients.Any());
                data.unusableRecipes.AddRange(Recipe.AllRecipes.Where(x => !x.RequiredSkills.Any() && x.Ingredients.Any()));
                
                sw.WriteLine("Getting World Resources...");
                //manually defined for now since theres no easy way of checking for these
                data.craftableItems.AddRange(new List<Type>()
                {
                    typeof(DirtItem),
                    typeof(SandItem),
                    typeof(StoneItem),
                    typeof(IronOreItem),
                    typeof(CopperOreItem),
                    typeof(GoldOreItem),
                    typeof(CoalItem)
                });
                
                sw.WriteLine("Getting Species Resources...");
                var seedless = new List<Species>();
                var resourceless = new List<Species>();
                EcoSim.AllSpecies.ForEach(x =>
                {
                    sw.WriteLine("Adding Species: " + x.DisplayName);
                    int resources = 0;
                    foreach(var resource in x.ResourceList)
                    {
                        var item = resource.ResourceType;
                        data.craftableItems.AddUnique(item);
                        resources++;
                        sw.WriteLine(" - New Resource: " + item.Name);
                    }
                    if (resources < 1)
                        resourceless.Add(x);
                });
                sw.WriteLine("\nSimulating...\n");
                UpdateRecipes(sw, data);
                UpdateRecipesFromSkills(sw, data);
                sw.WriteLine("\nTech Tree Sim Complete");
                ChatManager.SendChat(CheckStatus(sw, data) ? "Tech Tree Complete" : "Tech Tree Failed, check the TechTreeSimulation.txt for more information.", user); //get issues with complete
                
                //PLANT DATA
                sw.WriteLine("Plants Missing Resources");
                sw.WriteLine(String.Join(",", resourceless));

                //CRAFTABLES
                sw.WriteLine("\nUncraftable Accessed");
                foreach(var recipe in data.unusableRecipes)
                {
                    sw.WriteLine("  " + recipe.DisplayName);
                    recipe.Ingredients.ForEach(x => sw.WriteLine("    I: " + x.Item.DisplayName));
                    recipe.Products.ForEach(x => sw.WriteLine("    P: " + x.Item.DisplayName));

                    if (!data.craftableItems.Contains(recipe.Ingredients.Select(x => x.Item.Type)))
                        sw.WriteLine("    missing ingredients");
                    if (!CraftingComponent.TablesForRecipe(recipe.GetType()).Intersect(data.craftingTables).Any())
                        sw.WriteLine("    missing crafting table");
                }
                sw.WriteLine("\nUncraftable Unaccessed");
                foreach (var recipe in Recipe.AllRecipes.Except(data.curRecipes))
                {
                    sw.WriteLine("  " + recipe.DisplayName);
                    recipe.Ingredients.ForEach(x => sw.WriteLine("    I: " + x.Item.DisplayName));
                    recipe.Products.ForEach(x => sw.WriteLine("    P: " + x.Item.DisplayName));

                    if (!data.curSkills.Contains(recipe.RequiredSkills.Select(x => x.SkillType)))
                        sw.WriteLine("    missing skills");
                    if (!data.craftableItems.Contains(recipe.Ingredients.Select(x => x.Item.Type)))
                        sw.WriteLine("    missing ingredients");
                    if (!CraftingComponent.TablesForRecipe(recipe.GetType()).Intersect(data.craftingTables).Any())
                        sw.WriteLine("    missing crafting table");
                }

                //ALL UNOBTAINABLE ITEMS
                sw.WriteLine("\nUnobtainable Items");
                foreach (var item in Item.AllItems
                    .Where(x => !(x is Skill) && !(x is ActionbarItem) && !(x is SkillScroll) && !x.Hidden)
                    .Select(x => x.Type)
                    .Except(data.craftableItems))
                {
                    sw.WriteLine("  " + item.Name);
                }
            }
        }

        private static bool CheckStatus(StreamWriter sw, TechTreeSimData data)
        {
            StringBuilder s = new StringBuilder();
            if (!data.craftingTables.Contains(typeof(LaserObject)))
                s.AppendLine("Error: Laser Unobtainable");
            if(data.unusableRecipes.Any())
                s.AppendLine("Error: Visible Recipes Uncraftable");
            sw.Write(s.ToString());
            if (s.Length > 0)
                return false;
            else
                return true;
        }

        private static void UpdateRecipesFromSkills(StreamWriter sw, TechTreeSimData data)
        {
            while (data.skillsToEvaluate.Any())
            {
                var skill = data.skillsToEvaluate.Dequeue();
                data.curSkills.Add(skill);
                sw.WriteLine("Adding Skill: " + skill.Name);
                foreach (var recipe in Recipe.GetRecipesBySkill(skill))
                    data.unusableRecipes.Add(recipe);
                UpdateRecipes(sw, data);
            }
        }

        private static void UpdateRecipes(StreamWriter sw, TechTreeSimData data)
        {
            var numRemoved = 1;
            while(numRemoved != 0)
            {
                numRemoved = 0;
                var toRemove = new List<Recipe>();
                foreach(var recipe in data.unusableRecipes)
                {
                    if (data.craftableItems.Contains(recipe.Products.Select(x => x.Item.Type)))
                        toRemove.Add(recipe);
                    else if (data.craftableItems.Contains(recipe.Ingredients.Select(x => x.Item.Type)) && 
                             CraftingComponent.TablesForRecipe(recipe.GetType()).Intersect(data.craftingTables).Any())
                    {
                        foreach(var product in recipe.Products.Select(x => x.Item))
                        {
                            data.craftableItems.AddUnique(product.Type);
                            sw.WriteLine(" - New Craftable Items: " + product);
                            if (product is WorldObjectItem)
                                data.craftingTables.AddUnique((product as WorldObjectItem).WorldObjectType); //will need to specify power items
                            if (product is SkillBook)
                                data.AddSkill(sw, (product as SkillBook).Skill.Type);
                        }
                        toRemove.Add(recipe);
                        numRemoved++;
                    }
                }
                toRemove.ForEach(x =>
                {
                    data.unusableRecipes.Remove(x);
                    data.curRecipes.Add(x);
                });
            }
        }
    }
}