// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Core.Utils;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Plants;
using Eco.Gameplay.Players;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Stats;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Utils;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Eco.Simulation.WorldLayers;
using Eco.Simulation.WorldLayers.Layers;
using Eco.World;
using Eco.World.Blocks;
using Eco.Gameplay.GameActions;
using Eco.Simulation.WorldLayers.Pushers;
using Eco.Gameplay.Systems;
using Eco.Shared.Localization;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Items;

[Serialized]
class TrunkPiece
{
    [Serialized] public Guid ID;
    [Serialized] public float SliceStart;
    [Serialized] public float SliceEnd;

    public double LastUpdateTime;
    [Serialized] public Vector3 Position;
    [Serialized] public Vector3 Velocity;
    [Serialized] public Quaternion Rotation;

    [Serialized] public bool Collected;

    public BSONObject ToUpdateBson()
    {
        var bson = BSONObject.New;
        bson["id"] = this.ID;
        bson["pos"] = this.Position;
        bson["rot"] = this.Rotation;
        bson["v"]   = this.Velocity;
        return bson;
    }

    public BSONObject ToInitialBson()
    {
        var bson = BSONObject.New;
        bson["id"] = this.ID;
        bson["start"] = this.SliceStart;
        bson["end"] = this.SliceEnd;
        bson["pos"] = this.Position;
        bson["rot"] = this.Rotation;
        bson["v"]   = this.Velocity;
        bson["collected"] = this.Collected;
        return bson;
    }
}

// gameplay version of simulations tree
[Serialized]
public class TreeEntity : Tree, IInteractableObject, IDamageable, IMinimapObject
{
    // This needs to be 5, because 5 is the max yield bonus, and 5+5=10 is the max log stack size
    private const int maxTrunkPickupSize = 5;

    public string Category { get { return Localizer.DoStr("Trees"); } }

    public int IconID { get { return 0; } }

    [Serialized] public Quaternion Rotation { get; protected set; }

    // the list of all the slices done to this trunk
    [Serialized] ThreadSafeList<TrunkPiece> trunkPieces = new ThreadSafeList<TrunkPiece>();

    public override bool UpRooted { get { return this.stumpHealth <= 0; } }

    private const float saplingGrowthPercent = 0.3f;
    public override float SaplingGrowthPercent { get { return saplingGrowthPercent; } }

    // estimated, need to get better measurements w/ and w/o Top branch
    private static readonly float[] GrowthThresholds = new float[7] { 0.20f, 0.28f, 0.38f, 0.48f, 0.57f, 0.78f, 0.95f };
    private int CurrentGrowthThreshold = 0;

    public double LastUpdateTime { get; private set; }
    float lastKeyframeTime;

    public override IEnumerable<Vector3> TrunkPositions { get { return this.trunkPieces.Where(x=>!x.Collected).Select(x=>x.Position); } }

    private ThreadSafeHashSet<Vector3i> groundHits;

    public override bool Ripe
    {
        get
        {
            if (this.Fallen && this.trunkPieces.All(piece => piece.Collected))
                return false;
            return this.GrowthPercent >= saplingGrowthPercent;
        }
    }

    public override bool GrowthBlocked
    {
        get
        {
            if (this.Fallen || this.GrowthPercent >= 1f)
                return true;
            if ((this.CurrentGrowthThreshold >= GrowthThresholds.Length) // already at max occupied spaces
                || this.GrowthPercent < GrowthThresholds[this.CurrentGrowthThreshold])
                return false;
            var block = World.GetBlock(this.Position.Round + Vector3i.Up * (this.CurrentGrowthThreshold + 1));
            if (!block.Is<Empty>() && block.GetType() != this.Species.BlockType)
                    return true; // can't grow until obstruction is removed
            return false;
        }
    }

    public override float GrowthPercent
    {
        get { return base.GrowthPercent; }
        set { base.GrowthPercent = Mathf.Clamp01(value); this.UpdateGrowthOccupancy(); }
    }

