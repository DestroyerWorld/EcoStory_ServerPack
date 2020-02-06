// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Pipes;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;
    using Eco.Simulation.WorldLayers;
    using Eco.World.Blocks;
    using System.ComponentModel;
    using World = World.World;
    using Eco.Shared.Utils;

    [Serialized]
    [Weight(30000)]
    [MaxStackSize(10)]
    [RequiresTool(typeof(ShovelItem))]
    [MakesRoads]
    public class DirtItem : BlockItem<DirtBlock>, ICanExitFromPipe
    {
        public override LocString DisplayName       { get { return Localizer.DoStr("Dirt"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Dirt"); } }
        public override bool CanStickToWalls      { get { return false; } }

        public string FlowTooltip(float flowrate) { return null; }

        public float OnPipeExit(Ray posDir, float amount)
        {
            var existingBlock = World.GetBlock(posDir.FirstPos) as EmptyBlock;
            if (existingBlock != null)
            {
                var target = World.FindPyramidPos(posDir.FirstPos);
                World.SetBlock(this.OriginType, target);
                return 1;
            }
            return 0;
        }
    }

    [Serialized]
    [Weight(30000)]
    [MaxStackSize(10)]
    [RequiresTool(typeof(ShovelItem))]
    public class SandItem : BlockItem<SandBlock>
    {
        public override LocString DisplayName       { get { return Localizer.DoStr("Sand"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Sand"); } }
        public override bool CanStickToWalls      { get { return false; } }
    }

    [RequiresTool(typeof(ShovelItem))]
    public partial class ClayItem : BlockItem<ClayBlock>
    {
    }

    [Serialized, Liquid]
    [Category("Hidden")]
    [MaxStackSize(10)]
    public class WaterItem : BlockItem<WaterBlock>, ICanExitFromPipe
    {
        public override LocString DisplayName       { get { return Localizer.DoStr("Water"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Water"); } }


        public override bool CanStickToWalls      { get { return false; } }
        public string FlowTooltip(float flowrate) { return null; }

        public float OnPipeExit(Ray posDir, float amount)
        {
            var pos = posDir.FirstPos + Vector3i.Down;
            var existingBlock = World.GetBlock(pos);
            var waterOuput = Mathf.Min((amount / WorldObjectManager.TickDeltaTime) / 1000f, .999f);

            // Set the existing block if it's there, or add a new block.
            if (existingBlock is EmptyBlock) World.SetBlock(typeof(WaterBlock), pos, waterOuput, true);
            else if (existingBlock is WaterBlock) 
            {
                (existingBlock as WaterBlock).Water = waterOuput;
                (existingBlock as WaterBlock).PipeSupplied = true;
            }
            
            return amount;
        }
    }


    [Serialized, Liquid]
    [Category("Hidden")]
    public class SewageItem : BlockItem<SewageBlock>, ICanExitFromPipe
    {
        public override LocString DisplayName       { get { return Localizer.DoStr("Sewage"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Sewage"); } }
        public override bool CanStickToWalls      { get { return false; } }
        public string FlowTooltip(float flowrate) { return null; }
        public const float SewageItemsPerPollution = 1000;

        public float OnPipeExit(Ray posDir, float amount)
        {
            WorldLayerManager.ClimateSim.AddGroundPollution(posDir.FirstPos.XZ, amount / SewageItemsPerPollution / TimeUtil.SecondsPerHour);
            return amount;
        }
    }
}