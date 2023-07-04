using summer_practice_domain;

namespace summer_practice
{
    class Programm
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Student st1 = new Student("Соломатина", "Светлана", "Викторовна", "М8О-213Б-21", "C#");
            Console.WriteLine(st1.ToString());
            Console.WriteLine($"{st1.ChosenCourse} | {st1.Group} | {st1.Name} {st1.Surname} {st1.Patronymic}");
            Console.WriteLine($"course of student above is: {st1.GetCourseNumber()}");
            Console.WriteLine($"Hash code: {st1.GetHashCode()}");
            Student st2 = new Student("Иванов", "Иван", "Иванович", "М8О-210Б-22", "Go");
            Console.WriteLine(st2.ToString());
            Console.WriteLine($"Hash code: {st2.GetHashCode()}");
            Student st3 = new Student("Иванов", "Иван", "Иванович", "М8О-211Б-22", "Go");
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
    }
}