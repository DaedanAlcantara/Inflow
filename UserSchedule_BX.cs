using System;

namespace Inflow
{
    public class UserSchedule_BX
    {
        public string Username { get; set; }
        public TimeSpan MorningStart { get; set; }
        public TimeSpan MorningEnd { get; set; }
        public TimeSpan AfternoonStart { get; set; }
        public TimeSpan AfternoonEnd { get; set; }

        public UserSchedule_BX(string username, TimeSpan morningStart, TimeSpan morningEnd,
                               TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            Username = username;
            MorningStart = morningStart;
            MorningEnd = morningEnd;
            AfternoonStart = afternoonStart;
            AfternoonEnd = afternoonEnd;
        }
    }
}