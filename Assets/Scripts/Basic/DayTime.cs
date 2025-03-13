using System;
using UnityEngine;

namespace Basic
{
    public static class DayTime
    {
        private const int DayStartHours = 9;
        private const int DayEndHours = 22;
        public static TimeSpan CurrentTime
        {
            get
            {
                TimeSpan timePassed = DateTime.Now - _startTime;
                
                int currentTime = DayStartHours * 60 + timePassed.Minutes * 60 + timePassed.Seconds;
                int minutes = currentTime % 60;
                int hours = Mathf.Clamp(currentTime / 60, DayStartHours, DayEndHours);

                if (hours == DayEndHours) minutes = 0;

                return new TimeSpan(hours, minutes, 0);
            }
        }

        private static DateTime _startTime = DateTime.Now;
        
        public static void Start() => _startTime = DateTime.Now;
    }
}