using System;
using System.Collections.Generic;

namespace Assessment
{
    // Generic linked list
    public class LinkedList<T>
    {
        // One node in the list
        private class Element<U>
        {
            public U Data { get; }
            public Element<U>? Next { get; set; }

            public Element(U data)
            {
                Data = data;
                Next = null;
            }
        }

        // First item in the list
        private Element<T>? _head;

        public LinkedList()
        {
            _head = null;
        }

        // This checks if the list is empty
        public bool IsEmpty()
        {
            return _head == null;
        }

        // This then adds an item at the front
        public void PushFront(T data)
        {
            Element<T> newElement = new Element<T>(data);
            newElement.Next = _head;
            _head = newElement;
        }

        // This checks if something is in the list
        public bool Contains(T data)
        {
            Element<T>? current = _head;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        // This adds an item at the back
        public void PushBack(T data)
        {
            Element<T> newElement = new Element<T>(data);

            if (_head == null)
            {
                _head = newElement;
                return;
            }

            Element<T> current = _head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newElement;
        }

        // This removes the first item
        public bool PopFront(ref T data)
        {
            if (_head == null)
            {
                return false;
            }

            data = _head.Data;
            _head = _head.Next;
            return true;
        }

        // This removes the last item
        public bool PopBack(ref T data)
        {
            if (_head == null)
            {
                return false;
            }

            if (_head.Next == null)
            {
                data = _head.Data;
                _head = null;
                return true;
            }

            Element<T> current = _head;
            Element<T>? previous = null;

            while (current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            data = current.Data;
            previous!.Next = null;
            return true;
        }

        // This gets the first item without removing it
        public bool GetFront(ref T data)
        {
            if (_head == null)
            {
                return false;
            }

            data = _head.Data;
            return true;
        }

        // This gets the last item without removing it
        public bool GetBack(ref T data)
        {
            if (_head == null)
            {
                return false;
            }

            Element<T> current = _head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            data = current.Data;
            return true;
        }
    }
}
