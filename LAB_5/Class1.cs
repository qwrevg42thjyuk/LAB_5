using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5
{
    internal class Class1
    {
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

// Клас Товар
class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public Product(string name, double price, string description, string category)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
    }
}

// Клас Користувач
class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> PurchaseHistory { get; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        PurchaseHistory = new List<Order>();
    }
}

// Клас Замовлення
class Order
{
    public List<Product> Products { get; }
    public List<int> Quantities { get; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }

    public Order()
    {
        Products = new List<Product>();
        Quantities = new List<int>();
        TotalPrice = 0;
        Status = "In Progress";
    }
}

// Інтерфейс для пошуку товарів
interface ISearchable
{
    List<Product> SearchByPriceRange(List<Product> products, double minPrice, double maxPrice);
    List<Product> SearchByCategory(List<Product> products, string category);
    List<Product> SearchByRating(List<Product> products, int minRating);
}

// Клас Магазин
class Store : ISearchable
{
    public List<User> Users { get; }
    public List<Product> Products { get; }
    public List<Order> Orders { get; }

    public Store()
    {
        Users = new List<User>();
        Products = new List<Product>();
        Orders = new List<Order>();
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void CreateOrder(User user, List<Product> products, List<int> quantities)
    {
        var order = new Order();
        for (int i = 0; i < products.Count; i++)
        {
            order.Products.Add(products[i]);
            order.Quantities.Add(quantities[i]);
            order.TotalPrice += products[i].Price * quantities[i];
        }
        user.PurchaseHistory.Add(order);
        Orders.Add(order);
    }

    public void CompleteOrder(Order order)
    {
        order.Status = "Completed";
    }

    // Реалізація методів інтерфейсу ISearchable
    public List<Product> SearchByPriceRange(List<Product> products, double minPrice, double maxPrice)
    {
        return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
    }

    public List<Product> SearchByCategory(List<Product> products, string category)
    {
        return products.Where(p => p.Category == category).ToList();
    }

    public List<Product> SearchByRating(List<Product> products, int minRating)
    {
        // Додайте логіку для пошуку за рейтингом товарів (якщо це необхідно)
        // В даному прикладі рейтинг не використовується в класі Product
        return new List<Product>();
    }
}

class Programm
{
    static void Main()
    {
        Store store = new Store();

        User user1 = new User("user1", "password1");
        User user2 = new User("user2", "password2");

        Product product1 = new Product("Product 1", 10.0, "Description 1", "Category A");
        Product product2 = new Product("Product 2", 15.0, "Description 2", "Category B");

        store.AddUser(user1);
        store.AddUser(user2);

        store.AddProduct(product1);
        store.AddProduct(product2);

        List<Product> productsToBuy = new List<Product> { product1, product2 };
        List<int> quantities = new List<int> { 2, 1 };

        store.CreateOrder(user1, productsToBuy, quantities);
        store.CompleteOrder(store.Orders[0]);
    }
}
