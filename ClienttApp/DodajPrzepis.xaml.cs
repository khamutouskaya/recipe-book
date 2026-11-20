using ClienttApp.Transport;
using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DodajPrzepis : Window
    {
        Recipe recipe;

        public DodajPrzepis(Recipe recipe)
        {
            InitializeComponent();

            this.recipe = recipe;

            double cab = 0;
            double prot = 0;
            double fat = 0;
            double cal = 0;
            if (recipe != null && recipe.Id != 0)
            {
                txtNazwaPrzepisu.Text = recipe.Name;
                txtOpis.Text = recipe.Description;

                btnDelete.Visibility = Visibility.Visible;


                foreach(var ingridient in recipe.Ingridients)
                {
                    cab += ingridient.Product.Cab * ingridient.Count / 100;
                    prot += ingridient.Product.Protein * ingridient.Count / 100;
                    fat += ingridient.Product.Fat * ingridient.Count / 100;
                    cal += ingridient.Product.Calories * ingridient.Count / 100;

                    ListIngridients.Items.Add(ingridient.Product.Name + " - " + ingridient.Count + " " + ingridient.Product.MetricType);
                }

                

            }

            txtBJU.Text =
                    "Calories: " + cal + "kkal" + "\n" +
                    "Protein: " + prot + "g" + "\n" +
                    "Fats: " + fat + "g" + "\n" +
                    "Cab: " + cab + "g";

            ListIngridients_SelectionChanged(null, null);
        }

        private void BtnZapisz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwaPrzepisu.Text.Length == 0)
            {
                //ADD OBRABOTKA 
                return;
            }

            if(ListIngridients.Items.Count == 0)
            {
                return;
            }

            if (txtOpis.Text.Length == 0)
            {
                return;
            }


            recipe.Name = txtNazwaPrzepisu.Text;
            recipe.Description = txtOpis.Text;

            ClientConnectionModule clientConnectionModule = new ClientConnectionModule();
            if (recipe.Id == 0)
            {
                clientConnectionModule.CreateRecipe(recipe);
            }
            else
            {
                clientConnectionModule.UpdateRecipe(recipe);
            }
            
            Close();
        }

        private void btnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListIngridients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnDeleteIngridient.IsEnabled = ListIngridients.SelectedItems.Count != 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (recipe.Id == 0)
            {
                return;
            }

            ClientConnectionModule clientConnectionModule = new ClientConnectionModule();
            clientConnectionModule.DeleteRecipe(recipe.Id);

            Close();
        }
        private void ButtonAddIngridient_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DodajSkladnik(recipe);
            dialog.ShowDialog();

            if(dialog.Result != null)
            {
                recipe.Ingridients.Add(dialog.Result);
                ListIngridients.Items.Add(dialog.Result.Product.Name + " - " + dialog.Result.Count + " " + dialog.Result.Product.MetricType);
                double cab = 0;
                double prot = 0;
                double fat = 0;
                double cal = 0;
                foreach (var ingridient in recipe.Ingridients)
                {
                    cab += ingridient.Product.Cab * ingridient.Count / 100;
                    prot += ingridient.Product.Protein * ingridient.Count / 100;
                    fat += ingridient.Product.Fat * ingridient.Count / 100;
                    cal += ingridient.Product.Calories * ingridient.Count / 100;
                 }

                txtBJU.Text =
                    "Calories: " + cal + "kkal" + "\n" +
                    "Protein: " + prot + "g" + "\n" +
                    "Fats: " + fat + "g" + "\n" +
                    "Cab: " + cab + "g";
            }
        }

        private void BtnDeleteIngridient_Click(object sender, RoutedEventArgs e)
        {
            if (ListIngridients.SelectedIndex == -1)
                return;

            recipe.Ingridients.RemoveAt(ListIngridients.SelectedIndex);
            ListIngridients.Items.RemoveAt(ListIngridients.SelectedIndex);

            double cab = 0;
            double prot = 0;
            double fat = 0;
            double cal = 0;
            foreach (var ingridient in recipe.Ingridients)
            {
                cab += ingridient.Product.Cab * ingridient.Count / 100;
                prot += ingridient.Product.Protein * ingridient.Count / 100;
                fat += ingridient.Product.Fat * ingridient.Count / 100;
                cal += ingridient.Product.Calories * ingridient.Count / 100;
            }

            txtBJU.Text =
                "Calories: " + cal + "kkal" + "\n" +
                "Protein: " + prot + "g" + "\n" +
                "Fats: " + fat + "g" + "\n" +
                "Cab: " + cab + "g";
        }
    }
}
