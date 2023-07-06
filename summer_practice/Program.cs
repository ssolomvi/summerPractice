using System.Collections;
using summer_practice_domain;

namespace summer_practice
{
    sealed class Programm
    {
        sealed class IntComaprer : IEqualityComparer<int>
        {
            private static IntComaprer? _instance;

            private IntComaprer()
            {
        
            }

            public static IntComaprer Instance =>
                _instance ??= new IntComaprer();
            public bool Equals(int? x, int? y)
            {
                // int is not a reference, though how can i implement IEqualityComparer<int> (if i can)?
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return (x == y);
            }

            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }
        public static void StudentTest()
        {
            Student st1 = new Student("Соломатина", "Светлана", "Викторовна", "М8О-213Б-21", "CSharp");
            Console.WriteLine(st1.ToString());
            Console.WriteLine($"{st1.ChosenCourse} | {st1.Group} | {st1.Name} {st1.Surname} {st1.Patronymic}");
            Console.WriteLine($"course of student above is: {st1.CourseNumber}");
            Console.WriteLine($"Hash code: {st1.GetHashCode()}");
            Student st2 = new Student("Иванов", "Иван", "Иванович", "М8О-210Б-22", "Go");
            Console.WriteLine(st2.ToString());
            Console.WriteLine($"Hash code: {st2.GetHashCode()}");
            Student st3 = new Student("Иванов", "Иван", "Иванович", "М8О-211Б-22", "5");
            Console.WriteLine(st3.ToString());
            Console.WriteLine($"Hash code: {st3.GetHashCode()}");
                
            Console.WriteLine(st1.Equals(st2));
            Console.WriteLine(st2.Equals((object)st3));
            
            Dictionary<Student, string> hashTable = new Dictionary<Student, string>();
            hashTable.Add(st1, "a"); hashTable.Add(st2, "b"); hashTable.Add(st3, "c");
            hashTable.Remove(st1);
            Console.WriteLine(hashTable.Count);
            
            /*
            string surname = Console.ReadLine(), name = Console.ReadLine(), patronymic = Console.ReadLine();
            string group = Console.ReadLine(), chosenCourse = Console.ReadLine();
            try
            {
                Student custom = new Student(surname, name, patronymic, group, chosenCourse);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }*/
        }

        public static string ToStringIEnumerableIEnumerable<T>(IEnumerable<IEnumerable<T>> collections)
        {
            string result = "[";
            foreach (IEnumerable<T> collection in collections)
            {
                string colString = " [ ";
                colString += string.Join(", ", collection);
                colString += " ]";
                result += colString;
            }

            result += " ]";
            return result;
        }
        
        public static void CombinationsTest()
        {
            int k = 1;
            int[] set = { 1, 2, 3 };
            Console.WriteLine(ToStringIEnumerableIEnumerable(set.CombinationsWithoutRepetition(3)));
        }

        static void SubsetsTest()
        {
            int[] set = new int[] { 1, 2, 4 };
            IEqualityComparer<int> comparer;
            Console.WriteLine(ToStringIEnumerableIEnumerable(set.GetSubsetsWithoutRepetitions(comparer)));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int[] set = { 1, 2, 3 };
            CombinationsTest();
        }
    }
}