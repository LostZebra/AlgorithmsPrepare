using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    public class Queue
    {
        private readonly Stack<int> _s1 = new Stack<int>();
        private readonly Stack<int> _s2 = new Stack<int>();

        // Push element x to the back of queue.
        public void Push(int x)
        {
            _s1.Push(x);
        }

        // Removes the element from front of queue.
        public void Pop()
        {
            if (this.Empty())
            {
                return;
            }

            if (_s2.Count != 0)
            {
                _s2.Pop();
            }
            else
            {
                while (_s1.Count > 1)
                {
                    _s2.Push(_s1.Pop());
                }
                _s1.Pop();
            }
        }

        // Get the front element.
        public int Peek()
        {
            if (_s2.Count != 0)
            {
                return _s2.Peek();
            }
            while (_s1.Count != 0)
            {
                _s2.Push(_s1.Pop());
            }
            return _s2.Peek();
        }

        // Return whether the queue is empty.
        public bool Empty()
        {
            return _s1.Count == 0 && _s2.Count == 0;
        }
    }
}
