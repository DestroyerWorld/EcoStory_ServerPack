namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(100)]      
    public partial class CamoShirtItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Camo Shirt"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Hide from the animals!"); } }
        public override string Slot             { get { return ClothingSlot.Shirt; } }             
        public override bool Starter            { get { return false ; } }                       

        private static Dictionary<UserStatType, float> flatStats = new Dictionary<UserStatType, float>()
    {
                { UserStatType.DetectionRange, 1 }
    };
public override Dictionary<UserStatType, float> GetFlatStats() { return flatStats; }
    }

    
    [RequiresSkill(typeof(TailoringSkill), 3)]
    public class CamoShirtRecipe : Recipe
    {
        public CamoShirtRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CamoShirtItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CelluloseFiberItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent)),
                new CraftingElement<LeatherHideItem>(typeof(TailoringSkill), 30, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CamoShirtRecipe), Item.Get<CamoShirtItem>().UILink(), 1, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Camo Shirt"), typeof(CamoShirtRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    } 
}