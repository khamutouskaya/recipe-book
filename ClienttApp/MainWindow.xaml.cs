using ClienttApp.Transport;
using CommonEntities.Models;
using System.Data;
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
    public partial class MainWindow : Window
    {
        ClientConnectionModule clientConnectionModule = new ClientConnectionModule();

        List<Category> categories = new List<Category>();

        public MainWindow()
        {
            InitializeComponent();

            categories = clientConnectionModule.ReadAllCategories();

            int i = 0;
            foreach(var category in categories)
            {
                var button = new Button();
                button.Content = category.Name;
                button.Click += Category_Click;

                Grid.SetRow(button, i % 2);
                Grid.SetColumn(button, 1 + i / 2);

                ++i;

                mainGrid.Children.Add(button);
            }
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string categoryName = button.Content.ToString(); // название категории из кнопки
               
                Przepisy przepisyWindow = new Przepisy(categories.Find(c => c.Name.Equals(categoryName)));
                przepisyWindow.ShowDialog();
            }
        }

        private void OnProductsView(object sender, RoutedEventArgs e)
        {
            ProductsView productsView = new ProductsView();
            productsView.ShowDialog();
        }
    }
}