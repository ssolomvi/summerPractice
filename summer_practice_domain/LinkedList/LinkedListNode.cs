namespace summer_practice_domain.LinkedList;

public sealed class LinkedListNode<T>
{
    internal LinkedListNode<T>? next;
    internal T item;

    public LinkedListNode(T value)
    {
        item = value;
    }

    public LinkedListNode<T>? Next => next;

    public T Value
    {
        get => item;
        set => item = value;
    }

    /// <summary>Gets a reference to the value held by the node.</summary>
    public ref T ValueRef => ref item;

    internal void Invalidate()
    {
        next = null;
    }
}