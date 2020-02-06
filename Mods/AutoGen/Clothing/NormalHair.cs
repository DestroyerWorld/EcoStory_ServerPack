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
    [Category("Hidden")]  
    [Weight(100)]      
    public partial class NormalHairItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Normal Hair"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr("The hair with a completely normal amount of flare."); } }
        public override string Slot             { get { return ClothingSlot.Hair; } }             
        public override bool Starter            { get { return true ; } }                       

    }

    
    [RequiresSkill(typeof(TailoringSkill), 0)]
    public class NormalHairRecipe : Recipe
    {
        public NormalHairRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<NormalHairItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FurPeltItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(NormalHairRecipe), Item.Get<NormalHairItem>().UILink(), 1, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Normal Hair"), typeof(NormalHairRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    } 
}