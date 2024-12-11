using System;
using System.Collections.Generic;

public class CesarCipher
{
    private static int shift = 3;

    public static string Encrypt(string input)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            letter = (char)(letter + shift);
            if (letter > 'z')
            {
                letter = (char)(letter - 26);
            }
            else if (letter > 'Z' && buffer[i] <= 'Z')
            {
                letter = (char)(letter - 26);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }

    public static string Decrypt(string input)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            letter = (char)(letter - shift);
            if (letter < 'a')
            {
                letter = (char)(letter + 26);
            }
            else if (letter < 'A' && buffer[i] <= 'Z')
            {
                letter = (char)(letter + 26);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }
}

public class User
{
    private string login;
    private string password;
    private int privilegeLevel;

    public string Login
    {
        get => login;
        set => login = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }

    public int PrivilegeLevel
    {
        get => privilegeLevel;
        set
        {
            if (value >= 0 && value <= 15)
            {
                privilegeLevel = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Уровень привилегий должен быть от 1 до 15.");
            }
        }
    }

    public bool IsValidate()
    {
        return PrivilegeLevel >= 0 && PrivilegeLevel <= 15;
    }
}

public class Program
{
    private static List<User> users = new List<User>();

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1. Добавить нового пользователя");
            Console.WriteLine("2. Вывести всех пользователей");
            Console.WriteLine("3. Авторизация");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser();
                    break;
                case "2":
                    DisplayUsers();
                    break;
                case "3":
                    AuthorizeUser();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private static void AddUser()
    {
        User user = new User();
        Console.Write("Введите логин: ");
        user.Login = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();
        user.Password = CesarCipher.Encrypt(password);

        Console.Write("Введите уровень привилегий (1-15): ");
        int privilegeLevel = int.Parse(Console.ReadLine());
        user.PrivilegeLevel = privilegeLevel;

        users.Add(user);
        Console.WriteLine("Пользователь добавлен успешно.");
    }

    private static void DisplayUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Нет пользователей для отображения.");
            return;
        }

        Console.WriteLine("Список пользователей:");
        foreach (var user in users)
        {
            Console.WriteLine($"Логин: {user.Login}, Уровень привилегий: {user.PrivilegeLevel}");
        }
    }

    private static void AuthorizeUser()
    {
        Console.Write("Введите логин: ");
        string login = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();
        string encryptedPassword = CesarCipher.Encrypt(password);

        User foundUser = users.Find(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase) && u.Password == encryptedPassword);

        if (foundUser != null)
        {
            Console.WriteLine("Авторизация успешна Уровень привилегий: " + foundUser.PrivilegeLevel);
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль.");
        }
    }
}