    private void UpdateGrowthOccupancy()
    {
        while (this.Species != null && !this.Fallen && this.CurrentGrowthThreshold < GrowthThresholds.Length && this.GrowthPercent >= GrowthThresholds[this.CurrentGrowthThreshold])
        {
            var block = World.GetBlock(this.Position.Round + Vector3i.Up * (this.CurrentGrowthThreshold + 1));
            if (!block.Is<Empty>() && block.GetType() != this.Species.BlockType)
            {
                base.GrowthPercent = GrowthThresholds[this.CurrentGrowthThreshold] - 0.01f;
                return; // cap growth at slightly less than threshold, can't grow until obstruction is removed
            }
            this.CurrentGrowthThreshold++;
            World.SetBlock(this.Species.BlockType, this.Position.Round + Vector3i.Up * this.CurrentGrowthThreshold);
        }
    }

    public static bool TreeRootsBlockDigging(InteractionContext context) { return Tree.TreeRootsBlockDigging(context.BlockPosition.Value); }

    private bool CanHarvest
    {
        get
        {
            // cant harvest if any branches are still alive
            return !this.branches.Where(branch => branch != null).Select(branch => branch.Health).Where(health => health != 0).Any();
        }
    }

    public INetObjectViewer Controller { get; private set; }

    #region IInteractable interface
    public float InteractDistance { get { return 2.0f; } }

    private bool CanPickup(TrunkPiece trunk)
    {
        return this.GetBasePickupSize(trunk) <= maxTrunkPickupSize;
    }

    private float resourceMultiplier { get { return ((this.Species.ResourceRange.Diff * this.GrowthPercent) + this.Species.ResourceRange.Min); } }

    private int GetBasePickupSize(TrunkPiece trunk)
    {
        return Math.Max((int)Math.Round((trunk.SliceEnd - trunk.SliceStart) * resourceMultiplier), 1);
    }

    public InteractResult OnActLeft(InteractionContext context) { return InteractResult.NoOp; }
    public InteractResult OnActRight(InteractionContext context) { return InteractResult.NoOp; }
    public InteractResult OnActInteract(InteractionContext context)
    {
        if (!this.Fallen)
            return InteractResult.NoOp;

        if (context.Parameters != null && context.Parameters.ContainsKey("id"))
        {
            this.PickupLog(context.Player, context.Parameters["id"]);
            return InteractResult.Success;
        }

        return InteractResult.NoOp;
    }

    public InteractionDesc GetActDesc(InteractionContext context) { return InteractionDesc.None; }
    #endregion

    #region IMinimapObject interface
    public string DisplayName { get { return this.Species.DisplayName; } }

    public float MinimapYaw
    {
        get
        {
            Random rand = new Random(this.ID);
            return (2.0f * Mathf.PI) * ((float)Math.Round(rand.NextDouble() * 4.0f) / 4.0f);
        }
    }
    public string SubTitle { get { return string.Empty; } }
    #endregion

    public TreeEntity(TreeSpecies species, WorldPosition3i position, PlantPack plantPack)
        : base(species, position, plantPack)
    { }

    // needed for serialization
    protected TreeEntity()
    { }

    protected override void Initialize()
    {
        base.Initialize();
        this.UpdateGrowthOccupancy();
        if (!this.Fallen)
            MinimapManager.AddMinimapObject(this);
    }

    void CheckDestroy()
    {
        // destroy the tree if it has fallen, all the trunk pieces are collected, and the stump is removed
        if (this.Fallen && this.stumpHealth <= 0 && this.trunkPieces.All(piece => piece.Collected))
            this.Destroy();
    }

