using System.Collections.Generic;

namespace COM5113_Assignment_WinForm
{
    // queue using linked list
    internal class Queue<T>
    {
        private LinkedList<T> list = new LinkedList<T>();

        public bool IsEmpty => list.IsEmpty;
        public int Count => list.Count;

        public void Enqueue(T item)
        {
            list.AddLast(item);
        }

        public T Dequeue()
        {
            return list.RemoveFirst();
        }

        public T Peek()
        {
            return list.PeekFirst();
        }
    }
}

