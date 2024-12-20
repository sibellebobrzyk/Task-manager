using System;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Task Manager Menu");
            Console.WriteLine("1. Create Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Generate Report by Deadline");
            Console.WriteLine("4. Generate Report by Priority");
            Console.WriteLine("5. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    GenerateReportByDeadline();
                    break;
                case "4":
                    GenerateReportByPriority();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    Console.ReadKey();
                    ShowMenu();
                    break;
            }
        }

        public static void CreateTask()
        {
            Console.Clear();
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Category:");
            string category = Console.ReadLine();

            Console.WriteLine("Enter Priority (Low, Medium, High):");
            string priority = Console.ReadLine();

            // Solicitar data do prazo (deadline)
            Console.WriteLine("Enter Deadline (yyyy-MM-dd):");
            DateTime deadline;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out deadline))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
            }

            string query = "INSERT INTO Tasks (Title, Category, Priority, Deadline) VALUES (@Title, @Category, @Priority, @Deadline)";
            DatabaseHelper.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Priority", priority);
                cmd.Parameters.AddWithValue("@Deadline", deadline);
            });

            Console.WriteLine("Task created successfully!");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            ShowMenu();
        }

        public static void GenerateReportByDeadline()
        {
            Console.Clear();
            Console.WriteLine("Generate Task Report by Deadline");

            string query = "SELECT Id, Title, Category, Priority, Deadline FROM Tasks ORDER BY Deadline ASC";
            var tasks = DatabaseHelper.ExecuteReader(query);

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ID  | Title                          | Category   | Priority | Deadline");
            Console.WriteLine("--------------------------------------------------");

            while (tasks.Read())
            {
                Console.WriteLine($"{tasks["Id"],-4} | {tasks["Title"],-30} | {tasks["Category"],-10} | {tasks["Priority"],-8} | {tasks["Deadline"],-10}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            ShowMenu();
        }

        public static void GenerateReportByPriority()
        {
            Console.Clear();
            Console.WriteLine("Generate Task Report by Priority");

            // Solicita ao usuário para escolher a prioridade
            Console.WriteLine("Enter Priority (Low, Medium, High):");
            string priority = Console.ReadLine().Trim();

            // Valida a entrada do usuário
            while (priority != "Low" && priority != "Medium" && priority != "High")
            {
                Console.WriteLine("Invalid priority. Please enter one of the following: Low, Medium, High.");
                priority = Console.ReadLine().Trim();
            }

            // Consulta SQL com filtro por prioridade
            string query = "SELECT Id, Title, Category, Deadline, Priority FROM Tasks WHERE Priority = @Priority ORDER BY Deadline ASC";
            var tasks = DatabaseHelper.ExecuteReader(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Priority", priority);
            });

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ID  | Title                          | Category   | Deadline  | Priority");
            Console.WriteLine("--------------------------------------------------");

            while (tasks.Read())
            {
                Console.WriteLine($"{tasks["Id"],-4} | {tasks["Title"],-30} | {tasks["Category"],-10} | {tasks["Deadline"],-10} | {tasks["Priority"],-8}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            ShowMenu();
        }

        public static void ViewTasks()
        {
            Console.Clear();
            Console.WriteLine("View Tasks");

            string query = "SELECT Id, Title, Category, Priority, Deadline FROM Tasks";
            var tasks = DatabaseHelper.ExecuteReader(query);

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ID  | Title                          | Category   | Priority | Deadline");
            Console.WriteLine("--------------------------------------------------");

            while (tasks.Read())
            {
                Console.WriteLine($"{tasks["Id"],-4} | {tasks["Title"],-30} | {tasks["Category"],-10} | {tasks["Priority"],-8} | {tasks["Deadline"],-10}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            ShowMenu();
        }
    }
}
