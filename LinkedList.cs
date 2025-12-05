using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM5113_Assignment_WinForm
{
    // used for storing the path
    internal class LinkedListNode<T>
    {
        public T Value { get; set; }
        public LinkedListNode<T>? Next { get; set; }

        public LinkedListNode(T value)
        {
            Value = value;
        }
    }

    // linked list 
    internal class LinkedList<T>
    {
        private LinkedListNode<T>? head;
        private LinkedListNode<T>? tail;

        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;

        public LinkedListNode<T>? Head => head;

        public void AddLast(T item)
        {
            var node = new LinkedListNode<T>(item);

            if (head == null)
                head = tail = node;
            else
            {
                tail!.Next = node;
                tail = node;
            }

            Count++;
        }

        public void AddFirst(T item)
        {
            var node = new LinkedListNode<T>(item)
            {
                Next = head
            };

            head = node;

            if (tail == null)
                tail = head;

            Count++;
        }

        public T RemoveFirst()
        {
            if (head == null)
                throw new InvalidOperationException("List is empty.");

            var value = head.Value;
            head = head.Next;

            if (head == null)
                tail = null;

            Count--;
            return value;
        }

        public T PeekFirst()
        {
            if (head == null)
                throw new InvalidOperationException("List is empty.");
            return head.Value;
        }
    }
}

