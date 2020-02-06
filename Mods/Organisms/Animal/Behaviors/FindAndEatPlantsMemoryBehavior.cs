// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Organisms.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Shared.Math;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.RouteProbing;
    using Eco.Simulation.Time;

    /// <summary>
    /// An enumerator gets instantiated per-animal while seeking food and deleted when the animal is satisfied. 
    /// </summary>
    public class FindAndEatPlantsMemoryBehavior : FunctionalBehaviorTree<Animal>.MemoryNode
    {
        // Example for complex behaviors that retain temporary data between ticks
        //    yields pause execution until next tick which stores state info for states (looking for food / walking to food / eating)
        //    enumerator data - list of food sources to visit

        // Set this function to control how the animal moves to new locations while looking for food
        public Func<Animal, BTStatus> NoNearbyFoodBehavior = _ => BTStatus.Failure;
        public float HungerRestoredPerEat = 10;

        public FindAndEatPlantsMemoryBehavior()
        {
            this.MemoryName = "EatNode";
            this.Constructor = animal => FoodFinder(animal, this.NoNearbyFoodBehavior, this.HungerRestoredPerEat);
            this.StartCondition = StartEating;
            this.StopCondition = StopEating;
        }

        public static bool StartEating(Animal animal) { return animal.Hunger > Brain.HungerThreshold; }
        public static bool StopEating(Animal animal) { return animal.Hunger < Brain.HungerSatiated || animal.Alertness > Animal.FleeThreshold; }

        public static IEnumerator<BTStatus> FoodFinder(Animal agent, Func<Animal, BTStatus> noNearbyFoodBehavior, float hungerRestored)
        {
            var lastPos = Vector3.Zero;
            Queue<Vector3> foodSources = new Queue<Vector3>();

            while (agent.Hunger > Brain.HungerSatiated)
            {
                foodSources.Clear();
                while (foodSources.Count == 0)
                {
                    if (Vector3.WrappedDistanceSq(lastPos, agent.Position) > 100)
                    {
                        var agentRegion = RouteRegions.GetRegion(agent.Position.WorldPosition3i);
                        foodSources.AddRange(EcoSim.PlantSim.PlantsWithinRange(agent.Position, 10, plant => agent.Species.Eats(plant.Species)
                            && RouteRegions.GetRegion(plant.Position.WorldPosition3i.Down()) == agentRegion).Shuffle().Select(plant => plant.Position + Vector3.Down));
                        lastPos = agent.Position;
                    }
                    else
                        yield return noNearbyFoodBehavior(agent);
                }

                while (foodSources.Count > 0 && agent.Hunger > Brain.HungerSatiated && Vector3.WrappedDistanceSq(lastPos, agent.Position) < 100)
                {
                    // low-effort search for the first option or any other option visited while trying to hit the first
                    Vector3 targetPlantPosition;
                    PiecewiseLinearFunction route = AIUtilities.GetRouteToAny(agent.Position, foodSources, agent.Species.WanderingSpeed, out targetPlantPosition, 100, 20, agent.Species.HeadDistance);
                    if (route == null)
                        break;
                    agent.Target.SetPath(route);
                    agent.Target.LookPos = targetPlantPosition;
                    agent.AnimationState = AnimalAnimationState.LookingForFood;
                    var target = route.EndPosition;

                    // just in case something interrupts the path
                    while (agent.Target.TargetTime > WorldTime.Seconds && agent.Target.TargetPosition == target)
                    {
                        agent.NextTick = agent.Target.TargetTime;
                        yield return BTStatus.Running;
                    }

                    if (Vector3.WrappedDistanceSq(target, agent.Position) < 4)
                    {
                        agent.AnimationState = AnimalAnimationState.Eating;
                        agent.NextTick = WorldTime.Seconds + 10;
                        agent.Hunger -= hungerRestored;
                        agent.Target.LookPos = targetPlantPosition;
                        yield return BTStatus.Running;
                    }

                    // something interrupted eating, probably need to find new food
                    if (agent.Target.TargetPosition != target)
                        break;
                }
            }
        }
    }
}