using System.Collections;

namespace summer_practice_domain.LinkedList;

public sealed class LinkedList<T> : IEquatable<LinkedList<T>>,
    ICloneable,
    IEnumerable<T>
{
    public override bool Equals(object? obj)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    internal LinkedListNode<T>? head;
    internal LinkedListNode<T>? last;

    #region Help Methods

    private LinkedListNode<T> FindPrevByIndex(int index)
    {
        ThrowIfEmptyList();
        ThrowIfIndexNegative(index);
        if (index == 0)
        {
            return head;
        }
        LinkedListNode<T> found = head;
        int i = 0;
        while (i != (index - 1))
        {
            if (found == null)
            {
                throw new ArgumentException("Index out of range {0}", nameof(index));
            }

            i++;
            found = found.next;
        }

        return found;
    }

    private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
    {
        newNode.next = null;
        head = newNode;
        last = newNode;
    }

    private void InternalInsertNodeAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
        newNode.next = node.next;
        node.next = newNode;
        if (newNode.next == null)
        {
            last = newNode;
        }
    }

    private void ThrowIfIndexNegative(int index)
    {
        if (index < 0)
        {
            throw new ArgumentException("Index must be > 0 {0}", nameof(index));
        }
    }

    private void ThrowIfEmptyList()
    {
        if (head == null)
        {
            throw new NullReferenceException("List is empty");
        }
    }

    public LinkedListNode<T>? Find(T value, IEqualityComparer<T> comparer)
    {
        LinkedListNode<T>? node = head;
        if (node != null)
        {
            if (value != null)
            {
                while (node.next != null)
                {
                    if (comparer.Equals(node.item, value))
                    {
                        return node;
                    }

                    node = node.next;
                }
            }
            else
            {
                while (node.next != null)
                {
                    if (node.item == null)
                    {
                        return node;
                    }

                    node = node.next;
                }
            }
        }

        return null;
    }
    
    private T[] ToArray()
    {
        int currSize = 0, allocatedFor = 2;
        T[] arr = new T[2];
        foreach (LinkedListNode<T> item in this)
        {
            if (currSize == allocatedFor)
            {
                allocatedFor *= 2;
                Array.Resize(ref arr, allocatedFor);
            }

            arr[currSize++] = item.Value;
        }

        if (currSize != allocatedFor)
        {
            Array.Resize(ref arr, currSize);
        }

        return arr;
    }

    #endregion
    
    #region Constructors

    public LinkedList()
    {
    }

    public LinkedList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (var item in collection)
        {
            AddLast(item);
        }
    }

    public LinkedList(LinkedList<T> list)
    {
        ArgumentNullException.ThrowIfNull(list);

        foreach (LinkedListNode<T> item in list)
        {
            AddLast(item.Value);
        }
    }


    #endregion
    
    #region Indexator

    public T this[int index]
    {
        get
        {
            LinkedListNode<T> found = FindPrevByIndex(index);
            if (found.next == null)
            {
                throw new ArgumentException("Index out of range {0}", nameof(index));
            }

            return found.next.Value;
        }

        set
        {
            LinkedListNode<T> found = FindPrevByIndex(index);
            if (found.next == null)
            {
                AddLast(value);
            }
            else
            {
                found.next.Value = value;
            }
        }
    }

    #endregion

    #region Add methods

    public LinkedListNode<T> AddFirst(T value)
    {
        LinkedListNode<T> result = new LinkedListNode<T>(this, value);
        if (head == null)
        {
            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            result.next = head;
            head = result;
        }

        return result;
    }

    public LinkedListNode<T> AddLast(T value)
    {
        LinkedListNode<T> result = new LinkedListNode<T>(this, value);
        if (head == null)
        {
            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            InternalInsertNodeAfter(last, result);
        }

        return result;
    }

    public LinkedListNode<T> AddByIndex(T value, int index)
    {
        ThrowIfIndexNegative(index);
        LinkedListNode<T> result = new LinkedListNode<T>(this, value);
        if (head == null)
        {
            if (index > 0)
            {
                throw new ArgumentException("Index must be 0 because list is empty {0}", nameof(index));
            }

            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            LinkedListNode<T> currNode = head;
            int i = 0;
            while (i < (index - 1))
            {
                if (currNode == null)
                {
                    throw new ArgumentException("Index out of range {0}", nameof(index));
                }

                i++;
                currNode = currNode.next;
            }

            InternalInsertNodeAfter(currNode, result);
        }

        return result;
    }

    #endregion

    #region Remove methods

    public T RemoveFirst()
    {
        ThrowIfEmptyList();
        var removedValue = head.Value;
        head = head.next;
        return removedValue;
    }

    public T RemoveLast(T value)
    {
        ThrowIfEmptyList();
        var removedValue = last.Value;

        if (head == last)
        {
            head = null;
            last = null;
            return removedValue;
        }

        LinkedListNode<T> currNode = head;
        while (currNode.next != last)
        {
            currNode = currNode.next;
        }

        currNode.next = null;
        last = currNode;
        return removedValue;
    }

    public T RemoveByIndex(T value, int index)
    {
        ThrowIfEmptyList();
        ThrowIfIndexNegative(index);
        T removedValue;
        if (index == 0)
        {
            removedValue = head.Value;
            if (head.next == null)
            {
                head = null;
                last = null;
            }
            else
            {
                head = head.next;
            }

            return removedValue;
        }

        int i = 0;
        LinkedListNode<T> currNode = head;
        while (i < (index - 1))
        {
            if (currNode == null)
            {
                throw new ArgumentException("Index out of range {0}", nameof(index));
            }

            i++;
            currNode = currNode.next;
        }

        removedValue = currNode.next.Value;
        currNode.next = currNode.next.next;
        if (currNode.next == null)
        {
            last = currNode;
        }

        return removedValue;
    }    

    #endregion

    #region Reverse

    public LinkedList<T> Reverse()
    {
        LinkedList<T> reversed = new LinkedList<T>();
        if (head == null)
        {
            return reversed;
        }

        foreach (LinkedListNode<T> item in this)
        {
            reversed.AddLast(item);
        }

        return reversed;
    }

    public static LinkedList<T> operator !(LinkedList<T> list)
    {
        return list.Reverse();
    }

    #endregion

    #region Concatenation

    public LinkedList<T> Concatenate(LinkedList<T> other)
    {
        LinkedList<T> concatenationResult = new LinkedList<T>();
        if (head != null)
        {
            foreach (LinkedListNode<T> item in this)
            {
                concatenationResult.AddLast(item);
            }
        }

        if (other.head != null)
        {
            foreach (LinkedListNode<T> item in other)
            {
                concatenationResult.AddLast(item);
            }
        }

        return concatenationResult;
    }
    
    public static LinkedList<T> operator +(LinkedList<T> list, LinkedList<T> another)
    {
        return list.Concatenate(another);
    }    

    #endregion

    #region Intersection

    public LinkedList<T> Intersection(LinkedList<T> other, IEqualityComparer<T> comparer)
    {
        LinkedList<T> intersectionResult = new LinkedList<T>();

        if (head == null || other.head == null)
        {
            return intersectionResult;
        }

        foreach (LinkedListNode<T> item in this)
        {
            if (other.Find(item.Value, comparer) != null)
            {
                intersectionResult.AddLast(item.Value);
            }
        }

        return intersectionResult;
    }

    public static LinkedList<T> operator &(LinkedList<T> list, LinkedList<T> another)
    {
        return list.Intersection(another, EqualityComparer<T>.Default);
    }    

    #endregion

    #region Union

    public LinkedList<T> Unite(LinkedList<T> other, IEqualityComparer<T> comparer)
    {
        LinkedList<T> unionResult = new LinkedList<T>();

        if (head == null)
        {
            foreach (LinkedListNode<T> item in other)
            {
                unionResult.AddLast(item.Value);
            }

            return unionResult;
        }

        foreach (LinkedListNode<T> item in this)
        {
            unionResult.AddLast(item.Value);
        }

        if (other == null)
        {
            return unionResult;
        }

        foreach (LinkedListNode<T> item in other)
        {
            if (Find(item.Value, comparer) == null)
            {
                unionResult.AddLast(item.Value);
            }
        }

        return unionResult;
    }

    public static LinkedList<T> operator |(LinkedList<T> list, LinkedList<T> another)
    {
        return list.Unite(another, EqualityComparer<T>.Default);
    }

    #endregion

    #region Complement

    public LinkedList<T> Complement(LinkedList<T> other, IEqualityComparer<T> comparer)
    {
        ThrowIfEmptyList();
        LinkedList<T> complementionResult = new LinkedList<T>();
        if (other == null)
        {
            foreach (LinkedListNode<T> item in this)
            {
                complementionResult.AddLast(item.Value);
            }
        }

        foreach (LinkedListNode<T> item in this)
        {
            if (other.Find(item.Value) == null)
            {
                complementionResult.AddLast(item.Value);
            }
        }

        return complementionResult;
    }

    public static LinkedList<T> operator -(LinkedList<T> list, LinkedList<T> other)
    {
        return list.Complement(other, EqualityComparer<T>.Default);
    }

    #endregion

    #region Sorting

    public LinkedList<T> Sort(SortingMethodsImpl.SortingMode sortingMode,
        SortingMethodsImpl.SortingMethod sortingMethod)
    {
        T[] arr = ToArray();
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, Comparer<T>.Default);
        return new LinkedList<T>(sorted);
    }

    public LinkedList<T> Sort(SortingMethodsImpl.SortingMode sortingMode,
        SortingMethodsImpl.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        T[] arr = ToArray();
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, comparer);
        return new LinkedList<T>(sorted);
    }
    
    public LinkedList<T> Sort(SortingMethodsImpl.SortingMode sortingMode,
        SortingMethodsImpl.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        T[] arr = ToArray();
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, comparison);
        return new LinkedList<T>(sorted);
    }


    #endregion

    #region Action

    public void DoAction(Action<T> action)
    {
        ThrowIfEmptyList();
        foreach (LinkedListNode<T> item in this)
        {
            action(item.Value);
        }
    }

    #endregion

    #region Equal

    public bool Equals(LinkedList<T>? other)
    {
        if (other == null)
        {
            return false;
        }

        if (head == null && other.head == null)
        {
            return true;
        }
        else if (head == null || other.head == null)
        {
            return false;
        }

        LinkedListNode<T> currThis = head, currOther = other.head;
        while (currThis != null)
        {
            if (currOther == null)
            {

                return false;
            }

            if (!(currOther.Value.Equals(currThis.Value)))
            {
                return false;
            }

            currThis = currThis.next;
            currOther = currOther.next;
        }

        if (currOther != null)
        {
            return false;
        }

        return true;
    }

    public static bool operator ==(LinkedList<T> list, LinkedList<T> other)
    {
        return list.Equals(other);
    }

    public static bool operator !=(LinkedList<T> list, LinkedList<T> other)
    {
        return (!(list.Equals(other)));
    }

    #endregion

    #region Operator*

    // todo: method for operator*
    public static LinkedList<T> operator *(LinkedList<T> list, LinkedList<T> other)
    {
        throw new NotImplementedException();
    }

    #endregion
    
    
    public object Clone()
    {
        return new LinkedList<T>(this);
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
