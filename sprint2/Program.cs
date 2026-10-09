using System;

namespace CompanyStructure
{
    // 1. Базовый класс Employee
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public Employee(string name, string position, double salary)
        {
            Name = name;
            Position = position;
            Salary = salary;
        }

        // Метод для вывода информации о сотруднике
        public virtual void PrintInfo()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Должность: {Position}");
            Console.WriteLine($"Зарплата: {Salary}");
        }
    }

    // 2. Простое наследование: Manager наследует Employee
    public class Manager : Employee
    {
        public string Department { get; set; }

        public Manager(string name, string position, double salary, string department)
            : base(name, position, salary)
        {
            Department = department;
        }

        // Переопределяем PrintInfo, добавляя информацию об отделе
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Отдел: {Department}");
        }
    }

    // 3. Сложное наследование: ProjectManager наследует Manager
    public class ProjectManager : Manager, IReportable, ITeamLeader
    {
        public string ProjectName { get; set; }
        private string[] teamMembers;

        public ProjectManager(string name, string position, double salary,
                              string department, string projectName,
                              string[] teamMembers)
            : base(name, position, salary, department)
        {
            ProjectName = projectName;
            this.teamMembers = teamMembers;
        }

        // Переопределяем PrintInfo, добавляя название проекта
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Проект: {ProjectName}");
        }

        // Реализация метода интерфейса IReportable
        public string GenerateReport()
        {
            return $"Отчёт по проекту \"{ProjectName}\":\n" +
                   $"Руководитель: {Name} ({Position})\n" +
                   $"Отдел: {Department}\n" +
                   $"Количество участников команды: {teamMembers.Length}";
        }

        // Реализация метода интерфейса ITeamLeader
        public string[] GetTeamMembers()
        {
            return teamMembers;
        }
    }

    // 4. Интерфейсы
    public interface IReportable
    {
        string GenerateReport();
    }

    public interface ITeamLeader
    {
        string[] GetTeamMembers();
    }

    // 5. Тестирование
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Экземпляр Employee
            Employee employee = new Employee("Иван Иванов", "Разработчик", 80000);
            Console.WriteLine("=== Employee ===");
            employee.PrintInfo();

            // Экземпляр Manager
            Manager manager = new Manager("Пётр Петров", "Менеджер", 120000, "IT-отдел");
            Console.WriteLine("\n=== Manager ===");
            manager.PrintInfo();

            // Экземпляр ProjectManager
            string[] team = { "Анна", "Сергей", "Ольга" };
            ProjectManager projectManager = new ProjectManager(
                "Светлана Смирнова", "Проектный менеджер", 150000,
                "Отдел разработки", "Внедрение CRM", team);

            Console.WriteLine("\n=== ProjectManager ===");
            projectManager.PrintInfo();

            // Тестирование интерфейсов
            Console.WriteLine("\n=== Отчёт (IReportable) ===");
            Console.WriteLine(projectManager.GenerateReport());

            Console.WriteLine("\n=== Члены команды (ITeamLeader) ===");
            foreach (var member in projectManager.GetTeamMembers())
            {
                Console.WriteLine($"- {member}");
            }

            Console.ReadLine();
        }
    }
}