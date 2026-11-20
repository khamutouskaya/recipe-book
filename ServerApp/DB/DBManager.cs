using ClientApp.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp.DB
{
    internal class DBManager
    {
        static private string connectionString = "server=127.0.0.1;uid=root;pwd=root;database=recipe_database;";//ссылка к сервису бд 
        static private MySqlConnection connection = null; //хранит инфо и подключении (порт, протокол, методы)

        static private DBManager instance = null;//гарантия единсвтенного подкючения
        public CategoryRepository CategoryRepository { get; set; }
        public ProductRepository ProductRepository { get; set; }
        public RecipeProductRepository RecipeProductRepository { get; set; }
        public RecipeCategoryRepository RecipeCategoryRepository { get; set; }
        public RecipeRepository RecipeRepository { get; set; }
        //добавить репозитории 

        private DBManager()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            CategoryRepository = new CategoryRepository(connection);
            ProductRepository = new ProductRepository(connection);
            RecipeProductRepository = new RecipeProductRepository(connection);
            RecipeCategoryRepository = new RecipeCategoryRepository(connection);
            RecipeRepository = new RecipeRepository(connection);
            //добавить репо
        }

        public static DBManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBManager();

                return instance;
            }
        }
    }
}