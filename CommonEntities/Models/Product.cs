using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities.Models;

namespace CommonEntities.Models
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Cab { get; set; }
        public string MetricType { get; set; }
        public List<RecipeProduct> Recipes { get; set; }

        public Product()
        {
            Id = 0;
            Name = string.Empty;
            MetricType = string.Empty;
            Calories = 0;
            Protein = 0;
            Fat = 0;
            Cab = 0;
            Recipes = new List<RecipeProduct>();
        }

        public Product(int id, string name, double calories, double protein, double fat, double cab, string metricType)
        {
            Id = id;
            Name = name;
            Calories = calories;
            Protein = protein;
            Fat = fat;
            Cab = cab;
            MetricType = metricType;
            Recipes = new List<RecipeProduct>();
        }
    }
}