    void PickupLog(Player player, Guid logID)
    {
        lock (this)
        {
            if (!this.CanHarvest)
                player.SendTemporaryError(Localizer.DoStr("Log is not ready for harvest.  Remove all branches first."));

            TrunkPiece trunk;
            trunk = this.trunkPieces.FirstOrDefault(p => p.ID == logID);
            if (trunk != null && trunk.Collected == false)
            {
                // check log size, if its too big, it can't be picked up
                if (!this.CanPickup(trunk))
                {
                    player.SendTemporaryError(Localizer.DoStr("Log is too large to pick up, slice into smaller pieces first."));
                    return;
                }

                var resourceType = this.Species.ResourceItemType;
                var resource = Item.Get(resourceType);
                int baseCount = this.GetBasePickupSize(trunk);
                var yield = resource.Yield;
                int bonusItems = yield != null ? yield.GetCurrentValueInt(player.User) : 0;
                int numItems = baseCount + bonusItems;

                var destroyLog = true;
                if (numItems > 0)
                {
                    var carried = player.User.Inventory.Carried;
                    if (!carried.IsEmpty)
                    {
                        // If we're carrying the same type, we may be able to carry this too.
                        if (carried.Stacks.First().Item.Type == resourceType)
                        {
                            var currentCarried = carried.Stacks.First().Quantity;
                            var maxCarry = resource.MaxStackSize;
                            if (currentCarried + numItems > maxCarry)
                            {
                                LocString s = Localizer.Format("You can't carry {0:n0} more {1:items} ({2} max).", numItems, resource.UILink(numItems != 1 ? ItemLinkType.ShowPlural : 0), maxCarry);
                                player.SendTemporaryError(s);
                                return;
                            }
                        }
                        else
                        {
                            LocString s = Localizer.Format("You are already carrying {0:items} and cannot pick up {1:items}.", carried.Stacks.First().Item.UILink(ItemLinkType.ShowPlural), resource.UILink(ItemLinkType.ShowPlural));
                            player.SendTemporaryError(s);

                            return;
                        }
                    }

                    if (!player.User.Inventory.TryAddItems(this.Species.ResourceItemType, numItems, player.User))
                        destroyLog = false;
                }

                if (destroyLog)
                {
                    trunk.Collected = true;
                    this.RPC("DestroyLog", logID);

                    ActionManager.ActionPerformed(new HarvestGameAction()
                    {
                        HarvestedSpecies = this.Species,
                        HarvestedStacks = (new ItemStack(Item.Get(this.Species.ResourceItemType), numItems)).SingleItemAsEnumerable(),
                        TargetObj = this,
                        TargetPos = this.Position.Round,
                        User = player.User,
                        OrganismDestroyed = !this.Ripe
                    });
                }

                this.Save();
                this.CheckDestroy();
            }
        }
    }

    #region RPCs
    [RPC]
    public void DestroyLeaf(int branchID, int leafID)
    {
        TreeBranch branch = this.branches[branchID];
        LeafBunch leaf = branch.Leaves[leafID];

        if (leaf.Health > 0)
        {
            // replicate to all clients
            leaf.Health = 0;
            this.RPC("DestroyLeaves", branchID, leafID);
        }
    }

    [RPC]
    public void DestroyBranch(int branchID)
    {
        TreeBranch branch = this.branches[branchID];

        if (branch.Health > 0)
        {
            // replicate to all clients
            branch.Health = 0;
            this.RPC("DestroyBranch", branchID);
        }

        this.Save();
    }

    private bool TrySliceTrunk(Player player, float slicePoint)
    {
        lock (this) // prevent threading issues due to multiple choppers
        {
            // find the trunk piece this is coming from
            var target = this.trunkPieces.FirstOrDefault(p => p.SliceStart < slicePoint && p.SliceEnd > slicePoint);
            if (target == null)
                return false;
            else
            {
                // if this is a tiny slice, clamp to the nearest valid size
                const float minPieceResources = 5f;
                float minPieceSize = minPieceResources / this.resourceMultiplier;
                float targetSize = target.SliceEnd - target.SliceStart;
                float targetResources = targetSize * this.resourceMultiplier;
                float newPieceSize = target.SliceEnd - slicePoint;
                float newPieceResources = newPieceSize * this.resourceMultiplier;
                if (targetResources <= minPieceResources)
                    return false; // can't slice, too small

                if (targetResources < (2 * minPieceResources))              // if smaller than 2x the min size, slice directly in half
                    slicePoint = target.SliceStart + (.5f * targetSize);
                else if (newPieceSize < minPieceSize)                       // round down to nearest slice point where the resulting block will be the size of the log
                    slicePoint = target.SliceEnd - minPieceSize;
                else if (slicePoint - target.SliceStart <= minPieceSize)    // round up
                    slicePoint = target.SliceStart + minPieceSize;

                var sourceID = target.ID;
                // slice and assign new IDs (New piece is always the back end of the source piece)
                var newPiece = new TrunkPiece()
                {
                    ID = Guid.NewGuid(),
                    SliceStart = slicePoint,
                    SliceEnd = target.SliceEnd,
                    Position = target.Position,
                    Rotation = target.Rotation,
                };
                this.trunkPieces.Add(newPiece);
                target.ID = Guid.NewGuid();
                target.SliceEnd = slicePoint;

                // ensure the pieces are listed in order
                this.trunkPieces.Sort((a, b) => a.SliceStart.CompareTo(b.SliceStart));

                // reciprocate to clients
                this.RPC("SliceTrunk", slicePoint, sourceID, target.ID, newPiece.ID);

                PlantSimEvents.OnLogChopped.Invoke(player.DisplayName);

                this.Save();
                return true;
            }
        }
    }

