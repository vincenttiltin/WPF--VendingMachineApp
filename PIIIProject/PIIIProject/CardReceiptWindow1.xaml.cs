using PIIIProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for CardReceiptWindow1.xaml
    /// </summary>
    public partial class CardReceiptWindow1 : Window
    {
        private List<Item> items = new List<Item>();
        private ShoppingCart cart;
        private string paymentType;
        private string fileName = "./Transactions.txt";
        private string quatitiesFile = "./Quantities.txt";

        public CardReceiptWindow1()
        {
            InitializeComponent();
        }

        public CardReceiptWindow1(List<Item> items_, ShoppingCart cart_, string paymentType_) : this()
        {
            items = items_;
            cart = cart_;
            paymentType = paymentType_;

        }

        private void btnShowReceipt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.PayWithDebitCredit(paymentType);

                string receipt = cart.PrintReceipt();
                txbReceipt.Text = receipt;

                // Write Transactions to File
                StreamWriter sr = File.AppendText(fileName);
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
            catch (Exception execption)
            {
                MessageBox.Show(execption.Message);
            }

        }
    }
}
