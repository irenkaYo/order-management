namespace OrderManagementSystem;

public class Order
{
    public int UserId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }

    public Order(int userId, string productName, decimal price)
    {
        UserId = userId;
        ProductName = productName;
        Price = price;
    }    
}