using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow
{
    internal class AppState
    {
        public static int NitroElapsedSeconds = 0;

        public static User_BX CurrentUser { get; set;  }
        public static int TotalFinishedTasks { get; set; } = 0;
        public static int TotalDroppedTasks { get; set; } = 0;
        public static int CurrentStreak { get; set; } = 0;
        public static int ConsecutiveFinishes { get; set; } = 0;
    }
}
