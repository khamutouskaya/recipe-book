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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienttApp
{
    public partial class ProductComponent : UserControl
    {
        Product product;
        
        public delegate void EditHandler(object sender, RoutedEventArgs e);
        public event EditHandler editHandler;

        public delegate void DeleteHandler(object sender, RoutedEventArgs e);
        public event DeleteHandler deleteHandler;

        public ProductComponent(Product product)
        {
            InitializeComponent();
            this.product = product;

            TextName.Text = product.Name + " (" + (product.Recipes.Count.ToString()) + ")";
            TextProtein.Text = product.Protein.ToString();
            TextFat.Text = product.Fat.ToString();
            TextCab.Text = product.Cab.ToString();
            TextCalories.Text = product.Calories.ToString();

            BtnDelete.IsEnabled = product.Recipes.Count == 0;
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            editHandler.Invoke(product, null);
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            deleteHandler.Invoke(product, null);
        }
    }
}
