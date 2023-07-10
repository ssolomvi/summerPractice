using System.Collections;

namespace summer_practice_domain.LinkedList;

public sealed class LinkedListEnum<T> : IEnumerator<T>
{
    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public T Current { get; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}