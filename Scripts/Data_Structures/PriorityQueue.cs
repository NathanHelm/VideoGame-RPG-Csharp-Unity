using System;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> 
{

        private SortedSet<Tuple<int, T>> set = new SortedSet<Tuple<int, T>>();

        public void Enqueue(T item, int priority)
        {
            set.Add(Tuple.Create(priority, item));
        }

        public T Dequeue()
        {
            if (set.Count == 0)
                throw new InvalidOperationException("Priority queue is empty");

            var item = set.Min;
            set.Remove(item);

            return item.Item2;
        }

        public int Count => set.Count;
}
