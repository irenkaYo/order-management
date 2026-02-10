using OrderManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        int choice = 0;
        while (choice != 7)
        {
            Console.WriteLine("\n1. Show all users.\n" +
                              "2. Find user by ID.\n" +
                              "3. Add user.\n" +
                              "4. Add order.\n" +
                              "5. Show all user's orders.\n" +
                              "6. Delete user by ID.\n" +
                              "7. Exit.");
            choice = int.Parse(Console.ReadLine());
            DoChoice(choice);
        }
    }

    private static void DoChoice(int choice)
    {
        UsersRepository usersRepository = new UsersRepository();
        OrdersRepository ordersRepository = new OrdersRepository();
        switch (choice)
        {
            case 1:
            {
                ShowAllUsers(usersRepository);
                break;
            }
            case 2:
            {
                FindUserByID(usersRepository);
                break;
            }
            case 3:
            {
                AddUser(usersRepository);
                break;
            }
            case 4:
            {
                AddOrder(ordersRepository);
                break;
            }
            case 5:
            {
                ShowUserOrders(ordersRepository);
                break;
            }
            case 6:
            {
                DeleteUser(usersRepository);
                break;
            }
            case 7:
            {
                Console.WriteLine("Exiting...");
                break;
            }
            default:
            {
                Console.WriteLine("Invalid choice");
                break;
            }
        }
    }

    private static void ShowAllUsers(UsersRepository usersRepository)
    {
        List<User> Users = usersRepository.GetAllUsers();
        foreach (var user in Users)
        {
            PrintUser(user);
        }
    }

    private static void FindUserByID(UsersRepository usersRepository)
    {
        int userId = InputUserId();
        User user = usersRepository.GetUserById(userId);
        PrintUser(user);
    }

    private static void AddUser(UsersRepository usersRepository)
    {
        Console.WriteLine("Enter user name: ");
        string userName = Console.ReadLine();
        Console.WriteLine("Enter email: ");
        string email = Console.ReadLine();
        usersRepository.AddUser(userName, email);
    }

    private static void AddOrder(OrdersRepository ordersRepository)
    {
        Console.WriteLine("Enter product: ");
        string productName = Console.ReadLine();
        Console.WriteLine("Enter price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        int userId = InputUserId();
        ordersRepository.AddOrder(userId, productName, price);
    }

    private static void ShowUserOrders(OrdersRepository ordersRepository)
    {
        int userId = InputUserId();
        UserOrder userOrder = ordersRepository.ShowAllOrdersOfUser(userId);
        Console.WriteLine($"User: {userOrder.Name}\nOrders:");
        for (int i = 0; i < userOrder.Orders.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {userOrder.Orders[i].ProductName}");
        }
    }

    private static void DeleteUser(UsersRepository usersRepository)
    {
        int userId = InputUserId();
        usersRepository.DeleteUser(userId);
    }

    private static int InputUserId()
    {
        Console.WriteLine("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());
        return userId;
    }

    private static void PrintUser(User user)
    {
        Console.WriteLine($"ID: {user.UserId}\nName: {user.UserName}\nEmail: {user.Email}");
    }
}