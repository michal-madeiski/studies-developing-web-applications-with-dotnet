using System.Reflection;

namespace ExamplesLinq
{
    public class Department(int id, string name)
    {
        public int Id { get; set; } = id;
        public String Name { get; set; } = name;

        public override string ToString()
        {
            return $"{Id,2}) {Name,16}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics(int id, int index, string name, Gender gender, bool active,
        int departmentId, List<string> topics)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;
        public List<string> Topics { get; set; } = topics;

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public class Topic(int id, string name)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;

        public override string ToString()
        {
            return $"{Id,2}) {Name,15}";
        }
    }

    public class Student(int id, int index, string name, Gender gender, bool active,
        int departmentId, List<int> topics)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;
        public List<int> Topics { get; set; } = topics;

        public Student() : this(0, 0, "DefaultName", Gender.Male, false, 0, new List<int>()) { }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }

        public string ToStringParams(int age, string city)
        {
            return $"{Name}, {age}, {city}";
        }
    }

    public class StudentToTopic(int id, int studentId, int topicId)
    {
        public int Id { get; set; } = id;
        public int StudentId { get; set; } = studentId;
        public int TopicId { get; set; } = topicId;

        public override string ToString()
        {
            return $"{Id,2}) Student: {StudentId,2}, Topic: {TopicId,2}";
        }
    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return [5, 3, 9, 7, 1, 2, 6, 7, 8];
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new ();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateNamesEasy()
        {
            return [
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger",
                "Showner",
                "Neumann",
                "Rocky",
                "Bruno"
            ];
        }

        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return [
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    ["C#","PHP","algorithms"]),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    ["Basic","Java"]),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    ["Java","C#"]),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    ["algorithms","web programming"]),
            new StudentWithTopics(11,22345,"Nowaczyk", Gender.Female,true,2,
                    ["C#","PHP","web programming"]),
            new StudentWithTopics(12, 23235, "Newton", Gender.Male, false,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(13, 23444, "Showner", Gender.Male, true,2,
                    ["Basic","C#"]),
            new StudentWithTopics(14, 24000, "Neumann", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(15, 24001, "Rocky", Gender.Male, true,2,
                    ["fuzzy logic","C#"]),
            new StudentWithTopics(16, 24100, "Bruno", Gender.Female, false,2,
                    ["algorithms","web programming"]),
            ];
        }

        public static List<Department> GenerateDepartmentsEasy()
        {
            return [
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            ];
        }

        public static List<Topic> GenerateTopicsEasy()
        {
            return [
                new Topic(1, "algorithms"),
                new Topic(2, "Basic"),
                new Topic(3, "C#"),
                new Topic(4, "C++"),
                new Topic(5, "fuzzy logic"),
                new Topic(6, "Java"),
                new Topic(7, "JavaScript"),
                new Topic(8, "neural networks"),
                new Topic(9, "PHP"),
                new Topic(10, "web programming")
            ];
        }

        public static List<Topic> GenerateTopicsQuery()
        {
            var topicsList = GenerateStudentsWithTopicsEasy()
                             .SelectMany(s => s.Topics)
                             .Distinct()
                             .OrderBy(s => s)
                             .ToList();
            
            return topicsList.Select((name, idx) => new Topic(idx + 1, name)).ToList();
        }

        public static List<Student> GenerateStudents()
        {
            return GenerateStudentsWithTopicsEasy()
                   .Select(s => new Student(
                                    s.Id,
                                    s.Index,
                                    s.Name,
                                    s.Gender,
                                    s.Active,
                                    s.DepartmentId,
                                    GenerateTopicsQuery()
                                    .Where(t => s.Topics.Contains(t.Name))
                                    .Select(t => t.Id)
                                    .ToList() 
                    )).ToList();
        }

        public static List<StudentToTopic> GenerateStudentsToTopics()
        {
            int id = 1;
            return GenerateStudents()
                   .SelectMany(s => s.Topics.Select(t => new StudentToTopic(id++, s.Id, t))).ToList();          
        }
    }

    class Program
    {
        public static void ShowAllCollections()
        {
            Console.WriteLine(nameof(ShowAllCollections));
            Generator.GenerateIntsEasy().ToList().ForEach(Console.WriteLine);
            Generator.GenerateDepartmentsEasy().ForEach(Console.WriteLine);
            Generator.GenerateStudentsWithTopicsEasy().ForEach(Console.WriteLine);
        }

        static void SortedStudentsNGroups(int n)
        {
            Console.WriteLine(nameof(SortedStudentsNGroups));
            var studs = Generator.GenerateStudentsWithTopicsEasy()
                        .OrderBy(s => s.Name)
                        .ThenByDescending(s => s.Index)
                        .Select((stud, pos) => new {stud, pos})
                        .GroupBy(x => x.pos / n)
                        .Select(g => g.Select(x => x.stud));
            
            var studsList = studs.ToList();
            for (int i = 0; i < studsList.Count; i++)
            {
                Console.WriteLine($"Group {i + 1}:");
                studsList[i].ToList().ForEach(s => Console.WriteLine("  " + s));
            }
        }

        static void SortedTopics()
        {
            Console.WriteLine(nameof(SortedTopics));
            var topics = from s in Generator.GenerateStudentsWithTopicsEasy()
                         from topic in s.Topics
                         group topic by topic into tg
                                              let count = tg.Count()
                                              orderby count descending
                                              select new
                                              {
                                                  Topic = tg.Key,
                                                  Count = count
                                              };

            var topicsList = topics.ToList();
            foreach (var elem in topicsList)
            {
                Console.WriteLine($"{elem.Topic} -> {elem.Count}");
            }
        }

        static void SortedTopicsByGender()
        {
            Console.WriteLine(nameof(SortedTopicsByGender));
            var topicsByGender = from s in Generator.GenerateStudentsWithTopicsEasy()
                                 group s by s.Gender into g
                                 select new
                                 {
                                     Gender = g.Key,
                                     Topics = from stud in g
                                              from topic in stud.Topics
                                              group topic by topic into tg
                                              let count = tg.Count()
                                              orderby count descending
                                              select new
                                              {
                                                  Topic = tg.Key,
                                                  Count = count
                                              }
                                 };
            
            var topicsByGenderList = topicsByGender.ToList();
            foreach (var group in topicsByGenderList)
            {
                Console.WriteLine($"Gender: {group.Gender}");
                group.Topics.ToList().ForEach(t => Console.WriteLine($"  {t.Topic} -> {t.Count}"));
            }       
        }

        static void ShowCollection<T>(Func<List<T>> collectionGenerator, string name)
        {   
            Console.WriteLine(name);
            collectionGenerator().ForEach(item => Console.WriteLine(item));
        }

        static void ReflectionTest()
        {
            string className = "ExamplesLinq.Student";
            object stud1 = Assembly.GetExecutingAssembly().CreateInstance(className);
            if (stud1 == null) { Console.WriteLine("stud1 == null"); return; }
            MethodInfo methodInfo = stud1.GetType().GetMethod("ToStringParams", new Type[] { typeof(int), typeof(string) });
            if (methodInfo == null) { Console.WriteLine("methodInfo == null"); return; }
            object result = methodInfo.Invoke(stud1, new object[] {21, "Wroclaw"});
            Console.WriteLine($"Result = {result}");     
        }

        static void PrinSeparator(int n, bool flag, int max=80)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i == max / 2)
                {
                    if (flag) { Console.Write($"TASK{n}"); }
                    else { Console.Write("====="); }
                } else { Console.Write("="); }
            } Console.WriteLine();
        }

        static void Main()
        {
            ShowAllCollections();PrinSeparator(1, false);
            PrinSeparator(1, true);SortedStudentsNGroups(5);PrinSeparator(1, false);
            PrinSeparator(2, true);SortedTopics();Console.WriteLine();
            SortedTopicsByGender();PrinSeparator(2, false);
            PrinSeparator(3, true);ShowCollection(() => Generator.GenerateTopicsEasy(), nameof(Generator.GenerateTopicsEasy));Console.WriteLine();
            ShowCollection(() => Generator.GenerateTopicsQuery(), nameof(Generator.GenerateTopicsQuery));Console.WriteLine();
            ShowCollection(() => Generator.GenerateStudents(), nameof(Generator.GenerateStudents));Console.WriteLine();
            ShowCollection(() => Generator.GenerateStudentsToTopics(), nameof(Generator.GenerateStudentsToTopics));PrinSeparator(3, false);
            PrinSeparator(4, true); ReflectionTest(); PrinSeparator(4, false);
        }
    }
}