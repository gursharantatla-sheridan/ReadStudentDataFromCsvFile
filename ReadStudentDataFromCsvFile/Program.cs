using System.IO;
using ConsoleTables;

namespace ReadStudentDataFromCsvFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // set the csv file property "Copy to Output Directory" to "Copy if newer"
            // OR
            // place the csv file in the same folder as C# executable (bin/Debug/net)
            string filePath = "students.csv";

            // load students from the CSV file
            List<Student> students = LoadStudentsFromFile(filePath);

            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
                return;
            }

            // --- MENU LOGIC ---
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("\n1 - Display All Students");
                Console.WriteLine("2 - Sort by Marks (High to Low)");
                Console.WriteLine("3 - Show Top 3 Students");
                Console.WriteLine("4 - Find Highest Scorer");
                Console.WriteLine("5 - Exit\n");

                // keeps looping until the user enters a valid integer choice
                do
                {
                    Console.Write("Enter your choice: ");
                } while (!int.TryParse(Console.ReadLine(), out choice));

                switch (choice)
                {
                    case 1:
                        ShowAllStudents(students);
                        break;
                    case 2:
                        SortStudentsByMarks(students);
                        break;
                    case 3:
                        ShowTopThreeStudents(students);
                        break;
                    case 4:
                        FindHighestScorer(students);
                        break;
                    case 5:
                        Console.WriteLine("\nExiting program...");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
            while (choice != 5);
        }

        static List<Student> LoadStudentsFromFile(string filePath)
        {
            List<Student> students = new List<Student>();

            if (File.Exists(filePath))
            {
                // reading the CSV file using StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // skip the header line
                    reader.ReadLine();

                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // split the line by commas to get individual fields
                        string[] parts = line.Split(',');

                        // create a new Student object and populate it with data from the CSV
                        Student std = new Student();
                        std.Id = int.Parse(parts[0]);
                        std.Name = parts[1];
                        std.Marks = int.Parse(parts[2]);

                        // add the student to the list
                        students.Add(std);
                    }
                } // reader is automatically closed here
            }
            else
            {
                Console.WriteLine("Error: students.csv not found");
            }

            return students;
        }

        static void ShowAllStudents(List<Student> students)
        {
            Console.WriteLine("\nAll Students:\n");

            // create a console table to display all the students
            ConsoleTable table = new ConsoleTable("Id", "Name", "Marks");
            foreach (var s in students)
            {
                table.AddRow(s.Id, s.Name, s.Marks);
            }

            table.Write(Format.MarkDown);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void SortStudentsByMarks(List<Student> students)
        {
            var sortedStudents = students.OrderByDescending(s => s.Marks).ToList();

            Console.WriteLine("\nStudents Sorted by Marks:\n");

            // create a console table to display the students sorted by marks
            ConsoleTable table = new ConsoleTable("Id", "Name", "Marks");
            foreach (var s in sortedStudents)
            {
                table.AddRow(s.Id, s.Name, s.Marks);
            }

            table.Write(Format.MarkDown);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ShowTopThreeStudents(List<Student> students)
        {
            var topThreeStudents = students.OrderByDescending(s => s.Marks).Take(3).ToList();

            Console.WriteLine("\nTop 3 Students:\n");

            // create a console table to display the top 3 students
            ConsoleTable table = new ConsoleTable("Id", "Name", "Marks");
            foreach (var s in topThreeStudents)
            {
                table.AddRow(s.Id, s.Name, s.Marks);
            }

            table.Write(Format.MarkDown);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void FindHighestScorer(List<Student> students)
        {
            var highestScorer = students.OrderByDescending(s => s.Marks).FirstOrDefault();

            Console.WriteLine("\nHighest Scoring Student:\n");

            // create a console table to display the highest scoring student
            ConsoleTable table = new ConsoleTable("Id", "Name", "Marks");
            table.AddRow(highestScorer?.Id, highestScorer?.Name, highestScorer?.Marks);

            table.Write(Format.MarkDown);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
