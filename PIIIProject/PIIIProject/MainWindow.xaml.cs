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
using PIIIProject.Models;


namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // List Holding all objects at the end of purchase
        List<Item> items = new List<Item>();


        public MainWindow()
        {
            InitializeComponent();

            // Vending Machine Objects
            items.Add(new Item("Snack Bar", 2.99, Item.INITIAL_STOCK));
            items.Add(new Item("GumBall", 0.79, Item.INITIAL_STOCK));
            items.Add(new Item("Trail Mix", 1.50, Item.INITIAL_STOCK));
            items.Add(new Item("Popcorn", 5.00, Item.INITIAL_STOCK));
            items.Add(new Item("Protein Bar", 3.99, Item.INITIAL_STOCK));
            items.Add(new Item("Candy", 1.99, Item.INITIAL_STOCK));
            items.Add(new Item("Chocolate", 3.78, Item.INITIAL_STOCK));
            items.Add(new Item("Brownie", 7.00, Item.INITIAL_STOCK));
            items.Add(new Item("Chips", 2.50, Item.INITIAL_STOCK));
            items.Add(new Item("Water Bottle", 1.00, Item.INITIAL_STOCK));
            items.Add(new Item("Energy Drink", 6.33, Item.INITIAL_STOCK));
            items.Add(new Item("Soda", 1.89, Item.INITIAL_STOCK));

            updateStock();
        }

        private void updateStock()
        {
            try
            {
                stock1.Content = items[0].Stock;
                stock2.Content = items[1].Stock;
                stock3.Content = items[2].Stock;
                stock4.Content = items[3].Stock;
                stock5.Content = items[4].Stock;
                stock6.Content = items[5].Stock;
                stock7.Content = items[6].Stock;
                stock8.Content = items[7].Stock;
                stock9.Content = items[8].Stock;
                stock10.Content = items[9].Stock;
                stock11.Content = items[10].Stock;
                stock12.Content = items[11].Stock;
            }
            catch (Exception e)
            {
                throw new Exception("Error while updating stock");
            }
        }

        private void btnAddStock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string x = ((Button)sender).Uid;
                if (items[int.Parse(x)].Stock < Item.MAX_STOCK)
                {
                    items[int.Parse(x)].Stock += 1;
                    updateStock();
                }

                else
                    MessageBox.Show($"Stock cannot be greater than {Item.MAX_STOCK}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                throw new Exception("Error while adding stock");
            }
        }

        private void btnRemStock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string x = ((Button)sender).Uid;
                if (items[int.Parse(x)].Stock > Item.MIN_STOCK)
                {
                    items[int.Parse(x)].Stock -= 1;
                    updateStock();
                }
                else
                {
                    MessageBox.Show($"Stock cannot be lower than {Item.MIN_STOCK}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error while removing stock");
            }
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(q1.Text) || string.IsNullOrEmpty(q2.Text) || string.IsNullOrEmpty(q3.Text) || string.IsNullOrEmpty(q4.Text) || string.IsNullOrEmpty(q5.Text) || string.IsNullOrEmpty(q6.Text) || string.IsNullOrEmpty(q7.Text) || string.IsNullOrEmpty(q8.Text) || string.IsNullOrEmpty(q9.Text) || string.IsNullOrEmpty(q10.Text) || string.IsNullOrEmpty(q11.Text) || string.IsNullOrEmpty(q12.Text))
                    MessageBox.Show("Quantity is a required field for all products.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (int.Parse(q1.Text) < 0 || int.Parse(q2.Text) < 0 || int.Parse(q3.Text) < 0 || int.Parse(q4.Text) < 0 || int.Parse(q5.Text) < 0 || int.Parse(q6.Text) < 0 || int.Parse(q7.Text) < 0 || int.Parse(q8.Text) < 0 || int.Parse(q9.Text) < 0 || int.Parse(q10.Text) < 0 || int.Parse(q11.Text) < 0 || int.Parse(q12.Text) < 0)
                {
                    MessageBox.Show("Quantity must be positive for all products.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (int.Parse(q1.Text) > items[0].Stock || int.Parse(q2.Text) > items[1].Stock || int.Parse(q3.Text) > items[2].Stock || int.Parse(q4.Text) > items[3].Stock || int.Parse(q5.Text) > items[4].Stock || int.Parse(q6.Text) > items[5].Stock || int.Parse(q7.Text) > items[6].Stock || int.Parse(q8.Text) > items[7].Stock || int.Parse(q9.Text) > items[8].Stock || int.Parse(q10.Text) > items[9].Stock || int.Parse(q11.Text) > items[10].Stock || int.Parse(q12.Text) > items[11].Stock)
                {
                    MessageBox.Show("Quantity selected cannot exceed the amount of stock for an item.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    updateQuantity();
                    updateStock();

                    ComboBoxItem paymentTypeSelected = (ComboBoxItem)pType.SelectedItem;
                    string paymentType = paymentTypeSelected.Content.ToString();
                    ShoppingCart shoppingCart = new ShoppingCart(items, paymentType);

                    if (paymentType == "Cash")
                    {
                        ReceiptWindow receiptWindow = new ReceiptWindow(items, shoppingCart, paymentType);
                        receiptWindow.Show();
                    }

                    else
                    {
                        CardReceiptWindow1 cardReceiptWindow = new CardReceiptWindow1(items, shoppingCart, paymentType);
                        cardReceiptWindow.Show();
                    }

                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error while verifying quantities selected");
            }
        }

        private void updateQuantity()
        {
            try
            {
                items[0].Quantity = int.Parse(q1.Text);
                items[0].Stock -= int.Parse(q1.Text);
                items[1].Quantity = int.Parse(q2.Text);
                items[1].Stock -= int.Parse(q2.Text);
                items[2].Quantity = int.Parse(q3.Text);
                items[2].Stock -= int.Parse(q3.Text);
                items[3].Quantity = int.Parse(q4.Text);
                items[3].Stock -= int.Parse(q4.Text);
                items[4].Quantity = int.Parse(q5.Text);
                items[4].Stock -= int.Parse(q5.Text);
                items[5].Quantity = int.Parse(q6.Text);
                items[5].Stock -= int.Parse(q6.Text);
                items[6].Quantity = int.Parse(q7.Text);
                items[6].Stock -= int.Parse(q7.Text);
                items[7].Quantity = int.Parse(q8.Text);
                items[7].Stock -= int.Parse(q8.Text);
                items[8].Quantity = int.Parse(q9.Text);
                items[8].Stock -= int.Parse(q9.Text);
                items[9].Quantity = int.Parse(q10.Text);
                items[9].Stock -= int.Parse(q10.Text);
                items[10].Quantity = int.Parse(q11.Text);
                items[10].Stock -= int.Parse(q11.Text);
                items[11].Quantity = int.Parse(q12.Text);
                items[11].Stock -= int.Parse(q12.Text);
            }
            catch (Exception e)
            {
                throw new Exception("Error while updating quantities selected");
            }
        }
    }
}