    [RPC]
    public void CollideWithTerrain(Player player, Vector3i position)
    {
        if (player != this.Controller)
            return;

        lock (this)
        {
            if (this.groundHits == null)
                this.groundHits = new ThreadSafeHashSet<Vector3i>();
        }

        WorldLayer playerActivity = WorldLayerManager.GetLayer(LayerNames.PlayerActivity);

        // destroy plants and spawn dirt within a radius under the hit position
        int radius = 1;
        for (int x = -radius; x <= radius; x++)
            for (int z = -radius; z <= radius; z++)
            {
                var offsetpos = position + new Vector3i(x, -1, z);
                if (!this.groundHits.Add(offsetpos))
                    continue;

                var abovepos = offsetpos + Vector3i.Up;
                var aboveblock = World.GetBlock(abovepos);
                var hitblock = World.GetBlock(offsetpos);
                if (!aboveblock.Is<Solid>())
                {
                    // turn soil into dirt
                    if (hitblock.GetType() == typeof(GrassBlock) || hitblock.GetType() == typeof(ForestSoilBlock))
                    {
                        player.SpawnBlockEffect(offsetpos, typeof(DirtBlock), BlockEffect.Delete);
                        World.SetBlock<DirtBlock>(offsetpos);
                        BiomePusher.AddFrozenColumn(offsetpos.XZ);
                    }

                    // kill any above plants
                    if (aboveblock is PlantBlock)
                    {
                        // make sure there is a plant here, sometimes world/ecosim are out of sync
                        var plant = EcoSim.PlantSim.GetPlant(abovepos);
                        if (plant != null)
                        {
                            player.SpawnBlockEffect(abovepos, aboveblock.GetType(), BlockEffect.Delete);
                            EcoSim.PlantSim.DestroyPlant(plant, DeathType.Deforestation);
                        }
                        else
                            World.DeleteBlock(abovepos);
                    }

                    if (hitblock.Is<Solid>() && World.GetBlock(abovepos).Is<Empty>() && RandomUtil.Value < this.Species.ChanceToSpawnDebris)
                    {
                        Block placedBlock = World.SetBlock(this.Species.DebrisType, abovepos);
                        player.SpawnBlockEffect(abovepos, this.Species.DebrisType, BlockEffect.Place);
                        RoomData.QueueRoomTest(abovepos);
                    }
                }
            }
    }
    #endregion

    void FellTree(INetObject killer)
    {
        // create the initial trunk piece
        var trunkPiece = new TrunkPiece() { ID = Guid.NewGuid(), SliceStart = 0f, SliceEnd = 1f,  };

        // clear tree occupancy
        if (this.Species.BlockType != null)
        {
            var treeBlockCheck = this.Position.Round + Vector3i.Up;
            while (World.GetBlock(treeBlockCheck).GetType() == this.Species.BlockType)
            {
                World.DeleteBlock(treeBlockCheck);
                treeBlockCheck += Vector3i.Up;
            }
        }

        this.trunkPieces.Add(trunkPiece);

        if (killer is Player)
        {
            this.SetPhysicsController((INetObjectViewer)killer);
            killer.RPC("YellTimber");
            Animal.AlertNearbyAnimals(this.Position, 30);
        }

        this.RPC("FellTree", trunkPiece.ID, this.resourceMultiplier);

        // break off any branches that are young
        for (int branchID = 0; branchID < this.branches.Count(); branchID++)
        {
            var branch = this.branches[branchID];
            if (branch == null)
                continue;

            var branchAge = Mathf.Clamp01((float)((this.GrowthPercent - branch.SpawnAge) / (branch.MatureAge - branch.SpawnAge)));
            if (branchAge <= .5f)
                this.DestroyBranch(branchID);
        }

        if (killer is Player)
            PlantSimEvents.OnTreeFelled.Invoke((killer as Player).DisplayName);

        //Add air pollution (minor)
        WorldLayerManager.ClimateSim.AddAirPollution(new WorldPosition3i(this.Position.XYZi), -this.Species.ReleasesCO2ppmPerDay);

        this.Save();
    }

