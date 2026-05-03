using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Color CardColor { get; set; }  // Permanent color for this task

        public Task_BX(string name, int priority, TimeSpan duration, TimePreference_BX timePref, string description = "")
        {
            Name = name;
            Priority = priority;
            Duration = duration;
            TimePreference = timePref;
            Description = description;
            CardColor = GenerateRandomColor();  // Generate color once when task is created
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

        private Color GenerateRandomColor()
        {
            // List of pleasant, readable hex colors for task cards
            string[] colorHexList = new string[]
            {
                "#FFE5E5", // Soft Red
                "#E5FFE5", // Soft Green
                "#E5E5FF", // Soft Blue
                "#FFFCE5", // Soft Yellow
                "#FFE5F0", // Soft Pink
                "#E5F0FF", // Soft Light Blue
                "#F0E5FF", // Soft Purple
                "#E5FFF0", // Soft Mint
                "#FFE5CC", // Soft Orange
                "#F5E6E8", // Soft Rose
                "#E6F5F5", // Soft Cyan
                "#F5F5E6", // Soft Cream
                "#E8E8E8", // Soft Gray
                "#FFF0E6", // Soft Peach
                "#E6FFF0"  // Soft Seafoam
            };

            Random random = new Random();
            int randomIndex = random.Next(colorHexList.Length);
            return ColorTranslator.FromHtml(colorHexList[randomIndex]);
        }
    }
}