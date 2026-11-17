using Npgsql;
namespace OrderManagementSystem;

public class OrdersRepository
{
    string connectionString =
        "Host=localhost;" +
        "Port=5432;" +
        "Database=order_management;" +
        "Username=####;" +
        "Password=####;";
    
    List<Order> Orders;
    Order order;
    public OrdersRepository()
    {
        Orders = new List<Order>();
    }

    public void AddOrder(int userId, string product, decimal price)
    {
        using var connection = OpenConnection();

        var sql = "INSERT INTO orders (user_id, product, price) VALUES (@userId, @product, @price)";

        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("userId", userId);
        command.Parameters.AddWithValue("product", product);
        command.Parameters.AddWithValue("price", price);
        
        using var reader = command.ExecuteReader();
    }
    
    public UserOrder ShowAllOrdersOfUser(int userId)
    {
        using var connection = OpenConnection();
        string name = null;
        
        var sql = "SELECT name, orders.product, orders.price FROM users JOIN orders ON users.id = orders.user_id WHERE users.id = @id";
        
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", userId);

        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            name = reader.GetString(0);
            var product = reader.GetString(1);
            var price = reader.GetDecimal(2);
            Order order = new Order(userId, product, price);
            Orders.Add(order);
        }
        UserOrder userOrder = new UserOrder(name, Orders);
        return userOrder;
    }
    
    public NpgsqlConnection OpenConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
