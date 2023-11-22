namespace PIIIProject.Models
{
    public class Item
    {
        private const double MIN_PRICE = 0.01;
        private const double MAX_PRICE = 100.00;
        private const int MIN_QUANTITY = 1;
        public static int MIN_STOCK = 0;
        public static int MAX_STOCK = 10;
        private const int EMPTY = 0;
        public static readonly int INITIAL_STOCK = MAX_STOCK / 2;

        // Backing Fields for food items
        private string _name;
        private double _price;
        private int quantity = 0;
        private int _stock;

        /* Constructors: A VendingItem must have a name and a price. Stock is optional and if not provided
         * will be automatically set to the maxium amount the Vending Machine can hold.
         */
        public Item(string name_, double price_)
        {
            Name = name_;
            Price = price_;
            Stock = MAX_STOCK;
        }

        public Item(string name_, double price_, int stock_)
        {
            Name = name_;
            Price = price_;
            Stock = stock_;
        }

        // Properties 
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentException("Error, vending item must have a name");

                _name = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < MIN_PRICE || value > MAX_PRICE)
                    throw new System.ArgumentException("Error, items cannot cost less than 1.00$ and more than 100.00$");

                _price = value;
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < MIN_STOCK || value > Stock)
                    throw new System.ArgumentException("Error, quantity purchased must be atleast 1 and cannot be greater than available stock");

                quantity = value;
            }

        }

        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value < MIN_STOCK || value > MAX_STOCK)
                    throw new System.ArgumentException("Error, Vending Machine can only hold between 1-10 items");

                _stock = value;
            }
        }

        // Methods

        /* AddQuantity: This method takes in the quantity of the item the user requests in the app
         * and will increase the quantity backing field of the item.
         */
        public void AddQuantity(int quantity_)
        {
            if (quantity_ < MIN_QUANTITY)
                throw new System.ArgumentException("Error, you must purchase atleast 1 item");
            else if (quantity_ > Stock)
                throw new System.ArgumentException("Error, purchase quantity exceeds current stock");

            Quantity = quantity_;
            Stock -= quantity_;
        }

        /* RemoveQuantity: This method will take in the quantity of the item that the user wishes
         * to remove and subtract it from the Quantity Property
         */
        public void RemoveQuantity(int quantity_)
        {
            if (quantity_ < MIN_QUANTITY)
                throw new System.ArgumentException("Error, cannot remove less than 1 item");
            else if (Quantity - quantity_ <= EMPTY)
                throw new System.ArgumentException("Error, quantity of selected item is 0");

            Quantity -= quantity_;
            Stock += quantity_;
        }

        public override string ToString()
        {
            return $"{Quantity}-{Name} -- ${Price}";
        }

    }
}
