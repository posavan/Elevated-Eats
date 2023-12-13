using System.Collections.Generic;

namespace Capstone.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; } = 0;
        public string IngredientName { get; set; }
        public string Quantity { get; set; } = "";
    }

}
