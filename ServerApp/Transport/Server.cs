using CommonEntities;
using CommonEntities.Enums;
using CommonEntities.Models;
using ServerApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerApp.Transport
{
    public class Server
    {
        protected TCPServerProtocol protocol;

        protected ICollection<Socket> connectedUsers;

        protected virtual void Handler(object client)
        {
            Socket user = client as Socket;
            try
            { 
                while (true)
                {
                    switch (protocol.ReceiveCommand(user))
                    {
                        case Command.Disconnect:
                            {
                                return;
                            }
                        case Command.ReadAllCategories:
                            {
                                protocol.SendCollection(DBManager.Instance.CategoryRepository.FindAll(), user);
                                break;
                            }
                        case Command.ReadAllProducts:
                            {
                                protocol.SendCollection(DBManager.Instance.ProductRepository.FindAll(), user);
                                break;
                            }
                        case Command.CreateProduct:
                            {
                                var product = protocol.ReceiveObject<Product>(user);
                                DBManager.Instance.ProductRepository.Save(product);
                                break;
                            }
                        case Command.UpdateProduct:
                            {
                                var product = protocol.ReceiveObject<Product>(user);
                                DBManager.Instance.ProductRepository.Save(product); 
                                break;
                            }
                        case Command.DeleteProduct:
                            {
                                int id = int.Parse(protocol.ReceiveString(user));

                                if(DBManager.Instance.ProductRepository.FindById(id).Recipes.Count == 0)
                                DBManager.Instance.ProductRepository.DeleteById(id);
                                break;
                            }
                        case Command.ReadAllRecipesByCategory:
                            {
                                var category = protocol.ReceiveObject<Category>(user);
                                category = DBManager.Instance.CategoryRepository.FindById(category.Id);
                                List<Recipe> recipes = new List<Recipe>();
                                foreach(var recipeCategory in category.Recipes)
                                {
                                    
                                    recipes.Add(DBManager.Instance.RecipeRepository.FindById(recipeCategory.Recipe.Id));
                                }
                                protocol.SendCollection(recipes, user);
                                break;
                            }
                        case Command.CreateRecipe:
                            {
                                var recipe = protocol.ReceiveObject<Recipe>(user);
                                DBManager.Instance.RecipeRepository.Save(recipe);
                                break;
                            }
                        case Command.UpdateRecipe:
                            {
                                var recipe = protocol.ReceiveObject<Recipe>(user);
                                DBManager.Instance.RecipeRepository.Save(recipe);
                                break;
                            }
                        case Command.DeleteRecipe:
                            {
                                int id = int.Parse(protocol.ReceiveString(user));
                                DBManager.Instance.RecipeRepository.DeleteById(id);
                                break;
                            }
                        default:
                            {
                                protocol.SendAnswerFromServer(AnswerFromServer.UnknownCommand, user);
                                break;
                            }
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Client disconected");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public Server()
        {
            protocol = new TCPServerProtocol();
            connectedUsers = new List<Socket>();
        }

        public void openConnection()
        {
            while (true)
            {
                Socket connectedUserInfo = protocol.AcceptConnectionRequest();
                Console.WriteLine("New user connected!");
                ThreadPool.QueueUserWorkItem(Handler, connectedUserInfo);
            }
        }
    }
}
