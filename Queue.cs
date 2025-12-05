using System;

namespace Assessment
{
    // This is a simple queue that uses our LinkedList
    internal class Queue<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();

        // This adds an item to the back of the queue
        public void Enqueue(T data)
        {
            _list.PushBack(data);
        }

        // This removes the front item
        public bool Dequeue(ref T data)
        {
            return _list.PopFront(ref data);
        }

        // This then removes and returns the front item
        public T Pop()
        {
            T value = default!;
            Dequeue(ref value);
            return value;
        }

        // This checks if the queue is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }

        // This checks if the queue contains a value
        public bool Contains(T data)
        {
            return _list.Contains(data);
        }
    }
}
