using System;
using System.Collections.Generic;
using System.Linq;

namespace Inflow
{
    internal class User_BX
    {
        public string Username { get; set; }
        public UserSchedule_BX Schedule { get; set; }

        // Unsorted storage (used until SortTasks is called)
        private List<Task_BX> _morningTasksUnsorted;
        private List<Task_BX> _afternoonTasksUnsorted;

        // Sorted storage (only used after SortTasks is called)
        private SortedTaskList_BX _morningTasksSorted;
        private SortedTaskList_BX _afternoonTasksSorted;

        private bool _isSorted = false;

        // Public properties – use Count() and ToList() from LINQ
        public IEnumerable<Task_BX> MorningTasks => _isSorted
            ? (IEnumerable<Task_BX>)_morningTasksSorted
            : _morningTasksUnsorted;
        public IEnumerable<Task_BX> AfternoonTasks => _isSorted
            ? (IEnumerable<Task_BX>)_afternoonTasksSorted
            : _afternoonTasksUnsorted;

        public User_BX(string username, UserSchedule_BX schedule)
        {
            Username = username;
            Schedule = schedule;
            _morningTasksUnsorted = new List<Task_BX>();
            _afternoonTasksUnsorted = new List<Task_BX>();
        }

        public void AddTask(Task_BX task)
        {
            if (_isSorted)
            {
                // Add to sorted lists – they stay sorted automatically
                if (task.TimePreference == TimePreference_BX.Morning)
                    _morningTasksSorted.Add(task);
                else
                    _afternoonTasksSorted.Add(task);
            }
            else
            {
                // Insert at beginning so newest tasks appear first
                if (task.TimePreference == TimePreference_BX.Morning)
                    _morningTasksUnsorted.Insert(0, task);
                else
                    _afternoonTasksUnsorted.Insert(0, task);
            }
        }

        public bool RemoveTask(Task_BX task)
        {
            bool removed;
            if (_isSorted)
            {
                if (task.TimePreference == TimePreference_BX.Morning)
                    removed = _morningTasksSorted.Remove(task);
                else
                    removed = _afternoonTasksSorted.Remove(task);
            }
            else
            {
                if (task.TimePreference == TimePreference_BX.Morning)
                    removed = _morningTasksUnsorted.Remove(task);
                else
                    removed = _afternoonTasksUnsorted.Remove(task);
            }
            return removed;
        }

        /// <summary>
        /// Converts unsorted lists to SortedTaskList_BX (sorting by priority, highest first)
        /// and switches to sorted mode. After this, new tasks are added directly to sorted lists.
        /// </summary>
        public void SortTasks()
        {
            if (_isSorted) return;

            _morningTasksSorted = new SortedTaskList_BX();
            foreach (var task in _morningTasksUnsorted)
                _morningTasksSorted.Add(task);

            _afternoonTasksSorted = new SortedTaskList_BX();
            foreach (var task in _afternoonTasksUnsorted)
                _afternoonTasksSorted.Add(task);

            // Clear unsorted lists and mark as sorted
            _morningTasksUnsorted.Clear();
            _afternoonTasksUnsorted.Clear();
            _isSorted = true;
        }
    }
}