    public bool TryApplyDamage(INetObject damager, float amount, InteractionContext context, Type damageDealer = null)
    {
        // if the tree is really young, just outright uproot and destroy it.
        if (this.GrowthPercent < saplingGrowthPercent)
        {
            EcoSim.PlantSim.DestroyPlant(this, DeathType.Harvesting);
            return false;
        }
        else if (context.Parameters == null)
            return this.TryDamageUnfelledTree(damager, amount, context);
        else if (context.Parameters.ContainsKey("stump"))
            return this.TryDamageStump(damager, amount, context);
        else if (context.Parameters.ContainsKey("branch"))
            return this.TryDamageBranch(damager, amount, context);
        else if (context.Parameters.ContainsKey("slice"))
        {
            // trying to slice the tree
            // if there are still branches, damage them instead
            for (int branchID = 0; branchID < this.branches.Length; branchID++)
            {
                var branch = this.branches[branchID];
                if (this.TryDamageBranch(branch, branchID, amount))
                    return true;
            }

            return this.TrySliceTrunk(context.Player, context.Parameters["slice"]);
        }
        else
            return false;
    }

    private bool TryDamageUnfelledTree(INetObject damager, float amount, InteractionContext context)
    {
        if (this.health > 0)
        {
            List<IAtomicAction> playerActions = new List<IAtomicAction>();
            if (damager is Player)
            {
                IAtomicAction statAction = PlayerActions.Harvest.CreateAtomicAction(((Player)damager).User, this);
                playerActions.Add(statAction);

                if (!statAction.CanApplyNonDisposing().Notify((Player)damager))
                {
                    // We only want to dispose the action if it is invalid.  Othewise we want to keep it around to possibly apply later.
                    statAction.Dispose();
                    return false;
                }
                playerActions.Add(new SimpleAtomicAction(() => (context.SelectedItem as ToolItem).AddExperience(context.Player.User, 5 * this.Species.ExperienceMultiplier, Localizer.Format("felling a {0}", this.Species.UILink()))));
            }

            MultiAtomicAction playerAction = new MultiAtomicAction(playerActions);

            // damage trunk
            this.health = Mathf.Max(0, this.health - amount);

            this.RPC("UpdateHP", this.health / this.Species.TreeHealth);

            if (this.health <= 0)
            {
                this.health = 0;
                if (!playerAction.TryApply().Success)
                    throw new Exception("Killing this tree was verified to be legal a moment ago, but is not anymore.");
                this.FellTree(damager);
                EcoSim.PlantSim.KillPlant(this, DeathType.Harvesting);
            }
            else
                playerAction.Dispose(); // Dispose the unused action

            this.Save();
            return true;
        }
        else
            return false;
    }

    private bool TryDamageStump(INetObject damager, float amount, InteractionContext context)
    {
        if (this.Fallen && this.stumpHealth > 0)
        {
            List<IAtomicAction> actions = new List<IAtomicAction>();
            if (damager is Player)
            {
                var action = PlayerActions.RemoveStump.CreateAtomicAction(((Player)damager).User, this);
                actions.Add(action);

                if (!action.CanApplyNonDisposing().Notify((Player)damager))
                {
                    // We only want to dispose the action if it is invalid.  Othewise we want to keep it around to possibly apply later.
                    action.Dispose();
                    return false;
                }
            }

            this.stumpHealth = Mathf.Max(0, this.stumpHealth - amount);

            if (this.stumpHealth <= 0)
            {
                if (!new MultiAtomicAction(actions).TryApply().Success)
                    throw new Exception("Removing this stump was verified to be legal a moment ago, but is not anymore.");

                if (World.GetBlock(this.Position.Round).GetType() == this.Species.BlockType)
                    World.DeleteBlock(this.Position.Round);
                this.stumpHealth = 0;
                //give tree resources
                Player player = (Player)damager;
                if(player != null)
                {
                    InventoryChangeSet changes = new InventoryChangeSet(player.User.Inventory, player.User);
                    var trunkResources = this.Species.TrunkResources;
                    if (trunkResources != null)
                        trunkResources.ForEach(x => changes.AddItems(x.Key, x.Value.RandInt));
                    else
                        DebugUtils.Fail("Trunk resources missing for: " + this.Species.Name);
                    changes.TryApply();
                }
                this.RPC("DestroyStump");

                // Let another plant grow here
                EcoSim.PlantSim.UpRootPlant(this);
            }
            else
                new MultiAtomicAction(actions).Dispose();

            this.Save();
            this.CheckDestroy();
            return true;
        }
        else
            return false;
    }

