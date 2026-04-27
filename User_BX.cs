using System.Collections.Generic;
using System.Linq;

namespace Inflow
{
    internal class User_BX
    {
        public string Username { get; set; }
        public UserSchedule_BX Schedule { get; set; }

        private List<Task_BX> _morningTasksUnsorted;
        private List<Task_BX> _afternoonTasksUnsorted;
        private SortedTaskList_BX _morningTasksSorted;
        private SortedTaskList_BX _afternoonTasksSorted;
        private bool _isSorted = false;

        public IEnumerable<Task_BX> MorningTasks => _isSorted ? (IEnumerable<Task_BX>)_morningTasksSorted : _morningTasksUnsorted;
        public IEnumerable<Task_BX> AfternoonTasks => _isSorted ? (IEnumerable<Task_BX>)_afternoonTasksSorted : _afternoonTasksUnsorted;
        public bool IsSorted => _isSorted;

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
                if (task.TimePreference == TimePreference_BX.Morning)
                    _morningTasksSorted.Add(task);
                else
                    _afternoonTasksSorted.Add(task);
            }
            else
            {
                // Append at the end (oldest first, newest last)
                if (task.TimePreference == TimePreference_BX.Morning)
                    _morningTasksUnsorted.Insert(0, task);
                else
                    _afternoonTasksUnsorted.Insert(0, task);
            }
        }

        public bool RemoveTask(Task_BX task)
        {
            if (_isSorted)
            {
                return task.TimePreference == TimePreference_BX.Morning
                    ? _morningTasksSorted.Remove(task)
                    : _afternoonTasksSorted.Remove(task);
            }
            else
            {
                return task.TimePreference == TimePreference_BX.Morning
                    ? _morningTasksUnsorted.Remove(task)
                    : _afternoonTasksUnsorted.Remove(task);
            }
        }

        public void SortTasks()
        {
            if (_isSorted) return;
            _morningTasksSorted = new SortedTaskList_BX();
            foreach (var t in _morningTasksUnsorted) _morningTasksSorted.Add(t);
            _afternoonTasksSorted = new SortedTaskList_BX();
            foreach (var t in _afternoonTasksUnsorted) _afternoonTasksSorted.Add(t);
            _morningTasksUnsorted.Clear();
            _afternoonTasksUnsorted.Clear();
            _isSorted = true;
        }
    }
}