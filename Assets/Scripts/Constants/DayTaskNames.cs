using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DayTaskNames
    {
        public static readonly Dictionary<DayTask, string> TaskToName = new()
        {
            { DayTask.Shower, "Take a shower" },
            { DayTask.WashHands, "Wash hands" }
        };
    }
}