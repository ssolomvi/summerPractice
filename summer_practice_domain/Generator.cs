using System.Collections;

namespace summer_practice_domain;

public static class GeneratorEnumerableExtensions
{
    /*
     * Для каждого из методов требуется проверка элементов
     * входного перечисления на предмет попарного неравенства
     * по отношению эквивалентности, передаваемому в метод в виде
     * реализации обобщённого интерфейса IEqualityComparer<T>
     */
    
    // (n+m-1)!/((n-1)!*m!), m -- count in
    private static bool CheckEqualityInCollection<T>(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
        if (!collection.Any()) { return false; }

        foreach (var firstElement in collection)
        {
            foreach (var secondElement in collection.Skip(1))
            {
                if (comparer.Equals(firstElement, secondElement))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
# region Combinations

    public static IEnumerable<IEnumerable<T>> GetCombinationsWithoutRepetitions<T>(this IEnumerable<T> input,
        int length, IEqualityComparer<T> comparer)
    {
        bool repeatingElements = CheckEqualityInCollection(input, comparer);
        if (repeatingElements)
        {
            throw new ArgumentException("GetCombinationsWithoutRepetitions: Passed collection must not contain repeating elements");
        }

        if (length > input.Count())
        {
            throw new ArgumentException("GetCombinationsWithoutRepetitions: Length of aggregate should not be less than length param");
        }

        return input.CombinationsWithoutRepetition(length);
    }
    private static IEnumerable<IEnumerable<T>> CombinationsWithoutRepetition<T>(this IEnumerable<T> input, int length)
    {
        if (length <= 0)
            yield return new List<T>();
        else
        {
            int current = 1;
            foreach (T i in input)
            {
                foreach (IEnumerable<T> c in input.Skip(current++).CombinationsWithoutRepetition(length - 1))
                {
                    List<T> list = new List<T>();
                    list.Add(i);
                    list.AddRange(c);
                    yield return list;
                }    
            }
        }
    }
    
    public static IEnumerable<IEnumerable<T>> GetCombinationsWithRepetitions<T>(this IEnumerable<T> input,
        int length, IEqualityComparer<T> comparer)
    {
        bool repeatingElements = CheckEqualityInCollection(input, comparer);
        if (repeatingElements)
        {
            throw new ArgumentException("GetCombinationsWithRepetitions: Passed collection must not contain repeating elements");
        }
        
        if (length > input.Count())
        {
            throw new ArgumentException("GetCombinationsWithRepetitions: Length of aggregate should not be less than length param");
        }

        return input.CombinationsWithRepetition(length);
    }
    private static IEnumerable<IEnumerable<T>> CombinationsWithRepetition<T>(this IEnumerable<T> input, int length)
    {
        if (length <= 0)
            yield return new List<T>();
        else
        {
            foreach (T i in input)
            {
                foreach (IEnumerable<T> combination in input.CombinationsWithRepetition(length - 1))
                {
                    List<T> list = new List<T>();
                    list.Add(i);
                    list.AddRange(combination);
                    yield return list;
                }    
            }
        }
    }
#endregion

#region Subsets
public static IEnumerable<IEnumerable<T>> GetSubsetsWithoutRepetitions<T>(this IEnumerable<T> input, IEqualityComparer<T> comparer)
{
    bool repeatingElements = CheckEqualityInCollection(input, comparer);
    if (repeatingElements)
    {
        throw new ArgumentException("GetSubsetsWithoutRepetitions: Passed collection must not contain repeating elements");
    }

    return input.SubsetsWithoutRepetitions();
}

private static IEnumerable<IEnumerable<T>> SubsetsWithoutRepetitions<T>(this IEnumerable<T> input)
{
    int input_count = input.Count();
    for (int length = 0; length <= input_count; length++)
    {
        foreach (IEnumerable<T> c in input.CombinationsWithoutRepetition(length))
        {
            yield return c;
        }
    }
}

#endregion
    
#region Permutations
    public static IEnumerable<IEnumerable<T>> GetPermutationsWithoutRepetitions<T>(this IEnumerable<T> input, IEqualityComparer<T> comparer)
    {
        bool repeatingElements = CheckEqualityInCollection(input, comparer);
        if (repeatingElements)
        {
            throw new ArgumentException("GetPermutationsWithoutRepetitions: Passed collection must not contain repeating elements");
        }

        return input.PermutationWithoutRepetitions();
    }
    private static IEnumerable<IEnumerable<T>> PermutationWithoutRepetitions<T>(this IEnumerable<T> input)
    {            
        if (input == null || !input.Any()) yield break;
        if (input.Count() == 1) yield return input;

        foreach (var item in input)
        {
            var next  = input.Where(l => !(l.Equals(item))).ToList();
            foreach (var perm in PermutationWithoutRepetitions(next))
            {
                yield return (new List<T>{item}).Concat(perm);
            }
        }
    }
#endregion
}
