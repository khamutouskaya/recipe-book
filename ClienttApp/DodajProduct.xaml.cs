using CommonEntities.Models;
using System;
using System.Globalization;
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
using System.Xml.Linq;
using ClienttApp.Transport;

namespace ClienttApp
{
    public partial class DodajProduct : Window
    {
        Product product;
        public DodajProduct(Product product)
        {
            InitializeComponent();

            this.product = product;
            if (product != null && product.Id != 0)
            {
                txtNameProduct.Text = product.Name;
                txtCalories.Text = product.Calories.ToString();
                txtProtein.Text = product.Protein.ToString();
                txtFat.Text = product.Fat.ToString();
                txtCab.Text = product.Cab.ToString();
                txtMetricType.Text = product.MetricType;
            }
        }
        private static void ShowNumError(string fieldLabel, TextBox tb)
        {
            MessageBox.Show($"Podaj poprawną wartość dla: {fieldLabel}.",
                            "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            if (tb != null)
            {
                tb.Focus();
                tb.SelectAll();
            }
        }

        private void Zapisz_CLick(object sender, RoutedEventArgs e)
        {
            if(txtNameProduct.Text.Length == 0)
            {
                ShowNumError("Name", txtNameProduct);
                return;
            }

            int calories;
            if (txtCalories.Text.Length == 0 || !Int32.TryParse(txtCalories.Text, out calories) || calories <= 0)
            {
                ShowNumError("Calories", txtCalories);
                return;
            }

            int protein;
            if (txtProtein.Text.Length == 0 || !Int32.TryParse(txtProtein.Text, out protein) || protein <= 0)
            {
                ShowNumError("Protein", txtProtein);
                return;
            }

            int fat;
            if (txtFat.Text.Length == 0 || !Int32.TryParse(txtFat.Text, out fat) || fat <= 0)
            {
                ShowNumError("Fat", txtFat);
                return;
            }

            int cab;
            if (txtCab.Text.Length == 0 || !Int32.TryParse(txtCab.Text, out cab) || cab <= 0)
            {
                ShowNumError("Cab", txtCab);
                return;
            }

            if (txtMetricType.Text.Length == 0)
            {
                ShowNumError("MetricType", txtMetricType);
                return;
            }

            product.Name = txtNameProduct.Text;
            product.Protein = protein;
            product.Fat = fat;
            product.Cab = cab;
            product.Calories = calories;
            product.MetricType = txtMetricType.Text;

            ClientConnectionModule clientConnectionModule = new ClientConnectionModule();
            if(product.Id == 0)
            {
                clientConnectionModule.UpdateProduct(product);
            }
            else
            {
                clientConnectionModule.CreateProduct(product);
            }

            DialogResult = true;
            Close();
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
