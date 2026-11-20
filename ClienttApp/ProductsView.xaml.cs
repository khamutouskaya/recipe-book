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
    public partial class ProductsView : Window
    {
        ClientConnectionModule connectionModule = new ClientConnectionModule();
        public ProductsView()
        {
            InitializeComponent();

            Update();
        }

        private void Update()
        {
            var products = connectionModule.ReadAllProducts();

            StackLeft.Children.Clear();
            StackRight.Children.Clear();

            bool isLeft = true;
            foreach (var product in products)
            {
                ProductComponent productComponent = new ProductComponent(product);
                productComponent.Margin = new Thickness(0, 10, 0, 0);

                productComponent.editHandler += OnEdit;
                productComponent.deleteHandler += OnDelete;

                if (isLeft)
                    StackLeft.Children.Add(productComponent);
                else
                    StackRight.Children.Add(productComponent);

                isLeft = !isLeft;
            }
        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            var dialog = new DodajProduct(new Product());
            dialog.ShowDialog();

            Update();
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var product = sender as Product;

            var dialog = new DodajProduct(product);
            dialog.ShowDialog();

            Update();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            var product = sender as Product;

            connectionModule.DeleteProduct(product.Id);

            Update();
        }
    }
}
