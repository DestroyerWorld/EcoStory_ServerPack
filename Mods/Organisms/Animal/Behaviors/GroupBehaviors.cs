// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Organisms.Behaviors
{
    using System;
    using Eco.Shared.Utils;
    using Eco.Shared.States;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.Time;
    using Eco.Shared.Math;
    using System.Collections.Generic;
    using Eco.Shared.Networking;
    using System.Linq;
    using Eco.Simulation.RouteProbing;

    // This is a pretty last-minute addition that could use some refinement, should be a good place for modders to start experimenting with animal behaviors
    public class GroupBehaviors
    {
        const string IsLeader = "Leader";   // Leader memory contains list of followers
        const string IsFollower = "Follow"; // Follower memory points to leader
        const string IsAlone = "Alone";     // Next time to check for a pack
        const float TimeBetweenHerdChecks = 20f;

        public static Func<Animal, BTStatus> StayNearLeader(float maxDistance) { return agent => FollowLeader(agent, maxDistance); }
        public static BTStatus FollowLeader(Animal agent, float maxDistance)
        {
            var leader = GetLeader(agent);
            if (leader == null || leader == agent)
                return BTStatus.Failure;

            if (Vector3.WrappedDistance(agent.Position, leader.Position) > maxDistance)
            {
                // try to stay near alpha
                return MovementBehaviors.LandMovement(agent, Vector3.WrappedDirectionalVector(leader.Target.TargetPosition, agent.Position).XZ,
                    leader.AnimationState == AnimalAnimationState.Flee ? agent.Species.Speed : agent.Species.WanderingSpeed, AnimalAnimationState.Wander);
            }
            return BTStatus.Failure;
        }

        public static BTStatus SleepNearLeader(Animal agent)
        {
            var leader = GetLeader(agent);
            if (leader == null || leader == agent)
                return BTStatus.Failure;

            if (leader.AnimationState == AnimalAnimationState.Sleeping)
            {
                // sleep near alpha
                if (Vector3.WrappedDistance(agent.Position, leader.Position) < agent.Species.HeadDistance * 3)
                {
                    agent.SetState(AnimalAnimationState.LyingDown, 10f);
                    agent.Hunger = Math.Min(agent.Hunger, 50);
                    return BTStatus.Success;
                }

                // TODO: avoid overlapping with other herd members, make the leader pick a spot to sleep that can accomidate the herd
                var route = AIUtilities.GetRouteFacingTarget(agent.Position, leader.Position, agent.Species.WanderingSpeed, agent.Species.HeadDistance * 2);
                agent.Target.SetPath(route );
                agent.NextTick = agent.Target.TargetTime;
                agent.AnimationState = AnimalAnimationState.Wander;
                return BTStatus.Success;
            }
            return BTStatus.Failure;
        }

        public static List<Animal> GetHerdList(Animal agent)
        {
            Animal leader;
            List<Animal> herdList = null;
            agent.TryGetMemory<Animal>(IsFollower, out leader);
            (leader ?? agent).TryGetMemory<List<Animal>>(IsLeader, out herdList);
            return herdList;
        }

        public static void SyncFleePosition(Animal agent)
        {
            if (agent.FleePosition != Vector3.Zero && agent.Alertness > Animal.FleeThreshold)
            {
                var herdList = GetHerdList(agent);
                if (herdList != null)
                    foreach (var other in herdList)
                        if (other.FleePosition == Vector3.Zero && Vector3.WrappedDistance(other.Position, agent.Position) < agent.DetectionRange)
                        {
                            // flee in same direction
                            if (other.Alertness < Animal.FleeThreshold)
                                other.Alertness = agent.Alertness;
                            other.FleePosition = other.Position + Vector3.WrappedDirectionalVector(agent.FleePosition, agent.Position);
                            other.NextTick = WorldTime.Seconds;
                        }
            }
        }

        public static bool HaveAdjacentHomePositions(Animal one, Animal two)
        {
            return Vector3.WrappedDistance(one.WorldHomePos.X_Z(), two.WorldHomePos.X_Z()) < 2 * one.Species.VoxelsPerEntry;
        }

        // Groups form and dissolve along with animal visibility
        public static Animal GetLeader(Animal agent)
        {
            float checkTime;
            if (agent.TryGetMemory<float>(IsAlone, out checkTime))
            {
                if (TimeUtil.Seconds < checkTime)
                    return agent;

                agent.RemoveMemory(IsAlone);
            }

            List<Animal> followers;
            if (agent.TryGetMemory<List<Animal>>(IsLeader, out followers))
            {
                followers.RemoveAll(x => x == null || x.Dead);

                // this animal is a leader as long as it has followers
                if (followers.Count > 0)
                    return agent;
                else
                    agent.RemoveMemory(IsLeader);
            }

            Animal leader;
            if (!agent.TryGetMemory<Animal>(IsFollower, out leader))
            {
                var agentRegion = RouteRegions.GetRegion(agent.Position.WorldPosition3i);
                var nearAnimals = NetObjectManager.GetObjectsWithin(agent.Position.XZ, agent.Species.VoxelsPerEntry).OfType<Animal>()
                    .Where(x => !x.Dead && x.Visible && x.Species == agent.Species && x != agent
                    && HaveAdjacentHomePositions(agent, x) // don't join a herd far from home
                    && RouteRegions.GetRegion(x.Position.WorldPosition3i) == agentRegion);

                if (!nearAnimals.Any())
                {
                    agent.SetMemory(IsAlone, TimeUtil.Seconds + TimeBetweenHerdChecks);
                    return agent;
                }

                List<Animal> stragglers = new List<Animal>();
                foreach (var nearAnimal in nearAnimals)
                {
                    Animal tmpLeader;
                    if (nearAnimal.TryGetMemory<Animal>(IsFollower, out tmpLeader))
                    {
                        if (leader == null && tmpLeader.Visible && !tmpLeader.Dead && HaveAdjacentHomePositions(tmpLeader, agent))
                            leader = tmpLeader;
                    }
                    else if (nearAnimal.HasMemory(IsLeader))
                    {
                        if (leader == null)
                            leader = nearAnimal;
                    }
                    else
                        stragglers.Add(nearAnimal);
                }

                if (leader == null)
                {
                    // become the leader
                    leader = agent;
                    agent.SetMemory(IsLeader, stragglers);
                }
                else
                {
                    // join the pack along w/ any other stragglers
                    stragglers.Add(agent);

                    if (!leader.TryGetMemory<List<Animal>>(IsLeader, out followers))
                    {
                        followers = new List<Animal>();
                        leader.SetMemory(IsLeader, followers);
                    }

                    foreach (var newMember in stragglers)
                        followers.Add(newMember);
                }

                foreach (var newMember in stragglers)
                    newMember.SetMemory(IsFollower, leader);
            }
            else
            {
                if (leader == null || !leader.Visible || leader.Dead)
                {
                    agent.RemoveMemory(IsFollower);
                    return agent;
                }
            }
            return leader;
        }
    }
}
