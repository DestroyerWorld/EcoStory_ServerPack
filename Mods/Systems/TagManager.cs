// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    public static class TagManager
    {
        public static Dictionary<string, string[]> CategoryToTags = new Dictionary<string, string[]>() 
        {  
            { "Tiers", new[] { "Tier 0", "Tier 1", "Tier 2", "Tier 3", "Tier 4", "Tier 5" } } 
        };
    }
}