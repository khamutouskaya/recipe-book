using ClienttApp.Transport;
using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class DodajSkladnik : Window
    {
        private List<Product> products = new List<Product>();
        private Recipe recipe;
        public RecipeProduct Result { get; private set; }

        public DodajSkladnik(Recipe recipe)
        {
            InitializeComponent();

            this.recipe = recipe;

            ClientConnectionModule clientConnectionModule = new ClientConnectionModule();
            products = clientConnectionModule.ReadAllProducts();

            foreach (var product in products)
            {
                if(recipe.Ingridients.Find(p => p.Product.Id == product.Id) == null)
                    cmbNameProduct.Items.Add(product.Name);
            }

            Result = null;
        }
        private static void ShowNumError(string fieldLabel, TextBox tb)
        {
            MessageBox.Show($"Podaj poprawną wartość dla: {fieldLabel}.",
                            "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);

            if (tb != null)
            {
                tb.Focus();
                tb.SelectAll();
            }
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // 1) Продукт обязателен
            int selectedIndex = cmbNameProduct.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Wybierz produkt z listy.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbNameProduct.Focus();
                return;
            }

            int count = 0;
            if (!Int32.TryParse(txtCount.Text, out count))
            {
                ShowNumError("ilość", txtCount);
                return;
            }

            Result = new RecipeProduct() { Recipe = null, Id = 0, Count = count, Product = products[selectedIndex] };

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
