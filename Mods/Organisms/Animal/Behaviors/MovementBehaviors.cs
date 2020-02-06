// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Organisms.Behaviors
{
    using System.Linq;
    using Eco.Shared.Math;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.RouteProbing;
    using Eco.Simulation.Time;

    public static class MovementBehaviors
    {
        public const float FleeMinDist = 2f;
        public const float FleeMaxDist = 50f;
        public const float FleeMinAngle = 90f;
        public const float FleeMaxAngle = 360f;
        public const float GoHomeDistanceSquared = 6400; // 2 animal world layer cells (80^2)
        #region syntactic sugar
        public static BTStatus Wander(Animal agent)                 { return  LandMovement(agent, Vector2.zero, agent.Species.WanderingSpeed, AnimalAnimationState.Wander); }
        public static BTStatus SwimWander(Animal agent)             { return  Swim(agent, Vector2.zero, agent.Species.WanderingSpeed, AnimalAnimationState.Diving, false); }
        public static BTStatus SwimWanderSurface(Animal agent)      { return  Swim(agent, Vector2.zero, agent.Species.WanderingSpeed, AnimalAnimationState.SurfaceSwimming, true); }
        public static BTStatus AmphibiousWander(Animal agent)       { return  AmphibiousMovement(agent, Vector2.zero, agent.Species.WanderingSpeed, AnimalAnimationState.Wander); }

        public static bool ShouldReturnHome(Animal agent)           { return  Vector2.WrappedDistanceSq(agent.Position.XZi, agent.WorldHomePos) > GoHomeDistanceSquared; }
        public static BTStatus WanderHome(Animal agent)             { return  LandMovement(agent, agent.GetDirectionHome(), agent.Species.WanderingSpeed, AnimalAnimationState.Wander); }
        public static BTStatus SwimWanderHome(Animal agent)         { return  Swim(agent, agent.GetDirectionHome(), agent.Species.WanderingSpeed, AnimalAnimationState.WanderHome, false); }
        public static BTStatus SwimWanderHomeSurface(Animal agent)  { return  Swim(agent, agent.GetDirectionHome(), agent.Species.WanderingSpeed, AnimalAnimationState.WanderHome, true); }
        public static BTStatus AmphibiousWanderHome(Animal agent)   { return  AmphibiousMovement(agent, agent.GetDirectionHome(), agent.Species.Speed, AnimalAnimationState.WanderHome); }

        public static BTStatus Flee(Animal agent)                   { return  agent.ShouldFlee ? LandMovement(agent, agent.GetFleeDirectionWithChanceToStopFleeing(), agent.Species.Speed, AnimalAnimationState.Flee, FleeMinDist, FleeMaxDist, FleeMinAngle, FleeMaxAngle, 10) : BTStatus.Failure; }
        public static BTStatus SwimFlee(Animal agent)               { return  agent.ShouldFlee ? Swim(agent, agent.GetFleeDirectionWithChanceToStopFleeing(), agent.Species.Speed, AnimalAnimationState.Flee, false, 30) : BTStatus.Failure; }
        public static BTStatus SwimFleeSurface(Animal agent)        { return  agent.ShouldFlee ? Swim(agent, agent.GetFleeDirectionWithChanceToStopFleeing(), agent.Species.Speed, AnimalAnimationState.Flee, true, 30) : BTStatus.Failure; }
        public static BTStatus AmphibiousFlee(Animal agent)         { return  agent.ShouldFlee ? AmphibiousMovement(agent, agent.GetFleeDirectionWithChanceToStopFleeing(), agent.Species.Speed, AnimalAnimationState.Flee) : BTStatus.Failure; }
        #endregion

        public static BTStatus LandMovement(Animal agent, Vector2 direction, float speed, AnimalAnimationState state, float minDistance = 2f, float maxDistance = 20f, float minDirectionOffsetDegrees = 0, float maxDirectionOffsetDegrees = 360, int tryCount = 10)
        {
            var search = AIUtilities.FindRoute(agent.Position, 2f, 20f, direction);
            if (search != null)
            {
                var smoothed = search.LineOfSightSmooth(agent.Position);
                PiecewiseLinearFunction route = AIUtilities.ProjectRoute(smoothed, speed);
                agent.NextTick = WorldTime.Seconds + route.EndTime;
                agent.Target.SetPath(route);
                agent.AnimationState = state;
                return BTStatus.Success;
            }
            else
                return LandAnimalUnStuckOrDie(agent, speed, state);
        }

        public static BTStatus Swim(Animal agent, Vector2 direction, float speed, AnimalAnimationState state, bool surface, int tries = 10)
        {
            var targetPos = AIUtilities.FindTargetSwimPosition(agent.Position, 5.0f, 20.0f, direction, 90, 360, tries, surface);
            if (targetPos == agent.Position)
                return BTStatus.Failure;

            agent.AnimationState = state;
            agent.Target.Set(agent.Position, targetPos, speed);
            agent.NextTick = agent.Target.TargetTime;

            return BTStatus.Success;
        }

        public static BTStatus AmphibiousMovement(Animal agent, Vector2 generalDirection, float speed, AnimalAnimationState state, float minDistance = 2f, float maxDistance = 20f)
        {
            var start = agent.Position.WorldPosition3i;
            if (!World.World.IsUnderwater(start))
                start = RouteManager.NearestWalkableY(agent.Position.WorldPosition3i);

            if (!start.IsValid)
                return LandMovement(agent, generalDirection, speed, state, minDistance, maxDistance);

            if (generalDirection == Vector2.zero)
                generalDirection = Vector2.right.Rotate(RandomUtil.Range(0f, 360));
            else
                generalDirection = generalDirection.Normalized;

            var target = (agent.Position + (generalDirection * RandomUtil.Range(minDistance, maxDistance)).X_Z()).WorldPosition3i;
            if (World.World.IsUnderwater(target))
                target.y = World.World.MaxWaterHeight[target];
            else
                target = RouteManager.NearestWalkableXYZ(target, 5);

            // This is a low-effort search that includes water surface and should occasionally fail, just pick a semi-random node that was visited when it fails
            var allowWaterSearch = new AStarSearch(RouteCacheData.NeighborsIncludeWater, start, target, 30);
            if (allowWaterSearch.Status != SearchStatus.PathFound)
            {
                target = allowWaterSearch.Nodes.Last().Key;
                allowWaterSearch.GetPath(target);
            }

            if (allowWaterSearch.Path.Count < 2)
                return BTStatus.Failure;
            else if (allowWaterSearch.Status == SearchStatus.Unpathable && allowWaterSearch.Nodes.Count < RouteRegions.MinimumRegionSize && !World.World.IsUnderwater(agent.Position.WorldPosition3i))
            {
                // Search region was unexpectedly small and agent is on land, might be trapped by player construction. 
                // Try regular land movement so region checks can apply & the agent can get unstuck (or die)
                return LandMovement(agent, generalDirection, speed, state, minDistance, maxDistance);
            }

            var smoothed = allowWaterSearch.LineOfSightSmooth(agent.Position);
            PiecewiseLinearFunction route = AIUtilities.ProjectRoute(smoothed, speed);
            agent.AnimationState = state;
            agent.NextTick = WorldTime.Seconds + route.EndTime;
            agent.Target.SetPath(route);

            return BTStatus.Success;
        }

        public static BTStatus RouteToWater(Animal agent, float speed, AnimalAnimationState state)
        {
            var start = agent.Position.WorldPosition3i;

            // only works for surface start points
            if (World.World.IsUnderwater(start) || start.y != World.World.MaxYCache[start] || RouteManager.RouteToSeaMap == null)
                return BTStatus.Failure;

            if (start.IsValid)
            {
                agent.AnimationState = state;

                // get a destination a decent
                var current = start;
                for (int i = 0; i < 10; i++)
                {
                    var next = RouteManager.RouteToSeaMap[current];
                    if (next == current)
                        break;
                    current = next;
                    if (WorldPosition3i.Distance(current, start) > 10)
                        break;
                }

                var target = ((Vector2)(WorldPosition3i.GetDelta(current, start)).XZ).Rotate(RandomUtil.Range(-20, 20));
                return AmphibiousMovement(agent, target, speed, state, 10, 20);
            }

            return BTStatus.Failure;
        }

        // If the animal is stuck somewhere it shouldn't be help it escape or die if its very far from walkable terrain
        public static BTStatus LandAnimalUnStuckOrDie(Animal agent, float speed, AnimalAnimationState state)
        {
            if (!RouteManager.Ready)
            {
                // don't kill everything if pathfinding is resetting
                agent.LockNextTick(WorldTime.Seconds + 20f);
                return BTStatus.Success;
            }

            const int MaxUnStuckDistance = 20;
            var nearestValid = RouteManager.NearestWalkableXYZ(agent.Position.Round, MaxUnStuckDistance);
            if (nearestValid == agent.Position.WorldPosition3i)
                return BTStatus.Failure;

            if (!nearestValid.IsValid)
            {
                // cheating failed? time to die!
                agent.Kill(DeathType.Construction);
                return BTStatus.Success;
            }
            // ignore terrain, path directly to a valid area
            agent.Target.Set(agent.Position, (Vector3)nearestValid, speed);
            agent.LockNextTick(agent.Target.TargetTime);
            agent.AnimationState = state;
            return BTStatus.Success;
        }
    }
}
