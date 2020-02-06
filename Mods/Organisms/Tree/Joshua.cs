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
    using Eco.World;
    using Eco.World.Blocks;
    
    public partial class Joshua : TreeEntity
    {
        public partial class JoshuaSpecies : TreeSpecies
        {
            public override void PostInit()
            {
                // Lifetime
                this.TreeHealth = 8f;
                this.LogHealth = 2f;
                // Resources
                this.ChanceToSpawnDebris = 0.3f;
                // Visuals
                this.BranchingDef = new List<TreeBranchDef>()
                {
                    new TreeBranchDef() { Name = "Branch0", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0.05f), GrowthEndTime = new Range(0.4f, 0.6f) },
                };
                this.TopBranchLeafPoints = 0;
                this.TopBranchHealth = 3;
                this.BranchRotations = null;
                this.RandomYRotation = true;
                this.BranchCount = new Range(1f, 1f);
                this.BlockType = new BlockType(typeof(TreeBlock));
                this.DebrisType = typeof(JoshuaTreeDebrisBlock);
                this.DebrisResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(4, 5) },
                    { typeof(JoshuaSeedItem), new Range(0, 1) },
                };
                this.TrunkResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(8, 10) }
                };
                this.XZScaleRange = new Range(.8f, 1.4f);
                this.YScaleRange = new Range(.8f, 1.4f);
                this.Density = 320f;
            }
        }
    }
}
