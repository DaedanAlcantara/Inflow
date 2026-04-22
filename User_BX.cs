using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow
{
    internal class User_BX
    {
        public string Username { get; set; }
        public UserSchedule_BX Schedule { get; set; }
        public SortedTaskList_BX MorningTasks { get; private set; }
        public SortedTaskList_BX AfternoonTasks { get; private set; }

        public User_BX(string username, UserSchedule_BX schedule)
        {
            Username = username;
            Schedule = schedule;
            MorningTasks = new SortedTaskList_BX();
            AfternoonTasks = new SortedTaskList_BX();
        }

        public void AddTask(Task_BX task)
        {
            if (task.TimePreference == TimePreference_BX.Morning)
                MorningTasks.Add(task);
            else
                AfternoonTasks.Add(task);
        }

        public bool RemoveTask(Task_BX task)
        {
            return MorningTasks.Remove(task) || AfternoonTasks.Remove(task);
        }
    }
}
