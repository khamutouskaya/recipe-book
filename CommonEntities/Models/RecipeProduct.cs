using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities.Models;

namespace CommonEntities.Models
{
    [Serializable]
    public class RecipeProduct
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Recipe Recipe { get; set; }
        public Product Product { get; set; }
        public RecipeProduct()
        {
            Count = 0;
            Recipe = new Recipe();
            Product = new Product();
        }

        public RecipeProduct(int count, Recipe recipe, Product product)
        {
            this.Count = count;
            this.Recipe = recipe;
            this.Product = product;
        }
    }
}
