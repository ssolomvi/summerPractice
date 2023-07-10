using System.Collections;

namespace summer_practice_domain.LinkedList;

public sealed class LinkedList<T> : IEquatable<LinkedList<T>>,
    ICloneable,
    IEnumerable<T>
{
    internal LinkedListNode<T>? _head;
    internal LinkedListNode<T>? _last;

    #region Help Methods

    private LinkedListNode<T> FindPrevByIndex(int index)
    {
        ThrowIfEmptyList();
        ThrowIfIndexNegative(index);
        if (index == 0)
        {
            return _head;
        }
        LinkedListNode<T> found = _head;
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
        _head = newNode;
        _last = newNode;
    }

    private void InternalInsertNodeAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
        newNode.next = node.next;
        node.next = newNode;
        if (newNode.next == null)
        {
            _last = newNode;
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
        if (_head == null)
        {
            throw new NullReferenceException("List is empty");
        }
    }

    public LinkedListNode<T>? Find(T value, IEqualityComparer<T> comparer)
    {
        LinkedListNode<T>? node = _head;
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
        foreach (var item in this)
        {
            if (currSize == allocatedFor)
            {
                allocatedFor *= 2;
                Array.Resize(ref arr, allocatedFor);
            }

            arr[currSize++] = item;
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

        foreach (var item in list)
        {
            AddLast(item);
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

    public LinkedList<T> AddFirst(T value)
    {
        LinkedListNode<T> result = new LinkedListNode<T>(value);
        if (_head == null)
        {
            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            result.next = _head;
            _head = result;
        }

        return this;
    }

    public LinkedList<T> AddLast(T value)
    {
        LinkedListNode<T> result = new LinkedListNode<T>(value);
        if (_head == null)
        {
            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            InternalInsertNodeAfter(_last, result);
        }

        return this;
    }

    public LinkedList<T> AddByIndex(T value, int index)
    {
        ThrowIfIndexNegative(index);
        LinkedListNode<T> result = new LinkedListNode<T>(value);
        if (_head == null)
        {
            if (index > 0)
            {
                throw new ArgumentException("Index must be 0 because list is empty {0}", nameof(index));
            }

            InternalInsertNodeToEmptyList(result);
        }
        else
        {
            LinkedListNode<T> currNode = _head;
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

        return this;
    }

    #endregion

    #region Remove methods

    public T RemoveFirst()
    {
        ThrowIfEmptyList();
        var removedValue = _head.Value;
        _head = _head.next;
        return removedValue;
    }

    public T RemoveLast()
    {
        ThrowIfEmptyList();
        var removedValue = _last.Value;

        if (_head == _last)
        {
            _head = null;
            _last = null;
            return removedValue;
        }

        LinkedListNode<T> currNode = _head;
        while (currNode.next != _last)
        {
            currNode = currNode.next;
        }

        currNode.next = null;
        _last = currNode;
        return removedValue;
    }

    public T RemoveByIndex(int index)
    {
        ThrowIfEmptyList();
        ThrowIfIndexNegative(index);
        T removedValue;
        if (index == 0)
        {
            removedValue = _head.Value;
            if (_head.next == null)
            {
                _head = null;
                _last = null;
            }
            else
            {
                _head = _head.next;
            }

            return removedValue;
        }

        int i = 0;
        LinkedListNode<T> currNode = _head;
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
            _last = currNode;
        }

        return removedValue;
    }    

    #endregion

    #region Reverse

    public LinkedList<T> Reverse()
    {
        LinkedList<T> reversed = new LinkedList<T>();
        if (_head == null)
        {
            return reversed;
        }

        foreach (T item in this)
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
        if (_head != null)
        {
            foreach (T item in this)
            {
                concatenationResult.AddLast(item);
            }
        }

        if (other._head != null)
        {
            foreach (T item in other)
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

        if (_head == null || other._head == null)
        {
            return intersectionResult;
        }

        foreach (T item in this)
        {
            if (other.Find(item, comparer) != null)
            {
                intersectionResult.AddLast(item);
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

        if (_head == null)
        {
            foreach (T item in other)
            {
                unionResult.AddLast(item);
            }

            return unionResult;
        }

        foreach (T item in this)
        {
            unionResult.AddLast(item);
        }

        if (other == null)
        {
            return unionResult;
        }

        foreach (T item in other)
        {
            if (Find(item, comparer) == null)
            {
                unionResult.AddLast(item);
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
        LinkedList<T> complementResult = new LinkedList<T>();
        if (other == null)
        {
            foreach (T item in this)
            {
                complementResult.AddLast(item);
            }
        }

        foreach (var item in this)
        {
            if (other?.Find(item, comparer) == null)
            {
                complementResult.AddLast(item);
            }
        }

        return complementResult;
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
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, Comparer<T>.Default);
        return new LinkedList<T>(sorted);
    }

    public LinkedList<T> Sort(SortingMethodsImpl.SortingMode sortingMode,
        SortingMethodsImpl.SortingMethod sortingMethod, IComparer<T> comparer)
    {
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, comparer);
        return new LinkedList<T>(sorted);
    }
    
    public LinkedList<T> Sort(SortingMethodsImpl.SortingMode sortingMode,
        SortingMethodsImpl.SortingMethod sortingMethod, Comparison<T> comparison)
    {
        T[] sorted = ToArray().Sort(sortingMode, sortingMethod, comparison);
        return new LinkedList<T>(sorted);
    }


    #endregion

    #region Action

    public LinkedList<T> DoAction(Action<T> action)
    {
        ThrowIfEmptyList();
        foreach (var item in this)
        {
            action(item);
        }

        return this;
    }

    #endregion

    #region Equal

    public override bool Equals(object? obj)
    {
        if (obj is LinkedList<T> another)
        {
            return Equals(another);
        }

        return false;
    }
    
    public bool Equals(LinkedList<T>? other)
    {
        if (other == null)
        {
            return false;
        }

        if (_head == null && other._head == null)
        {
            return true;
        }
        else if (_head == null || other._head == null)
        {
            return false;
        }

        LinkedListNode<T> currThis = _head, currOther = other._head;
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

    public static bool operator ==(LinkedList<T>? list, LinkedList<T> other)
    {
        return list.Equals(other);
    }

    public static bool operator !=(LinkedList<T> list, LinkedList<T> other)
    {
        return (!(list.Equals(other)));
    }

    #endregion

    #region Operator*

    private LinkedList<T> Multiplying(LinkedList<T>? another)
    {
        LinkedList<T> toReturn = new LinkedList<T>();
        if (another == null)
        {
            return toReturn;
        }

        if (_head == null || another._head == null)
        {
            return toReturn;
        }

        LinkedListNode<T> currThis = _head, currAnother = another._head;

        while (currThis != null && currAnother != null)
        {
            dynamic first = currThis.Value;
            toReturn.AddLast(first * currAnother.Value);

            currThis = currThis.next;
            currAnother = currAnother.next;
        }
        
        return toReturn;
    }

    public static LinkedList<T> operator *(LinkedList<T> list, LinkedList<T>? other)
    {
        return list.Multiplying(other);
    }

    #endregion

    #region Conctracts and interfaces

    public object Clone()
    {
        return new LinkedList<T>(this);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new LinkedListEnum<T>(_head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public override string ToString()
    {
        string toReturn = "";
        toReturn += string.Join(", ", this);
        toReturn += Environment.NewLine;
        return toReturn;
    }

    public override int GetHashCode()
    {
        HashCode hc = new();
        foreach (var item in this)
        {
            hc.Add(item);
        }

        return hc.ToHashCode();
    }

    #endregion
}
