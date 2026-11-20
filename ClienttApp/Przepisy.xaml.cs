using ClienttApp.Transport;
using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClienttApp
{
    /// <summary>
    /// Logika interakcji dla klasy Przepisy.xaml
    /// </summary>
    public partial class Przepisy : Window
    {
        ClientConnectionModule connectionModule = new ClientConnectionModule();
        Category category;
        public Przepisy(Category category)
        {
            InitializeComponent();

            this.category = category;
            TextName.Text = category.Name;
            Update();
        }

        private void Update()
        {
            var recipes = connectionModule.ReadAllRecipesByCategory(category);

            stackContent.Children.Clear();
            foreach (var recipe in recipes)
            {
                RecipComponent recipComponent = new RecipComponent(recipe);
                recipComponent.Margin = new Thickness(0, 10, 0, 0);

                recipComponent.editHandler += OnEdit;

                stackContent.Children.Add(recipComponent);
            }   
        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.Categories.Add(new RecipeCategory() { Id = 0, Category = category });
            var dialog = new DodajPrzepis(recipe);
            dialog.ShowDialog();

            Update();
        }
        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var recipe = sender as Recipe;

            var dialog = new DodajPrzepis(recipe);
            dialog.ShowDialog();

            Update();
        }
    }
}
