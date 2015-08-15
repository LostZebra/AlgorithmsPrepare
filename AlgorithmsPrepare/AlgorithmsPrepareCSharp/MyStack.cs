using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    class MyStack
    {
        private Queue<int> a = new Queue<int>();
        private Queue<int> b = new Queue<int>();

        // Push element x onto stack.
        public void Push(int x)
        {
            if (b.Count == 0)
            {
                a.Enqueue(x);
            }
            else
            {
                b.Enqueue(x);
            }
        }

        // Removes the element on top of the stack.
        public void Pop()
        {
            var full = a.Count == 0 ? b : a;
            var empty = a.Count == 0 ? a : b;
            while (full.Count != 1)
            {
                empty.Enqueue(full.Dequeue());
            }

            full.Dequeue();
        }

        // Get the top element.
        public int Top()
        {
            var full = a.Count == 0 ? b : a;
            var empty = a.Count == 0 ? a : b;
            while (full.Count != 1)
            {
                empty.Enqueue(full.Dequeue());
            }

            int top = full.Peek();
            empty.Enqueue(full.Dequeue());
            return top;
        }

        // Return whether the stack is empty.
        public bool Empty()
        {
            return a.Count == 0 && b.Count == 0;
        }
    }
}
