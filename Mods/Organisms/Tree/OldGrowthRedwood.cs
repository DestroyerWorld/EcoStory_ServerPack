namespace Eco.Mods.Organisms
{
    using System;
    using System.Collections.Generic;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.World.Blocks;
    
    public partial class OldGrowthRedwood : TreeEntity
    {
        public partial class OldGrowthRedwoodSpecies : TreeSpecies
        {
            public override void PostInit()
            {
                // Lifetime
                this.TreeHealth = 200f;
                this.LogHealth = 2f;
                // Resources
                this.ChanceToSpawnDebris = 0.4f;
                this.ExperienceMultiplier = 20f;
                // Visuals
                this.BranchingDef = new List<TreeBranchDef>()
                {
                    new TreeBranchDef() { Name = "Branch0", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch1", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch2", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch3", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch4", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch5", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch6", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch7", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Branch8", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                    new TreeBranchDef() { Name = "Attach_Tip", Health = 3f, LeafPoints = 3, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                };
                this.TopBranchLeafPoints = 3;
                this.TopBranchHealth = 3;
                this.BranchRotations = null;
                this.RandomYRotation = false;
                this.BranchCount = new Range(10f, 10f);
                this.BlockType = typeof(TreeBlock);
                this.DebrisType = typeof(OldGrowthRedwoodTreeDebrisBlock);
                this.DebrisResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(4, 5) },
                    { typeof(RedwoodSeedItem), new Range(0, 1) },
                };
                this.TrunkResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(18, 20) }
                };
                this.XZScaleRange = new Range(.7f, 1.2f);
                this.YScaleRange = new Range(.7f, 1.2f);
                this.Density = 550f;
            }
        }

        public override void RandomizeAge()
        {
            // old growth redwoods do not grow
            this.GrowthPercent = 1f;
            this.YieldPercent = 1f;
        }

        [OnDeserialized]
        void OnDeserialized()
        {
            // migration - fix spawned ages of redwoods
            this.GrowthPercent = 1f;
            this.YieldPercent = 1f;
        }
    }
}