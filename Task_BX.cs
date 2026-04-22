using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow
{
    internal enum TimePreference_BX
    {
        Morning,
        Afternoon
    }

    internal class Task_BX : IComparable<Task_BX>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public TimeSpan Duration { get; set; }
        public TimePreference_BX TimePreference { get; set; }

        public Task_BX(string name, int priority, TimeSpan duration, TimePreference_BX timePref, string description = "")
        {
            Name = name;
            Priority = priority;
            Duration = duration;
            TimePreference = timePref;
            Description = description;
        }

        public int CompareTo(Task_BX other)
        {
            if (other == null) return 1;
            return other.Priority.CompareTo(this.Priority);
        }

        public override string ToString()
        {
            return $"[Priority {Priority}] {Name} ({Duration:hh\\:mm}) - {TimePreference}";
        }
    }
}