    private bool TryDamageBranch(INetObject damager, float amount, InteractionContext context)
    {
        int branchID = context.Parameters["branch"];
        TreeBranch branch = this.branches[branchID];

        if (context.Parameters.ContainsKey("leaf"))
        {
            int leafID = context.Parameters["leaf"];

            // damage leaf
            LeafBunch leaf = branch.Leaves[leafID];

            if (leaf.Health > 0)
            {
                List<IAtomicAction> actions = new List<IAtomicAction>();
                if (damager is Player)
                {
                    var action = PlayerActions.HarvestLeaves.CreateAtomicAction(((Player)damager).User, this);
                    actions.Add(action);

                    if (!PlayerActions.HarvestLeaves.CreateAtomicAction(((Player)damager).User, this).CanApply().Notify((Player)damager))
                    {
                        // We only want to dispose the action if it is invalid.  Othewise we want to keep it around to possibly apply later.
                        action.Dispose();
                        return false;
                    }
                }

                leaf.Health = Mathf.Max(0, leaf.Health - amount);

                if (leaf.Health <= 0)
                {
                    if (!new MultiAtomicAction(actions).TryApply().Success)
                        throw new Exception("Removing this stump was verified to be legal a moment ago, but is not anymore.");

                    leaf.Health = 0;
                    this.RPC("DestroyLeaves", branchID, leafID);
                }
                else
                    new MultiAtomicAction(actions).Dispose();
            }

            this.Save();
            return true;
        }
        else
            return this.TryDamageBranch(branch, branchID, amount);
    }

    private bool TryDamageBranch(TreeBranch branch, int branchID, float amount)
    {
        if (branch != null && branch.Health > 0)
        {
            branch.Health = Mathf.Max(0, branch.Health - amount);

            if (branch.Health <= 0)
            {
                branch.Health = 0;
                this.RPC("DestroyBranch", branchID);
                this.Save();
            }

            return true;
        }
        else
            return false;
    }

    public override void SendInitialState(BSONObject bsonObj, INetObjectViewer viewer)
    {
        base.SendInitialState(bsonObj, viewer);

        // if we have trunk pieces, send those
        var trunkInfo = BSONArray.New;
        if (this.trunkPieces.Count > 0)
        {
            foreach (var trunkPiece in this.trunkPieces)
                trunkInfo.Add(trunkPiece.ToInitialBson());
        }
        bsonObj["trunks"] = trunkInfo;

        if (this.Controller != null && this.Controller is INetObject)
            bsonObj["controller"] = ((INetObject)this.Controller).ID;

        bsonObj["mult"] = this.resourceMultiplier;
        bsonObj["density"] = this.Species.Density;
    }

    public override void SendUpdate(BSONObject bsonObj, INetObjectViewer viewer)
    {
        base.SendUpdate(bsonObj, viewer);

        if (this.Fallen && this.Controller != viewer)
        {
            BSONArray trunkInfo = BSONArray.New;
            foreach (var trunkPiece in this.trunkPieces)
            {
                if (trunkPiece.Position == Vector3.Zero)
                    continue;
                if (trunkPiece.LastUpdateTime < viewer.LastSentUpdateTime)
                    continue;

                trunkInfo.Add(trunkPiece.ToUpdateBson());
            }
            bsonObj["trunks"] = trunkInfo;
            bsonObj["time"] = this.lastKeyframeTime;
        }
    }

