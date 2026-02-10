namespace OrderManagementSystem;

public class UserOrder
{
    public string? Name { get; private set; }
    public List<Order> Orders { get; private set; }

    public UserOrder(string? name, List<Order> orders)
    {
        Name = name;
        Orders = orders;
    }
}