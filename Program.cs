using System;

namespace BIS_2
{
    public class User
    {
        string name;
        int id;
        public string Name
        {
            get { return name; }
        }
        public int Id
        {
            get { return id; }
        }
        public User(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
        
        public void PrintUser()
        {
            Console.Write("name - {0,10}, id - {1,2}", name, id);
        }
    }

    public class Obj
    {
        string objName;
        int id;
        public string Name
        {
            get { return objName; }
        }
        public int Id
        {
            get { return id; }
        }
        public Obj(string name, int id)
        {
            this.objName = name;
            this.id = id;
        }

        public void PrintObj()
        {
            Console.WriteLine("name - {0,10}, id - {1,2}", objName, id);
        }
    }
   
    class Program
    {
        static int ConvertFromDecToBin(int dec)//Перевод из десятичной системы счисления в двоичную
        {
            int bin = 0;
            int i = 0;
            while (dec != 0)
            {
                bin = bin + (dec % 2) * (int)(Math.Pow(10,i));
                dec = dec / 2;
                i++;
            }
            return bin;
        }
        static User[] CreateUsers(int users)//Создание массива пользователей
        {
            string[] name = new string[5] { "Vika", "Dima", "Kate", "Alex", "Nick" };
            User[] userArr = new User[users];
            userArr[0] = new User(name[0], 0);
            for (int i = 1; i < users; i++)
            {
                userArr[i] = new User(name[i], i);
            }
            return userArr;
        }
        static Obj[] CreateObjs(int objs)//Создание массива объектов
        {
            string[] objName = new string[4] { "Объект 1", "Объект 2", "Объект 3", "Объект 4" };
            Obj[] objArr = new Obj[objs];
            for (int j = 0; j < objs; j++)
            {
                objArr[j] = new Obj(objName[j], j);
            }
            return objArr;
        }
        static int[,] CreateMatrix(int users, int objs)
        {
            int[,] matrix = new int[users, objs];
            for (int j = 0; j < objs; j++)
                matrix[0, j] = 111;//Полные права у администратора
            Random rnd = new Random();
            for (int i = 1; i < users; i++)
                for (int j = 0; j < objs; j++)
                {
                    matrix[i, j] = ConvertFromDecToBin(rnd.Next(0, 8));//Случайное присвоение прав с 000 до 111 (изначально в десятичной системе счисления)
                    if (matrix[i, j] == 1)
                        matrix[i, j] = 0;//Запрет на случайное присвоение права 001
                }
            return matrix;
        }
        static void PrintMatrix(User[] userArr, Obj[] objArr, int[,] matrix)//Вывод матрицы в консоль
        {
            Console.WriteLine("_________|{0,5}|{1,5}|{2,5}|{3,5}|{4,5}", userArr[0].Name, userArr[1].Name, userArr[2].Name, userArr[3].Name, userArr[4].Name);
            Console.WriteLine("{0,9}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}", objArr[0].Name, matrix[userArr[0].Id, objArr[0].Id], matrix[userArr[1].Id, objArr[0].Id], matrix[userArr[2].Id, objArr[0].Id], matrix[userArr[3].Id, objArr[0].Id], matrix[userArr[4].Id, objArr[0].Id]);
            Console.WriteLine("{0,9}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}", objArr[1].Name, matrix[userArr[0].Id, objArr[1].Id], matrix[userArr[1].Id, objArr[1].Id], matrix[userArr[2].Id, objArr[1].Id], matrix[userArr[3].Id, objArr[1].Id], matrix[userArr[4].Id, objArr[1].Id]);
            Console.WriteLine("{0,9}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}", objArr[2].Name, matrix[userArr[0].Id, objArr[2].Id], matrix[userArr[1].Id, objArr[2].Id], matrix[userArr[2].Id, objArr[2].Id], matrix[userArr[3].Id, objArr[2].Id], matrix[userArr[4].Id, objArr[2].Id]);
            Console.WriteLine("{0,9}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}", objArr[3].Name, matrix[userArr[0].Id, objArr[3].Id], matrix[userArr[1].Id, objArr[3].Id], matrix[userArr[2].Id, objArr[3].Id], matrix[userArr[3].Id, objArr[3].Id], matrix[userArr[4].Id, objArr[3].Id]);

            Console.WriteLine();
        }
        static void GUI(User[] userArr, Obj[] objArr, int[,] matrix)//Пользовательский интерфейс
        {
            int userId = 0;
            string inputUser = "";
            bool userExist = false;
            while (true)
            { 
            Console.Write("Введите идентификатор пользователя > ");
            inputUser = Console.ReadLine();
            Console.WriteLine();
                foreach (User user in userArr)
                {
                    if (inputUser == user.Name && !userExist)
                    {
                        userExist = true;
                        userId = user.Id;
                        Console.WriteLine("User: {0}", user.Name);
                        Console.WriteLine("Идентификация прошла успешно, добро пожаловать в систему");
                        Console.WriteLine("Перечень Ваших прав:");
                        foreach (Obj obj in objArr)
                        {
                            Console.WriteLine("{0,7}: read {1}, write {2}, grant {3}",
                                obj.Name,
                                matrix[user.Id, obj.Id] / 100,
                                matrix[user.Id, obj.Id] / 10 % 10,
                                matrix[user.Id, obj.Id] % 100 % 10);
                        }
                    }                
                }
                if (!userExist)
                    Console.WriteLine("Ошибка идентификации");
                else
                {      
                    bool comandExit = false;
                    while (!comandExit)
                    {
                        Console.Write("Жду ваших указаний > ");
                        string inputComand = Console.ReadLine();
                        Console.WriteLine();
                        switch (inputComand)
                        {
                            case "show":
                            PrintMatrix(userArr, objArr, matrix);
                                break;
                            case "read":
                                Console.Write("Над каким объектом производится операция? > ");
                                if (matrix[userId, Convert.ToInt32(Console.ReadLine()) - 1] / 100 == 1)
                                    Console.WriteLine("Операция прошла успешно");
                                else
                                    Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                    break;
                            case "write":
                                Console.Write("Над каким объектом производится операция? > ");
                                if (matrix[userId, Convert.ToInt32(Console.ReadLine()) - 1] / 10 % 10 == 1)
                                    Console.WriteLine("Операция прошла успешно");
                                else
                                    Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                break;
                            case "grant":
                                Console.Write("Право на какой объект передается? > ");
                                int grantedObj = Convert.ToInt32(Console.ReadLine()) - 1;
                                if (!(matrix[userId, grantedObj] % 100 % 10 == 1))
                                    Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                else
                                {
                                    Console.Write("Какое право передается? > ");
                                    switch(Console.ReadLine())
                                    {
                                        case "read":
                                            if (matrix[userId, grantedObj] / 100 == 1)
                                            {
                                                Console.Write("Какому пользователю передается право? > ");
                                                string grantedUser = Console.ReadLine();
                                                bool uExist = false;
                                                foreach (User user in userArr)
                                                {
                                                    if (grantedUser == user.Name)
                                                        if (matrix[user.Id, grantedObj] / 100 != 1)
                                                            matrix[user.Id, grantedObj] += 100;
                                                    uExist = true;
                                                }
                                                if (uExist)
                                                {
                                                    Console.WriteLine("Операция прошла успешно");
                                                    Console.WriteLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Отказ в выполнении операции. Пользователя не существует");
                                                    Console.WriteLine();
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                                Console.WriteLine();
                                            }
                                                break;
                                        case "write":
                                            if (matrix[userId, grantedObj] / 10 % 10 == 1)
                                            {
                                                Console.Write("Какому пользователю передается право? > ");
                                                string grantedUser = Console.ReadLine();
                                                bool uExist = false;
                                                foreach (User user in userArr)
                                                {
                                                    if (grantedUser == user.Name)
                                                        if (matrix[user.Id, grantedObj] / 10 % 10 != 1)
                                                            matrix[user.Id, grantedObj] += 10;
                                                    uExist = true;
                                                }
                                                if (uExist)
                                                {
                                                    Console.WriteLine("Операция прошла успешно");
                                                    Console.WriteLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Отказ в выполнении операции. Пользователя не существует");
                                                    Console.WriteLine();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                                Console.WriteLine();
                                            }
                                                break;
                                        case "grant":
                                            if (matrix[userId, grantedObj] % 10 % 10 == 1)
                                            {
                                                Console.Write("Какому пользователю передается право? > ");
                                                string grantedUser = Console.ReadLine();
                                                bool uExist = false;
                                                foreach (User user in userArr)
                                                {
                                                    if (grantedUser == user.Name)
                                                        if (matrix[user.Id, grantedObj] % 10 % 10 != 1)
                                                            matrix[user.Id, grantedObj] += 1;
                                                    uExist = true;
                                                }
                                                if (uExist)
                                                {
                                                    Console.WriteLine("Операция прошла успешно");
                                                    Console.WriteLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Отказ в выполнении операции. Пользователя не существует");
                                                    Console.WriteLine();
                                                }
                                                }
                                            else
                                                Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                            break;
                                        default:
                                            Console.WriteLine("Отказ в выполнении операции. У Вас нет прав для ее осуществления");
                                            break;
                                    }
                                }
                                break;
                            case "quit":
                                Console.WriteLine("Работа пользователя {0} завершена. До свидания.", inputUser);
                                userExist = false;
                                comandExit = true;
                                break;
                            default:
                                Console.WriteLine("Неизвестная команда");
                                break;
                        }
                    }
                }
            }
        }      
        static void Main(string[] args)
        {
            int users = 5;
            int objs = 4;
            var userArr = CreateUsers(users);
            var objArr = CreateObjs(objs);
            var matrix = CreateMatrix(users, objs);
            PrintMatrix(userArr, objArr, matrix);
            GUI(userArr, objArr, matrix);
        }
    }
}
