using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList
{
    public class CircularLinkedList<T> : IEnumerable<T>  
    {
        Node<T> head; 
        Node<T> tail; 
        int count;  
        public delegate void Delegate();
        public event Delegate Empty;
        public delegate void UpdateDelegate(CircularLinkedList<T> list, T events);
        public event UpdateDelegate AddEvent;
        public event UpdateDelegate DeleteEvent;


        public void Add(T data)
        {
            if (data == null)
                throw new ArgumentNullException();
            Node<T> node = new Node<T>(data);
            // if empty
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                tail = node;
            }
            AddEvent?.Invoke(this, node.Data);
            count++;
        }
        public void AddFirst(T data)
        {
            if (data == null)
                throw new ArgumentNullException();
            Node<T> node = new Node<T>(data);
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                head = node;
            }
            AddEvent?.Invoke(this, node.Data);
            count++;
        }
        public void RemoveFirst()
        {
            Node<T> current = head;
            if (IsEmpty) throw new NullReferenceException();
            
            if (count == 1)
            {
                head = tail = null;
                Empty?.Invoke();
                count = 0;
            }
            else
            {
                head = current.Next;
                tail.Next = current.Next;
                count--;
            }
            DeleteEvent?.Invoke(this, current.Data);
        }
        public void RemoveLast()
        {
            Node<T> current = head;
            Node<T> previous = null;

            if (IsEmpty) throw new NullReferenceException();

            do
            {
                if (current == tail)
                {
                    previous.Next = current.Next;
                    tail = previous;
                    count--;
                    if(tail==null)
                        Empty?.Invoke();

                }
                previous = current;
                current = current.Next;
            } while (current != head);
            DeleteEvent?.Invoke(this, current.Data);

        }
        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            if (IsEmpty) throw new NullReferenceException();

            do
            {
                if (current.Data.Equals(data))
                {
                    // If the node is in the middle or at the end
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        
                        if (current == tail)
                            tail = previous;
                    }
                    else // If the node is  head
                    {

                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.Next;
                            tail.Next = current.Next;
                        }
                    }
                    DeleteEvent?.Invoke(this, current.Data);
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            } while (current != head);

            return false;
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
            Empty?.Invoke();
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            if (current == null) return false;
            do
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            while (current != head);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != head);
        }
    }

}
