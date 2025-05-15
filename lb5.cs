// Program with 4 tasks and English characters instead of Ukrainian
using System;
using System.Collections.Generic;
using System.Linq;

#region Task 1 - Organization Hierarchy
class Organization
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Organization() { Console.WriteLine("Organization constructor (default)"); }
    public Organization(string name) { Name = name; Console.WriteLine("Organization constructor (1 param)"); }
    public Organization(string name, string address) { Name = name; Address = address; Console.WriteLine("Organization constructor (2 params)"); }
    ~Organization() { Console.WriteLine("Organization destructor"); }

    public virtual void Show() => Console.WriteLine($"Organization: {Name}, Address: {Address}");
}

class InsuranceCompany : Organization
{
    public string InsuranceType { get; set; }
    public InsuranceCompany() : base() { Console.WriteLine("InsuranceCompany constructor (default)"); }
    public InsuranceCompany(string name, string type) : base(name) { InsuranceType = type; Console.WriteLine("InsuranceCompany constructor (2 params)"); }
    public InsuranceCompany(string name, string address, string type) : base(name, address) { InsuranceType = type; Console.WriteLine("InsuranceCompany constructor (3 params)"); }
    ~InsuranceCompany() { Console.WriteLine("InsuranceCompany destructor"); }

    public override void Show() { base.Show(); Console.WriteLine($"Insurance Type: {InsuranceType}"); }
}

class OilGasCompany : Organization
{
    public int Wells { get; set; }
    public override void Show() { base.Show(); Console.WriteLine($"Wells: {Wells}"); }
}

class Factory : Organization
{
    public int Employees { get; set; }
    public override void Show() { base.Show(); Console.WriteLine($"Employees: {Employees}"); }
}
#endregion

#region Task 2 - Abstract class Persona
abstract class Persona
{
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public abstract void ShowInfo();
    public int GetAge() => DateTime.Now.Year - BirthDate.Year;
}

class Applicant : Persona
{
    public string Faculty { get; set; }
    public override void ShowInfo() => Console.WriteLine($"Applicant: {Surname}, Faculty: {Faculty}, Age: {GetAge()}");
}

class Student : Persona
{
    public string Faculty { get; set; }
    public int Course { get; set; }
    public override void ShowInfo() => Console.WriteLine($"Student: {Surname}, Faculty: {Faculty}, Course: {Course}, Age: {GetAge()}");
}

class Teacher : Persona
{
    public string Faculty { get; set; }
    public string Position { get; set; }
    public int Experience { get; set; }
    public override void ShowInfo() => Console.WriteLine($"Teacher: {Surname}, Faculty: {Faculty}, Position: {Position}, Experience: {Experience} years, Age: {GetAge()}");
}
#endregion

#region Task 3 - Structs, Tuples, Records
struct HumanStruct
{
    public string Surname, Name, Patronymic;
    public int Year, Height, Weight;
}

record HumanRecord(string Surname, string Name, string Patronymic, int Year, int Height, int Weight);

class Task3
{
    public static void Run()
    {
        Console.WriteLine("--- Using Struct ---");
        var structs = new List<HumanStruct>
        {
            new HumanStruct { Surname = "Black", Name = "Tom", Patronymic = "A.", Year = 1990, Height = 180, Weight = 80 },
            new HumanStruct { Surname = "White", Name = "Anna", Patronymic = "B.", Year = 1988, Height = 160, Weight = 60 }
        };
        structs.RemoveAll(h => h.Height == 180 && h.Weight == 80);
        var idx = structs.FindIndex(h => h.Surname == "White");
        if (idx >= 0) structs.Insert(idx + 1, new HumanStruct { Surname = "Green", Name = "Nina", Patronymic = "C.", Year = 2000, Height = 170, Weight = 70 });
        foreach (var h in structs) Console.WriteLine($"{h.Surname} {h.Name} {h.Patronymic}, {h.Year}, {h.Height}cm, {h.Weight}kg");

        Console.WriteLine("--- Using Tuple ---");
        var tuples = new List<(string Surname, string Name, string Patronymic, int Year, int Height, int Weight)>
        {
            ("Smith", "John", "D.", 1985, 180, 75),
            ("Doe", "Jane", "M.", 1990, 160, 60)
        };
        tuples.RemoveAll(h => h.Height == 180 && h.Weight == 75);
        var tidx = tuples.FindIndex(h => h.Surname == "Doe");
        if (tidx >= 0) tuples.Insert(tidx + 1, ("Brown", "Mike", "R.", 2000, 170, 70));
        foreach (var h in tuples) Console.WriteLine($"{h.Surname} {h.Name} {h.Patronymic}, {h.Year}, {h.Height}cm, {h.Weight}kg");

        Console.WriteLine("--- Using Record ---");
        var records = new List<HumanRecord>
        {
            new HumanRecord("Stone", "Alex", "Q.", 1992, 180, 80),
            new HumanRecord("Lee", "Grace", "K.", 1991, 160, 60)
        };
        records.RemoveAll(h => h.Height == 180 && h.Weight == 80);
        var ridx = records.FindIndex(h => h.Surname == "Lee");
        if (ridx >= 0) records.Insert(ridx + 1, new HumanRecord("Fox", "Liam", "Z.", 1995, 170, 70));
        foreach (var h in records) Console.WriteLine($"{h.Surname} {h.Name} {h.Patronymic}, {h.Year}, {h.Height}cm, {h.Weight}kg");
    }
}
#endregion