    public override void ReceiveUpdate(BSONObject bsonObj)
    {
        bool changed = false;
        if (!bsonObj.ContainsKey("trunks"))
            return;
        BSONArray trunks = bsonObj["trunks"].ArrayValue;
        foreach (BSONObject obj in trunks.list)
        {
            Guid id = obj["id"];
            TrunkPiece piece = this.trunkPieces.FirstOrDefault(p => p.ID == id);

            if (piece != null && (piece.Position != obj["pos"] || piece.Rotation != obj["rot"]))
            {
                piece.Position = obj["pos"];
                piece.Rotation = obj["rot"];
                piece.Velocity = obj["v"];
                piece.LastUpdateTime = TimeUtil.Seconds;
                changed = true;
            }
        }

        if (changed)
        {
            if (this.UpRooted)
                this.Position = this.trunkPieces.First().Position;
            this.lastKeyframeTime = bsonObj["time"];
            this.LastUpdateTime = TimeUtil.Seconds;
        }
    }

    public override void OnChanged()
    {
        this.LastUpdateTime = TimeUtil.Seconds;
    }

    #region mostly copied from NetPhysicsEntity
    public override bool IsRelevant(INetObjectViewer viewer)
    {
        if (viewer is IWorldObserver)
        {
            IWorldObserver observer = viewer as IWorldObserver;
            var closestWrapped = World.ClosestWrappedLocation(observer.Position, this.Position);
            var notVisibleDistance = observer.ViewDistance + Eco.Shared.Voxel.Chunk.Size;
            var v = closestWrapped - observer.Position;
            if (Mathf.Abs(v.x) >= notVisibleDistance || Mathf.Abs(v.z) >= notVisibleDistance)
            {
                if (this.Controller == null)
                    this.SetPhysicsController(viewer);
                return true;
            }
        }
        return false;
    }

    public override bool IsNotRelevant(INetObjectViewer viewer)
    {
        if (viewer is IWorldObserver)
        {
            IWorldObserver observer = viewer as IWorldObserver;
            var closestWrapped = World.ClosestWrappedLocation(observer.Position, this.Position);
            var notVisibleDistance = observer.ViewDistance + Eco.Shared.Voxel.Chunk.Size;
            var v = closestWrapped - observer.Position;
            if (Mathf.Abs(v.x) >= notVisibleDistance || Mathf.Abs(v.z) >= notVisibleDistance)
            {
                if (this.Controller != null && this.Controller.Equals(viewer))
                    this.SetPhysicsController(null);
                return true;
            }
            else
            {
                // Still relevant, Check if viewer would be a better controller
                if (this.Controller == null)
                    this.SetPhysicsController(viewer);
                else if (this.Controller != viewer && Vector2.WrappedDistance(observer.Position.XZ, this.Position.XZ) < 10f)
                {
                    IWorldObserver other = this.Controller as IWorldObserver;
                    if (Vector2.WrappedDistance(other.Position.XZ, this.Position.XZ) > 15f)
                        this.SetPhysicsController(viewer);
                }
            }
        }
        return false;
    }

    public bool SetPhysicsController(INetObjectViewer owner)
    {
        // Trees don't need physics until felled
        if (!this.Fallen)
            return false;

        if (Equals(this.Controller, owner))
            return false;

        if (this.Controller != null)
            this.Controller.RemoveDestroyAction(this.RemovePhysicsController);

        this.Controller = owner;

        if (this.Controller != null)
            this.Controller.AddDestroyAction(this.RemovePhysicsController);

        int id = owner == null ? -1 : ((INetObject)owner).ID;
        this.NetObj.Controller.RPC("UpdateController", id);

        return true;
    }

    void RemovePhysicsController()
    {
        this.SetPhysicsController(null);
    }
    #endregion

    public override bool IsUpdated(INetObjectViewer viewer)
    {
        if (this.LastUpdateTime > viewer.LastSentUpdateTime && SleepManager.Obj.AcceleratingTime) return true;
        return this.Fallen && this.trunkPieces.Any(piece => !piece.Collected) && this.LastUpdateTime > viewer.LastSentUpdateTime;
    }


    public override void Destroy()
    {
        var treeBlockCheck = this.Position.Round;
        for (int i = 0; i <= GrowthThresholds.Length; i++)
        {
            var block = World.GetBlock(treeBlockCheck);
            if (block.GetType() == this.Species.BlockType)
                World.DeleteBlock(treeBlockCheck);
            if (block.Is<Solid>())
                break;
            treeBlockCheck += Vector3i.Up;
        }
        MinimapManager.RemoveMinimapObject(this);
        base.Destroy();
    }
}
