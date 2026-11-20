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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienttApp
{
    /// <summary>
    /// Логика взаимодействия для RecipComponent.xaml
    /// </summary>
    public partial class RecipComponent : UserControl
    {
        Recipe recipe;

        public delegate void EditHandler(object sender, RoutedEventArgs e);
        public event EditHandler editHandler;

        public RecipComponent(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;

            txtName.Text = recipe.Name;
        }
        private void OnEdit(object sender, RoutedEventArgs e)
        {
            editHandler.Invoke(recipe, null);
        }
    }
}
