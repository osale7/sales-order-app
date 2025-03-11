using System;

class Product
{
    public int Id;
    public string Name;
    public int Quantity;
    public double Price;

    public Product(int id, string name, int quantity, double price)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public void PrintProduct()
    {
        Console.Write("ID: ");
        Console.Write(Id);
        Console.Write(", Name: ");
        Console.Write(Name);
        Console.Write(", Quantity: ");
        Console.Write(Quantity);
        Console.Write(", Price: ");
        Console.WriteLine(Price);
    }
}

class Customer
{
    public int Id;
    public string Name;

    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void PrintCustomer()
    {
        Console.Write("Customer ID: ");
        Console.Write(Id);
        Console.Write(", Name: ");
        Console.WriteLine(Name);
    }
}

class Order
{
    public int OrderId;
    public Customer Customer;
    public Product Product;
    public int OrderQuantity;
    public string Status;

    public Order(int orderId, Customer customer, Product product, int quantity)
    {
        OrderId = orderId;
        Customer = customer;
        Product = product;
        OrderQuantity = quantity;
        Status = "New";

        if (product.Quantity >= quantity)
        {
            product.Quantity -= quantity;
            Console.Write("Order placed: ");
            Console.Write(quantity);
            Console.Write("x ");
            Console.Write(Product.Name);
            Console.Write(" for ");
            Console.WriteLine(Customer.Name);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not enough stock available.");
            Console.ResetColor();
            Status = "Failed";
        }
    }

    public void PrintOrder()
    {
        Console.Write("Order ID: ");
        Console.Write(OrderId);
        Console.Write(", Product: ");
        Console.Write(Product.Name);
        Console.Write(", Quantity: ");
        Console.Write(OrderQuantity);
        Console.Write(", Status: ");
        Console.WriteLine(Status);
    }
}

class Payment
{
    public string PaymentMethod;
    public double Amount;

    public Payment(string method, double amount)
    {
        PaymentMethod = method;
        Amount = amount;
    }

    public void ProcessPayment()
    {
        Console.Write("Payment of ");
        Console.Write(Amount);
        Console.Write(" made via ");
        Console.WriteLine(PaymentMethod);
    }
}

class Program
{
    static void Main()
    {
        Product[] products = {
            new Product(1, "Laptop", 5, 1000),
            new Product(2, "Mouse", 10, 20),
            new Product(3, "Keyboard", 8, 50)
        };

        while (true)
        {
            Console.WriteLine("Available Products:");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].PrintProduct();
            }

            Console.Write("Enter Customer ID: ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Customer ID. Please enter a number.");
                Console.ResetColor();
                Console.Write("Enter Customer ID: ");
            }

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();
            Customer customer = new Customer(customerId, customerName);

            Product selectedProduct = null;
            while (selectedProduct == null)
            {
                Console.Write("Enter the Product ID you want to buy: ");
                int productId;
                if (!int.TryParse(Console.ReadLine(), out productId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                    continue;
                }

                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].Id == productId)
                    {
                        selectedProduct = products[i];
                        break;
                    }
                }

                if (selectedProduct == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid product ID. Please try again.");
                    Console.ResetColor();
                }
            }

            int orderQuantity;
            while (true)
            {
                Console.Write("Enter quantity for ");
                Console.Write(selectedProduct.Name);
                Console.Write(": ");
                if (!int.TryParse(Console.ReadLine(), out orderQuantity) || orderQuantity <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid quantity. Please enter a positive number.");
                    Console.ResetColor();
                    continue;
                }

                if (orderQuantity > selectedProduct.Quantity)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Not enough stock! Available: ");
                    Console.WriteLine(selectedProduct.Quantity);
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            Order order = new Order(1, customer, selectedProduct, orderQuantity);
            order.PrintOrder();

            string paymentMethod;
            while (true)
            {
                Console.Write("Enter Payment Method (Cash/Card): ");
                paymentMethod = Console.ReadLine().Trim().ToLower();
                if (paymentMethod == "cash" || paymentMethod == "card")
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid payment method. Please enter 'Cash' or 'Card'.");
                Console.ResetColor();
            }

            Payment payment = new Payment(paymentMethod, selectedProduct.Price * orderQuantity);
            payment.ProcessPayment();

            // Separator before updating stock
            Console.WriteLine("--------------------");

            Console.WriteLine("Updated Stock:");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].PrintProduct();
            }

            // Ask if the user wants to place another order with colored options
            Console.Write("Do you want to place another order? (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("yes");
            Console.ResetColor();
            Console.Write("/");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("no");
            Console.ResetColor();
            Console.Write("): ");

            string response = Console.ReadLine().Trim().ToLower();
            if (response != "yes")
            {
                Console.WriteLine("Thank you! Goodbye.");
                break;
            }
        }
    }
}
