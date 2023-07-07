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
    // todo Assert(array != null, "Check the arguments in the caller!");
    // todo Assert(index >= 0 && length >= 0 && (array.Length - index >= length), "Check the arguments in the caller!");

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

public static class SortingMethodsImpl
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
    
    /// <summary>
    /// Sorting mode is ascending and x is less than y
    /// </summary>
    /// <param name="x">the first object to compare.</param>
    /// <param name="y">the second object to compare.</param>
    /// <param name="sortingMode">either ascending or descending</param>
    /// <param name="comparer">function by which comparison is done</param>
    /// <typeparam name="T">type of x, y</typeparam>
    /// <returns>Bool, which indicates that sorting mode is ascending and x is less than y</returns>
    private static bool CompareInner<T>(T x, T y, SortingMode sortingMode, IComparer<T> comparer)
    {
        return ((sortingMode == SortingMode.Ascending) & (comparer.Compare(x, y) < 0));
    }
    
    private static void Swap<T>(T[] a, int i, int j)
    {
        (a[i], a[j]) = (a[j], a[i]);
    }
    
    #region InsertionSort

    private static T[] InsertionSorting<T>(T[] keys, SortingMode sortingMode, IComparer<T> comparer)
    {
        T[] toReturn = new T[keys.Length];
        toReturn[0] = keys[0];
        
        for (int i = 0; i < keys.Length - 1; i++)
        {
            T t = keys[i + 1];

            int j = i;
            while (j >= 0 && CompareInner(t, toReturn[j], sortingMode, comparer))
            {
                toReturn[j + 1] = toReturn[j];
                j--;
            }
                
            toReturn[j + 1] = t;
        }

        return toReturn;
    }

    #endregion
    
    #region SelectionSort
    
    private static T[] SelectionSorting<T>(T[] keys, SortingMode sortingMode, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();

        for (int i = 0; i < toReturn.Length - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < toReturn.Length; j++)
            {
                if (CompareInner(toReturn[j], toReturn[min], sortingMode, comparer))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                Swap(toReturn, i, min);
            }
        }
        return toReturn;
    }

    #endregion

    #region HeapSort
    private static T[] HeapSorting<T>(T[] keys, SortingMode sortingMode, IComparer<T> comparer)
    {
        // todo: Assert(comparer != null);
        // todo: Assert(!keys.IsEmpty);
        T[] toReturn = (T[])keys.Clone();
        
        int n = toReturn.Length;
        for (int i = n >> 1; i >= 1; i--)
        {
            DownHeap(toReturn, i, n, sortingMode, comparer);
        }

        for (int i = n; i > 1; i--)
        {
            Swap(toReturn, 0, i - 1);
            DownHeap(toReturn, 1, i - 1, sortingMode, comparer);
        }

        return toReturn;
    }

    private static void DownHeap<T>(T[] keys, int i, int n, SortingMode sortingMode, IComparer<T> comparer)
    {
        T d = keys[i - 1];
        while (i <= n >> 1)
        {
            int child = 2 * i;
            if (child < n && CompareInner(keys[child - 1], keys[child], sortingMode, comparer))
            {
                child++;
            }

            if (!CompareInner(d, keys[child - 1], sortingMode, comparer))
                break;

            keys[i - 1] = keys[child - 1];
            i = child;
        }

        keys[i - 1] = d;
    }

    #endregion

    #region QuickSort

    private static T[] QuickSorting<T>
        (T[] keys, SortingMode sortingMode, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();
        
        QuickSortingInner(toReturn, 0, toReturn.Length - 1, sortingMode, comparer);
        
        return toReturn;
    }

    private static void QuickSortingInner<T>
        (T[] keys, int leftBound, int rightBound, SortingMode sortingMode, IComparer<T> comparer)
    {
        if (leftBound < rightBound)
        {
            int pivot = Partition(keys, leftBound, rightBound, sortingMode, comparer);
            QuickSortingInner(keys, leftBound, pivot - 1, sortingMode, comparer);
            QuickSortingInner(keys, pivot + 1, rightBound, sortingMode, comparer);
        }
    }

    private static int Partition<T>
        (T[] keys, int leftBound, int rightBound, SortingMode sortingMode, IComparer<T> comparer)
    {
        T pivot = keys[rightBound];
        int i = leftBound - 1;
        for (int j = leftBound; j < rightBound; j++)
        {
            if (CompareInner(keys[j], pivot, sortingMode, comparer))
            {
                ++i;
                Swap(keys, i, j);
            }
        }
        
        Swap(keys, i + 1, rightBound);
        return (i + 1);
    }
    
    #endregion

    #region MergeSort
    
    private static T[] MergeSorting<T>(T[] keys, SortingMode sortingMode, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();
        if (keys.Length == 1) return toReturn;
        int middle = keys.Length / 2;
        
        return Merge(MergeSorting(toReturn.Take(middle).ToArray(), sortingMode, comparer), 
            MergeSorting(toReturn.Skip(middle).ToArray(), sortingMode, comparer), sortingMode, comparer);
    }

    private static T[] Merge<T>(T[] first, T[] second, SortingMode sortingMode, IComparer<T> comparer)
    {
        int ptr1 = 0, ptr2 = 0;
        T[] merged = new T[first.Length + second.Length];

        for (int i = 0; i < merged.Length; ++i)
        {
            if (ptr1 < first.Length && ptr2 < second.Length)
            {
                merged[i] = CompareInner(second[ptr2], first[ptr1], sortingMode, comparer) ? second[ptr2++] : first[ptr1++];
            }
            else
            {
                merged[i] = ptr2 < second.Length ? second[ptr2++] : first[ptr1++];
            }
        }
		
        return merged;
    }
    
    #endregion

    public static T[] Sort<T>(this T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod)
        where T : IComparable<T>
    {
        var comparer = Comparer<T>.Default;
        return toSort.Sort(sortingMode, sortingMethod, comparer);
    }

    public static T[] Sort<T>(this T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod, IComparer<T> comparer)
    {
        switch (sortingMethod)
        {
            case SortingMethod.InsertionSort:
                return InsertionSorting(toSort, sortingMode, comparer);
            case SortingMethod.SelectionSort:
                return SelectionSorting(toSort, sortingMode, comparer);
            case SortingMethod.MergeSort:
                return MergeSorting(toSort, sortingMode, comparer);
            case SortingMethod.HeapSort:
                return HeapSorting(toSort, sortingMode, comparer);
            default:
                return QuickSorting(toSort, sortingMode, comparer);
        }
    }

    public static T[] Sort<T>(this T[] toSort, SortingMode sortingMode, SortingMethod sortingMethod, Comparison<T> comparison)
    {
        var comparer = Comparer<T>.Create(comparison);
        return toSort.Sort(sortingMode, sortingMethod, comparer);
    }
}
