using MySql.Data.MySqlClient;
using CommonEntities.Models;
using ServerApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.DB;
using Org.BouncyCastle.Asn1.Mozilla;

namespace ServerApp.DB
{
    internal class RecipeProductRepository : MySqlRepository<RecipeProduct>
    {
        private readonly string createCommand = "INSERT INTO recipe_products (count, recipe_id, product_id) VALUES (?, ?, ?)";
        private readonly string updateCommand = "UPDATE recipe_products SET count = ?, recipe_id = ?, product_id = ? WHERE id = ?";
        private readonly string deleteCommand = "DELETE FROM recipe_products WHERE id = ?";
        private readonly string deleteByRecipeCommand = "DELETE FROM recipe_products WHERE recipe_id = ?";
        private readonly string deleteByProductCommand = "DELETE FROM recipe_products WHERE product_id = ?";
        private readonly string readCommand = "SELECT * FROM recipe_products WHERE id = ?";
        private readonly string readByRecipeCommand = "SELECT * FROM recipe_products WHERE recipe_id = ?";
        private readonly string readByProductCommand = "SELECT * FROM recipe_products WHERE product_id = ?";
        private readonly string readAllCommand = "SELECT * FROM recipe_products";

        public RecipeProductRepository(MySqlConnection connection) : base(connection)
        {
        }

        public override void DeleteById(int id) 
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            command.ExecuteNonQuery();
        }
        public void DeleteByRecipeId(int recipeId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteByRecipeCommand;
            command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = recipeId;

            command.ExecuteNonQuery();
        }
        public void DeleteByProductId(int productId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteByProductCommand;
            command.Parameters.Add("@product_id", MySqlDbType.Int64).Value = productId;

            command.ExecuteNonQuery();
        }
        public override List<RecipeProduct> FindAll()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readAllCommand;

            var reader = command.ExecuteReader();

            List<RecipeProduct> result = new List<RecipeProduct>();
            while (reader.Read())
            {
                RecipeProduct recipeProduct = new RecipeProduct();
                recipeProduct.Id = int.Parse(reader["id"].ToString());
                recipeProduct.Count = int.Parse(reader["count"].ToString());
                recipeProduct.Product.Id = int.Parse(reader["product_id"].ToString());
                recipeProduct.Recipe.Id = int.Parse(reader["recipe_id"].ToString());
             
                result.Add(recipeProduct);
            }

            reader.Close();

            return result;
        }

        public override RecipeProduct FindById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            var reader = command.ExecuteReader();
            reader.Read();
            RecipeProduct recipeProduct = new RecipeProduct();
            recipeProduct.Id = int.Parse(reader["id"].ToString());
            recipeProduct.Count = int.Parse(reader["count"].ToString());
            recipeProduct.Product.Id = int.Parse(reader["product_id"].ToString());
            recipeProduct.Recipe.Id = int.Parse(reader["recipe_id"].ToString());

            reader.Close();

            return recipeProduct;
        }
        public List<RecipeProduct> FindByRecipeId(int recipeId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readByRecipeCommand;
            command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = recipeId;

            var reader = command.ExecuteReader();

            List<RecipeProduct> result = new List<RecipeProduct>();
            while (reader.Read())
            {
                RecipeProduct recipeProduct = new RecipeProduct();
                recipeProduct.Id = int.Parse(reader["id"].ToString());
                recipeProduct.Count = int.Parse(reader["count"].ToString());
                recipeProduct.Product.Id = int.Parse(reader["product_id"].ToString());
                recipeProduct.Recipe.Id = int.Parse(reader["recipe_id"].ToString());


                result.Add(recipeProduct);
            }

            reader.Close();

            var productRepository = new ProductRepository(connection);
            foreach (var item in result)
            {
                item.Product = productRepository.FindById(item.Product.Id);
            }

            return result;
        }

        public List<RecipeProduct> FindByProductId(int productId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readByProductCommand;
            command.Parameters.Add("@product_id", MySqlDbType.Int64).Value = productId;

            var reader = command.ExecuteReader();

            List<RecipeProduct> result = new List<RecipeProduct>();
            while (reader.Read())
            {
                RecipeProduct recipeProduct = new RecipeProduct();
                recipeProduct.Id = int.Parse(reader["id"].ToString());
                recipeProduct.Count = int.Parse(reader["count"].ToString());
                recipeProduct.Product.Id = int.Parse(reader["product_id"].ToString());
                recipeProduct.Recipe.Id = int.Parse(reader["recipe_id"].ToString());


                result.Add(recipeProduct);
            }

            reader.Close();

            return result;
        }

        public override void Save(RecipeProduct entity)
        {
            if (entity.Id == 0)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = createCommand;

                command.Parameters.Add("@count", MySqlDbType.Int64).Value = entity.Count;
                command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = entity.Recipe.Id;
                command.Parameters.Add("@product_id", MySqlDbType.Int64).Value = entity.Product.Id;

                command.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = updateCommand;
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = entity.Id;
                command.Parameters.Add("@count", MySqlDbType.Int64).Value = entity.Count;
                command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = entity.Recipe.Id;
                command.Parameters.Add("@product_id", MySqlDbType.Int64).Value = entity.Product.Id;

                command.ExecuteNonQuery();
            }
        }

       
    }
}
