using MySql.Data.MySqlClient;
using ServerApp.DB;
using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.DB
{
    internal class ProductRepository : MySqlRepository<Product>
    {
        private readonly string createCommand = "INSERT INTO products (name, calories, protein, fat, cab, metric_type) VALUES (?, ?, ?, ?, ?, ?)";
        private readonly string updateCommand = "UPDATE products SET name = ?, calories = ?, protein = ?, fat = ?, cab = ?, metric_type = ? WHERE id = ?";
        private readonly string deleteCommand = "DELETE FROM products WHERE id = ?";
        private readonly string readCommand = "SELECT * FROM products WHERE id = ?";
        private readonly string readAllCommand = "SELECT * FROM products";

        public ProductRepository(MySqlConnection connection) : base(connection)
        {

        }

        public override void DeleteById(int id)
        {
            RecipeProductRepository recipeProductRepository = new RecipeProductRepository(connection);
            recipeProductRepository.DeleteByRecipeId(id);

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            command.ExecuteNonQuery();
        }
        public override Product FindById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            var reader = command.ExecuteReader();

            reader.Read();
            Product product = new Product();
            product.Id = int.Parse(reader["id"].ToString());
            product.Name = reader["name"].ToString();
            product.Calories = double.Parse(reader["calories"].ToString());
            product.Protein = double.Parse(reader["protein"].ToString());
            product.Cab = double.Parse(reader["cab"].ToString());
            product.Fat = double.Parse(reader["fat"].ToString());
            product.MetricType = reader["metric_type"].ToString();

            reader.Close();

            RecipeProductRepository repository = new RecipeProductRepository(connection);
            product.Recipes = repository.FindByProductId(id);

            return product;
        }

        public override List<Product> FindAll()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readAllCommand;

            var reader = command.ExecuteReader();

            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product();
                product.Id = int.Parse(reader["id"].ToString());
                product.Name = reader["name"].ToString();
                product.Calories = double.Parse(reader["calories"].ToString());
                product.Protein = double.Parse(reader["protein"].ToString());
                product.Cab = double.Parse(reader["cab"].ToString());
                product.Fat = double.Parse(reader["fat"].ToString());
                product.MetricType = reader["metric_type"].ToString();

                result.Add(product);
            }

            reader.Close();

            RecipeProductRepository repository = new RecipeProductRepository(connection);
            foreach (Product product in result)
            {
                product.Recipes = repository.FindByProductId(product.Id);
            }

            return result;
        }

        

        public override void Save(Product entity)
        {
            if (entity.Id == 0)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = createCommand;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;
                command.Parameters.Add("@calories", MySqlDbType.Double).Value = entity.Calories;
                command.Parameters.Add("@protein", MySqlDbType.Double).Value = entity.Protein;
                command.Parameters.Add("@fat", MySqlDbType.Double).Value = entity.Fat;
                command.Parameters.Add("@cab", MySqlDbType.Double).Value = entity.Cab;
                command.Parameters.Add("@metric_type", MySqlDbType.String).Value = entity.MetricType;

                command.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand command = connection.CreateCommand();
                ///update?
                command.CommandText = updateCommand;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;
                command.Parameters.Add("@calories", MySqlDbType.Double).Value = entity.Calories;
                command.Parameters.Add("@protein", MySqlDbType.Double).Value = entity.Protein;
                command.Parameters.Add("@fat", MySqlDbType.Double).Value = entity.Fat;
                command.Parameters.Add("@cab", MySqlDbType.Double).Value = entity.Cab;
                command.Parameters.Add("@metric_type", MySqlDbType.String).Value = entity.MetricType;
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = entity.Id;

                command.ExecuteNonQuery();
            }
        }

        
    }
}