class Program
{
    static void Main()
    {
        Console.WriteLine("Select task to run (1-4):");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var org = new InsuranceCompany("Guardian", "NY", "Life");
                org.Show();
                break;
            case "2":
                Console.Write("Enter number of personas: ");
                int n = int.Parse(Console.ReadLine());
                Persona[] people = new Persona[n];
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Choose type (1-Applicant, 2-Student, 3-Teacher):");
                    int t = int.Parse(Console.ReadLine());
                    Console.Write("Enter surname: ");
                    string s = Console.ReadLine();
                    Console.Write("Enter birth year: ");
                    int y = int.Parse(Console.ReadLine());
                    Console.Write("Enter birth month: ");
                    int m = int.Parse(Console.ReadLine());
                    Console.Write("Enter birth day: ");
                    int d = int.Parse(Console.ReadLine());
                    DateTime bday = new DateTime(y, m, d);
                    Console.Write("Enter faculty: ");
                    string f = Console.ReadLine();

                    switch (t)
                    {
                        case 1:
                            people[i] = new Applicant { Surname = s, BirthDate = bday, Faculty = f };
                            break;
                        case 2:
                            Console.Write("Enter course: ");
                            int c = int.Parse(Console.ReadLine());
                            people[i] = new Student { Surname = s, BirthDate = bday, Faculty = f, Course = c };
                            break;
                        case 3:
                            Console.Write("Enter position: ");
                            string pos = Console.ReadLine();
                            Console.Write("Enter experience: ");
                            int exp = int.Parse(Console.ReadLine());
                            people[i] = new Teacher { Surname = s, BirthDate = bday, Faculty = f, Position = pos, Experience = exp };
                            break;
                    }
                }
                Console.WriteLine("All personas:");
                foreach (var p in people) p.ShowInfo();
                Console.WriteLine("Enter age range (from-to):");
                int from = int.Parse(Console.ReadLine());
                int to = int.Parse(Console.ReadLine());
                foreach (var p in people.Where(p => p.GetAge() >= from && p.GetAge() <= to)) p.ShowInfo();
                break;
            case "3":
                Console.Write("Enter number of personas: ");
                int n3 = int.Parse(Console.ReadLine());
                Persona[] people3 = new Persona[n3];
                for (int i = 0; i < n3; i++)
                {
                    Console.WriteLine("Choose type (1-Applicant, 2-Student, 3-Teacher):");
                    int t = int.Parse(Console.ReadLine());
                    Console.Write("Enter surname: ");
                    string s = Console.ReadLine();
                    Console.Write("Enter birth year: ");
                    int y = int.Parse(Console.ReadLine());
                    Console.Write("Enter birth month: ");
                    int m = int.Parse(Console.ReadLine());
                    Console.Write("Enter birth day: ");
                    int d = int.Parse(Console.ReadLine());
                    DateTime bday = new DateTime(y, m, d);
                    Console.Write("Enter faculty: ");
                    string f = Console.ReadLine();

                    switch (t)
                    {
                        case 1:
                            people3[i] = new Applicant { Surname = s, BirthDate = bday, Faculty = f };
                            break;
                        case 2:
                            Console.Write("Enter course: ");
                            int c = int.Parse(Console.ReadLine());
                            people3[i] = new Student { Surname = s, BirthDate = bday, Faculty = f, Course = c };
                            break;
                        case 3:
                            Console.Write("Enter position: ");
                            string pos = Console.ReadLine();
                            Console.Write("Enter experience: ");
                            int exp = int.Parse(Console.ReadLine());
                            people3[i] = new Teacher { Surname = s, BirthDate = bday, Faculty = f, Position = pos, Experience = exp };
                            break;
                    }
                }
                Console.WriteLine("All personas:");
                foreach (var p in people3) p.ShowInfo();
                Console.WriteLine("Enter age range (from-to):");
                int from3 = int.Parse(Console.ReadLine());
                int to3 = int.Parse(Console.ReadLine());
                foreach (var p in people3.Where(p => p.GetAge() >= from3 && p.GetAge() <= to3)) p.ShowInfo();
                break;
            case "4":
                Console.WriteLine("Running Task 4: Structs, Tuples, Records");
                Task3.Run();
                break;
            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}
