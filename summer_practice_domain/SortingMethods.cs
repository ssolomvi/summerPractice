namespace summer_practice_domain;

/*
 * Реализуйте набор перегруженных методов расширения для сортировки обобщённых массивов данных. В качестве первого
 * параметра методы получают на вход коллекцию сортируемых данных типа T[]. В качестве второго - порядок сортировки
 * (по возрастанию, по убыванию) - в виде значения собственного перечислимого типа (enum). Третий параметр - алгоритм
 * сортировки (вставками, выбором, пирамидальная, быстрая, слиянием).
 * Методы перегружены засчёт четвёртого параметра:
 * ● его нет (должен быть обеспечено ограничение внутренней сравнимости на предмет отношения порядка элементов массива);
 * ● значение типа IComparer<T>, задающее внешнее правило отношения порядка на пространстве элементов типа T;
 * ● значение типа Comparer<T>, задающее внешнее правило отношения порядка на пространстве элементов типа T;
 * ● делегат Comparison<T>, на объект которого подписан метод, задающий внешнее отношение порядка на пространстве
 *   элементов типа T.
 * Возвращаемое значение метода типа T[] - отсортированный массив.
 * 
 */
/*
internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
{
    Debug.Assert(array != null, "Check the arguments in the caller!");
    Debug.Assert(index >= 0 && length >= 0 && (array.Length - index >= length), "Check the arguments in the caller!");

    int lo = index;
    int hi = index + length - 1;
    while (lo <= hi)
    {
        int i = lo + ((hi - lo) >> 1);
        int order = comparer.Compare(array[i], value);

        if (order == 0) return i;
        if (order < 0)
        {
            lo = i + 1;
        }
        else
        {
            hi = i - 1;
        }
    }

    return ~lo;
}
*/

public interface ISortingMethods
{
    public enum SortingMode
    {
        Ascending = 0,
        Descending = 1
    }
    
    public enum SortingMethod
    {
        InsertionSort,
        SelectionSort,
        HeapSort,
        QuickSort,
        MergeSort
    }

    public T[] Sort<T>(T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod) where T : System.IComparable<T>;
    
    public T[] Sort<T>(T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod, IComparer<T> comparer);
    
    public T[] Sort<T>(T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod, Comparison<T> comparison);
}

public class InsertionSort : ISortingMethods
{
    private static void InsertionSorting<T>(T[] keys, IComparer<T> comparer)
    {
        for (int i = 0; i < keys.Length - 1; i++)
        {
            T t = keys[i + 1];

            int j = i;
            while (j >= 0 && comparer.Compare(t, keys[j]) < 0)
            {
                keys[j + 1] = keys[j];
                j--;
            }
                
            if (j != i) keys[j + 1] = t;
        }
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod)
        where T : System.IComparable<T>
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}

public class SelectionSort : ISortingMethods
{
    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod)
        where T : System.IComparable<T>
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}

public class HeapSort : ISortingMethods
{
    /*
    private static void HeapSort<T>(Span<T> keys, Comparison<T> comparer)
    {
        Debug.Assert(comparer != null);
        Debug.Assert(!keys.IsEmpty);

        int n = keys.Length;
        for (int i = n >> 1; i >= 1; i--)
        {
            DownHeap(keys, i, n, comparer);
        }

        for (int i = n; i > 1; i--)
        {
            Swap(keys, 0, i - 1);
            DownHeap(keys, 1, i - 1, comparer);
        }
    }
*/
    private static void DownHeap<T>(Span<T> keys, int i, int n, Comparison<T> comparer)
    {
        // Debug.Assert(comparer != null);

        T d = keys[i - 1];
        while (i <= n >> 1)
        {
            int child = 2 * i;
            if (child < n && comparer(keys[child - 1], keys[child]) < 0)
            {
                child++;
            }

            if (!(comparer(d, keys[child - 1]) < 0))
                break;

            keys[i - 1] = keys[child - 1];
            i = child;
        }

        keys[i - 1] = d;
    }

    
    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod)
        where T : System.IComparable<T>
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}

public class QuickSort : ISortingMethods
{
    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod)
        where T : System.IComparable<T>
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}

public class MergeSort : ISortingMethods
{
    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod)
        where T : System.IComparable<T>
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        throw new NotImplementedException();
    }

    public T[] Sort<T>(T[] toSort, ISortingMethods.SortingMode sortingMode, ISortingMethods.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}