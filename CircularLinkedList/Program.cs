using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList
{
    public class EventCatcher<T>
    {
        public void EventHandlerEmpty()
        {
            Console.WriteLine("List is empty");
        }
        public void EventHandlerAdd(CircularLinkedList<T> list, T value)
        {
            Console.WriteLine("Added value: " + value);
        }
        public void EventHandlerDelete(CircularLinkedList<T> list, T value)
        {
            Console.WriteLine("Deleted value: " + value);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CircularLinkedList<string> circularList = new CircularLinkedList<string>();
            EventCatcher<string> catcher = new EventCatcher<string>();
            circularList.Empty += catcher.EventHandlerEmpty;
            circularList.AddEvent += catcher.EventHandlerAdd;
            circularList.DeleteEvent += catcher.EventHandlerDelete;

            circularList.Add("Tom");
            circularList.Add("Bob");
            circularList.Add("Alice");
            circularList.Add("Jack");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }

            circularList.Remove("Bob");
            Console.WriteLine("\n После удаления: \n");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }
            circularList.RemoveLast();
            Console.WriteLine("\n После удаления останнього: \n");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }
            circularList.Clear();
            Console.ReadLine();
        }
    }
}
