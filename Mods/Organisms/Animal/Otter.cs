namespace Eco.Mods.Organisms
{
    using System;
    using System.Collections.Generic;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Gameplay.Animals;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.Types;
    using Eco.Simulation.WorldLayers;
    using Eco.Mods.TechTree;

    public class Otter : AnimalEntity
    {
        public Otter(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class OtterSpecies : AnimalSpecies
        {
            public OtterSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Otter);

                // Info
                this.Name = "Otter";
                this.DisplayName = Localizer.DoStr("Otter");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(Clam), typeof(Urchin), typeof(Tuna), typeof(Salmon), typeof(Trout)};
                this.CalorieValue = 50f;
                // Movement
                this.Flying = false;
                this.Swimming = true;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(OtterCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(Brain<Otter>);
                this.WanderingSpeed = 1f;
                this.Speed = 2f;
                this.Health = 1f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 1f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }

            // Otters can spawn on land or water
            public override bool IsValidSpawnPosition(Vector3i pos) { return true; }
        }

        // Otter specific behavior
        private bool floating;
        private class BT : FunctionalBehaviorTree<Otter> { }
        public static readonly Func<Otter, BTStatus> OtterTreeRoot;
        public static readonly Func<Otter, BTStatus> OtterLandTree;
        public static readonly Func<Otter, BTStatus> OtterFloatingTree;
        public static readonly Func<Otter, BTStatus> OtterDivingTree;
        static Otter()
        {
            const float MinIdleTime = 3f;
            const float MaxIdleTime = 10f;
            const float MinFloatIdleTime = 10f;
            const float MaxFloatIdleTime = 20f;
            const float ChanceToIdle = 0.3f;
            const float ChanceToSurface = 0.3f;
            const float ChanceToDive = 0.1f;
            const float TimeToStopFloating = 2f;
            const float DiveAlertness = 200;
            const float MaxHeightAboveSeaLevel = 5;

            OtterLandTree =
                BT.If(x => !World.World.IsUnderwater(x.Position.WorldPosition3i),
                    BT.Selector(
                        MovementBehaviors.AmphibiousFlee,
                        BT.If(x => RandomUtil.Chance(ChanceToIdle),
                            Brain.PlayAnimation(AnimalAnimationState.Idle, _ => RandomUtil.Range(MinIdleTime, MaxIdleTime))),
                        BT.If(x => x.Position.y > WorldLayerManager.ClimateSim.State.SeaLevel + MaxHeightAboveSeaLevel,
                            x => MovementBehaviors.RouteToWater(x, x.Species.WanderingSpeed, AnimalAnimationState.Wander)),
                        BT.If(MovementBehaviors.ShouldReturnHome,
                            MovementBehaviors.AmphibiousWanderHome),
                        MovementBehaviors.AmphibiousWander));

            OtterFloatingTree =
                BT.If(x => x.floating,
                    BT.Selector(
                        BT.If(x => x.AnimationState == AnimalAnimationState.FloatingIdle,
                            // floating otters need time to flip back over before doing other things
                            Brain.PlayFixedTimeAnimation(AnimalAnimationState.SurfaceSwimming, TimeToStopFloating)),
                        BT.If(x => x.Alertness > DiveAlertness,
                            // dive when scared
                            BT.Success(x => x.floating = false),
                            BT.Selector(MovementBehaviors.SwimFlee, MovementBehaviors.SwimWander)),
                        MovementBehaviors.AmphibiousFlee,
                        BT.If(x => RandomUtil.Chance(ChanceToDive),
                            BT.Success(x => x.floating = false),
                            MovementBehaviors.SwimWander),
                        BT.If(MovementBehaviors.ShouldReturnHome,
                            MovementBehaviors.AmphibiousWanderHome),
                        BT.If(x => RandomUtil.Chance(ChanceToIdle),
                            Brain.PlayAnimation(AnimalAnimationState.FloatingIdle, _ => RandomUtil.Range(MinFloatIdleTime, MaxFloatIdleTime))),
                        MovementBehaviors.AmphibiousWander));

            OtterDivingTree =
                BT.Selector(
                    MovementBehaviors.SwimFlee,
                    BT.If(MovementBehaviors.ShouldReturnHome,
                        BT.Success(x => x.floating = true),
                        MovementBehaviors.SwimWanderHomeSurface),
                    BT.If(x => x.Alertness < DiveAlertness && RandomUtil.Chance(ChanceToSurface),
                        BT.Success(x => x.floating = true),
                        MovementBehaviors.SwimWanderSurface),
                    BT.If(x => RandomUtil.Chance(1 - ChanceToIdle),
                        MovementBehaviors.SwimWander),
                    Brain.PlayAnimation(AnimalAnimationState.SwimmingIdle, _ => RandomUtil.Range(MinIdleTime, MaxIdleTime)));

            OtterTreeRoot =
                BT.Selector(
                    OtterLandTree,
                    // TODO: dive for food & eat on surface (need tummy eating animation)
                    OtterFloatingTree,
                    OtterDivingTree
                );

            Brain<Otter>.SharedBehavior = OtterTreeRoot;
        }
    }
}
