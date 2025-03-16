using System;
using UnityEngine;
using VContainer.Unity;

namespace Basic
{
    public class DayTime : ITickable
    {
        private const int DayStartHours = 9;
        private const int DayEndHours = 22;
        private const float TimeSpeedMultiplier = 2f;
        
        private static float _passedTime;
        
        public static TimeSpan CurrentTime
        {
            get
            {
                var currentTime = Convert.ToInt32(DayStartHours * 60 + _passedTime);
                int minutes = currentTime % 60;
                int hours = Mathf.Clamp(currentTime / 60, DayStartHours, DayEndHours);

                if (hours == DayEndHours) minutes = 0;

                return new TimeSpan(hours, minutes, 0);
            }
        }

        public static DayOfWeek CurrentDay;
        
        public void Tick()
        {
            _passedTime += Time.deltaTime * TimeSpeedMultiplier;
        }
    }
}