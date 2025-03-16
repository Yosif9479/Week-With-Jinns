using System;
using PlayerScripts;
using UnityEngine;
using VContainer.Unity;

namespace Basic
{
    public class DayTime : ITickable, IStartable
    {
        private const int DayStartHours = 9;
        private const int DayEndHours = 22;
        private const float TimeSpeedMultiplier = 2f;
        
        private static float _passedTime;
        private static bool _isStopped;
        
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

        public static DayOfWeek CurrentDay = DayOfWeek.Monday;
        
        public void Tick()
        {
            if (_isStopped) return;
            _passedTime += Time.deltaTime * TimeSpeedMultiplier;
        }

        public void Start()
        {
            Player.Slept += () => _isStopped = true;
            Player.ClosedEyes += OnPlayerClosedEyes;
        }

        private static void OnPlayerClosedEyes()
        {
            _isStopped = false;
            _passedTime = 0;
            CurrentDay++;
        }
    }
}