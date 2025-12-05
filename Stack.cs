using System;

namespace Assessment
{
    // This is a simple stack that uses our LinkedList
    internal class Stack<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();

        // This adds an item on top of the stack
        public void Push(T data)
        {
            _list.PushFront(data);
        }

        // This removes and returns the top item
        public T Pop()
        {
            T item = default!;
            _list.PopFront(ref item);
            return item;
        }

        // This checks if the stack is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }

        // This checks if the stack contains a value
        public bool Contains(T data)
        {
            return _list.Contains(data);
        }
    }
}
