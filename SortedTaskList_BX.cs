using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inflow
{
    internal class SortedTaskList_BX : IEnumerable<Task_BX>
    {
        private readonly List<Task_BX> _tasks = new List<Task_BX>();

        public void Add(Task_BX task)
        {
            _tasks.Add(task);
            _tasks.Sort();
        }

        public bool Remove(Task_BX task)
        {
            bool removed = _tasks.Remove(task);
            if (removed)
                _tasks.Sort();
            return removed;
        }

        public IEnumerator<Task_BX> GetEnumerator() => _tasks.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _tasks.Count;
        public Task_BX this[int index] => _tasks[index];
    }
}
