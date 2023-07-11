using System.Collections;

namespace summer_practice_domain.LinkedList;

public sealed class LinkedListEnum<T> : IEnumerator<T>
{
    private LinkedListNode<T>? _head;
    private LinkedListNode<T>? _current;
    
    public LinkedListEnum(LinkedListNode<T>? head)
    {
        _current = null;
        _head = head;
    }
    
    public bool MoveNext()
    {
        if (_current == null)
        {
            if (_head == null)
            {
                return false;
            }
            _current = _head;
            return true;
        }

        if (_current.next != null)
        {
            _current = _current.next;
            return true;    
        }

        return false;
    }

    public T Current
    {
        get
        {
            if (_current != null)
            {
                return _current.Value;
            }

            throw new NullReferenceException("Current node is null {0}");
        }
    }

    object IEnumerator.Current => Current;
    
    public void Reset()
    {
        _current = _head;
    }
    
    public void Dispose()
    {
        // not necessary
    }
}