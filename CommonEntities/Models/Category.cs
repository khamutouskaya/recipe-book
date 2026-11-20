using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities.Models;

namespace CommonEntities.Models
{
    [Serializable]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeCategory> Recipes { get; set; }

        public Category()
        {
            Id = 0;
            Name = string.Empty;
            Recipes = new List<RecipeCategory>();
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            Recipes = new List<RecipeCategory>();
        }
    }
}
