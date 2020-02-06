// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Organisms.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.States;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.RouteProbing;
    using Eco.Simulation.Time;

    public class HuntMemoryBehavior : FunctionalBehaviorTree<Animal>.MemoryNode
    {
        // Set this function to control how the animal moves to new locations while looking for food
        public Func<Animal, BTStatus> NoNearbyFoodBehavior = _ => BTStatus.Failure;

        public HuntMemoryBehavior()
        {
            this.MemoryName = "HuntNode";
            this.Constructor = animal => Hunt(animal, this.NoNearbyFoodBehavior);
            this.StartCondition = StartHunting;
            this.StopCondition = StopHunting;
        }

        public static bool StartHunting(Animal animal) { return animal.Hunger > Brain.HungerThreshold; }
        public static bool StopHunting(Animal animal) { return animal.Hunger < Brain.HungerSatiated || animal.Alertness > Animal.FleeThreshold; }

        public static IEnumerator<BTStatus> Hunt(Animal agent, Func<Animal, BTStatus> noNearbyFoodBehavior)
        {
            while (agent.Hunger > Brain.HungerSatiated)
            {
                var agentRegion = RouteRegions.GetRegion(agent.Position.WorldPosition3i);
                var nearPrey = NetObjectManager.GetObjectsWithin(agent.Position.XZ, agent.DetectionRange).OfType<Animal>()
                    .Where(x => agent.ShouldFleeUs(x) && RouteRegions.GetRegion(x.Position.WorldPosition3i) == agentRegion)
                    .OrderBy(x => Vector3.WrappedDistanceSq(x.Position, agent.Position) + (x.AnimationState == AnimalAnimationState.Sleeping ? -200 : 0));
                foreach (var prey in nearPrey)
                {
                    var i = 0;
                    while (prey.Active)
                    {
                        Eco.Simulation.ExternalInputs.PredatorTracker.AddPredator(agent);
                        var route = AIUtilities.GetRoute(agent.Position, prey.Position, prey.Alertness < 50 ? agent.Species.WanderingSpeed : agent.Species.Speed);
                        agent.Target.SetPath(route);
                        agent.Target.Set(prey);
                        agent.NextTick = Math.Min(agent.Target.TargetTime, WorldTime.Seconds + 3);
                        i++;
                        yield return BTStatus.Running;

                        if (Vector3.WrappedDistance(agent.Position, prey.Position) < 2f || i > 4)
                        {
                            // making this look good will require a lot of work, for now just let it go
                            agent.Hunger = 0;
                            Eco.Simulation.ExternalInputs.PredatorTracker.RemovePredator(agent);
                            yield break;
                        }
                    }
                }
                yield return noNearbyFoodBehavior(agent);
            }
        }
    }
}