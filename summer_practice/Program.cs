using System.Collections;
using System.Linq.Expressions;
using summer_practice_domain;

namespace summer_practice
{
    sealed class Programm
    {
        sealed class IntComaprerWithIComparer : IEqualityComparer<int>, IComparer<int>
        {
            private static IntComaprerWithIComparer? _instance;

            private IntComaprerWithIComparer()
            {
        
            }

            public static IntComaprerWithIComparer Instance =>
                _instance ??= new IntComaprerWithIComparer();
            public bool Equals(int x, int y)
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

            public int Compare(int x, int y)
            {
                return (x - y);
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

        #region Lab2 test

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
        
        public static void CombinationsTest(IEnumerable<int> set, int length)
        {
            Console.WriteLine(ToStringIEnumerableIEnumerable(set.GetCombinationsWithoutRepetitions(length, IntComaprerWithIComparer.Instance)));
        }

        public static void SubsetsTest(IEnumerable<int> set)
        {
            Console.WriteLine(ToStringIEnumerableIEnumerable(set.GetSubsetsWithoutRepetitions(IntComaprerWithIComparer.Instance)));
        }

        public static void PermutationsTest(IEnumerable<int> set)
        {
            Console.WriteLine(ToStringIEnumerableIEnumerable(set.GetPermutationsWithoutRepetitions(IntComaprerWithIComparer.Instance)));
        }

        public static void Lab2Test()
        {
            int[] set = { 1, 2, 3 };
            Console.WriteLine("=====Combinations Test=====");
            try
            {
                CombinationsTest(set, 2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("=====  Subsets  Test  =====");
            try
            {
                SubsetsTest(set);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("=====Permutations Test=====");
            try
            {
                PermutationsTest(set);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region lab3 test
        sealed class IntComparerWithComparer : Comparer<int>
        {
            public override int Compare(int x, int y)
            {
                return (x - y);
            }
        }
        
        public static int Comparison(int x, int y)
        {
            return (x - y);
        }
        
        public static void ToStringArray<T>(T[] arr)
        {
            Console.WriteLine(string.Join(", ", arr));
        }

        static void Lab3Test()
        {
            int Min = -100;
            int Max = 200;
            Random randNum = new Random();
            int[] set = Enumerable
                .Repeat(0, 10)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();
            // int[] set = { 9, 7, 6, 5, 3, 11, 10 };
            Console.WriteLine("Original array");
            ToStringArray(set);
            Console.WriteLine("=====Selection sorted=====");
            var selectionSorted = set.Sort(SortingMethodsImpl.SortingMode.Ascending, SortingMethodsImpl.SortingMethod.SelectionSort);
            ToStringArray(selectionSorted);
            Console.WriteLine("=====Insertion sorted=====");
            var insertionSorted = set.Sort(SortingMethodsImpl.SortingMode.Ascending, SortingMethodsImpl.SortingMethod.InsertionSort, IntComaprerWithIComparer.Instance);
            ToStringArray(insertionSorted);
            Console.WriteLine("=====Merge sorted=====");
            var mergeSorted = set.Sort(SortingMethodsImpl.SortingMode.Ascending, SortingMethodsImpl.SortingMethod.MergeSort, new IntComparerWithComparer());
            ToStringArray(mergeSorted);
            Console.WriteLine("=====Heap sorted=====");
            var heapSorted = set.Sort(SortingMethodsImpl.SortingMode.Ascending, SortingMethodsImpl.SortingMethod.HeapSort, Comparison);
            ToStringArray(heapSorted);
            Console.WriteLine("=====Quick sorted=====");
            var quickSorted = set.Sort(SortingMethodsImpl.SortingMode.Ascending, SortingMethodsImpl.SortingMethod.QuickSort);
            ToStringArray(quickSorted);
        }

        #endregion
        
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // StudentTest();
            Lab2Test();
            // Lab3Test();
        }
    }
}