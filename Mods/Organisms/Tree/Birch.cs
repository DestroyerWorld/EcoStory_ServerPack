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

    public partial class Birch : TreeEntity
    {
        public partial class BirchSpecies : TreeSpecies
        {
            public override void PostInit()
            {
                // Lifetime
                this.TreeHealth = 10f;
                this.LogHealth = 2f;
                // Resources
                this.ChanceToSpawnDebris = 0.3f;
                // Visuals
                this.BranchingDef = new List<TreeBranchDef>()
                {
                    new TreeBranchDef() { Name = "Branch0", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.1f, 0.15f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch1", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.15f, 0.2f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch2", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.2f, 0.25f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch3", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.25f, 0.3f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch4", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.3f, 0.35f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch5", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.35f, 0.4f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch6", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.4f, 0.45f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch7", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.45f, 0.5f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch8", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.5f, 0.55f), GrowthEndTime = new Range(0.8f, 1f) },
                    new TreeBranchDef() { Name = "Branch9", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0.55f, 0.6f), GrowthEndTime = new Range(0.8f, 1f) },
                };
                this.TopBranchLeafPoints = 1;
                this.TopBranchHealth = 3;
                this.BranchRotations = new float[] {0f, 90f, 180f, 270f};
                this.RandomYRotation = false;
                this.BranchCount = new Range(2f, 4f);
                this.BlockType = typeof(TreeBlock);
                this.DebrisType = typeof(BirchTreeDebrisBlock);
                this.DebrisResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(4, 5) },
                    { typeof(BirchSeedItem), new Range(0, 1) },
                };
                this.TrunkResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(8, 10) }
                };
                this.XZScaleRange = new Range(.8f, 1.4f);
                this.YScaleRange = new Range(.8f, 1.4f);
                this.Density = 670f;
            }
        }
    }
}
