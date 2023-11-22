using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PIIIProject.Models;
using System.IO;


namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        private List<Item> items = new List<Item>();
        private ShoppingCart cart;
        private string paymentType;
        private string transactionsFile = "./Transactions.txt";
        private string quatitiesFile = "./Quantities.txt";

        public ReceiptWindow()
        {
            InitializeComponent();
           
        }

        public ReceiptWindow(List<Item> items_, ShoppingCart cart_, string paymentType_) :this()
        {
           items = items_;
           cart = cart_;
           paymentType = paymentType_;

        }

        private void btnShowReceipt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string radioButtonValue = "";
                double amount;

                IEnumerable<RadioButton> myRadioButtons = stkRadioButtons.Children.OfType<RadioButton>().Where(x => x.IsChecked.Value);

                foreach (RadioButton radioButton in myRadioButtons)
                {
                    if (radioButton.IsChecked == true)
                        radioButtonValue = radioButton.Content.ToString();
                }

                amount = double.Parse(radioButtonValue);
                cart.PayWithCash(paymentType, amount);

                string receipt = cart.PrintReceipt();
                txbReceipt.Text = receipt;

                // Write Transactions to File
                StreamWriter sr = File.AppendText(transactionsFile);
                sr.WriteLine(receipt);
                sr.WriteLine("------------------------------------------ ");
                sr.Close();

                //Write Quantities to File
                StreamWriter streamWriter = File.AppendText(quatitiesFile);

                foreach (Item item in items)
                {
                    streamWriter.WriteLine(item.Stock);
                }
                streamWriter.WriteLine("------------------------------------------");
                streamWriter.Close();


            }
            catch(Exception execption)
            {
                MessageBox.Show(execption.Message); 
            }
        }
    }
}
