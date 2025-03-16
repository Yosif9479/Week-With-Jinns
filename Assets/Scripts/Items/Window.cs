using System;
using System.Collections.Generic;
using System.Linq;
using Basic;
using Models;
using PlayerScripts;
using UnityEngine;

namespace Items
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Light[] _lights;
        [SerializeField] private List<WindowTimedSetting> _timeSettings;
        [SerializeField] private bool _useDynamicColors;
        
        private WindowTimedSetting CurrentSetting
        {
            get
            {
                return _timeSettings.LastOrDefault(x => DayTime.CurrentTime.Hours >= x.Hours);
            }
        }
        private WindowTimedSetting NextSetting
        {
            get
            {
                return _timeSettings.FirstOrDefault(x => x.Hours > DayTime.CurrentTime.Hours);
            }
        }

        private void Start()
        {
            InsertFirstSetting();

            Player.ClosedEyes += OnPlayerClosedEyes;
        }

        private void Update()
        {
            HandleLights();
        }
        
        private void OnPlayerClosedEyes()
        {
            foreach (Light light in _lights)
            {
                light.intensity = _timeSettings[0].Intensity;
                light.color = _timeSettings[0].Color;
            }
        }

        private void HandleLights()
        {
            if (_timeSettings.Count == 0 || NextSetting == null) return;

            TimeSpan time = DayTime.CurrentTime;

            float currentTimeInMinutes = time.Hours * 60 + time.Minutes;
            float startTimeInMinutes = CurrentSetting.Hours * 60;
            float endTimeInMinutes = NextSetting.Hours * 60;
    
            float ratio = Mathf.InverseLerp(startTimeInMinutes, endTimeInMinutes, currentTimeInMinutes);
    
            float intensity = Mathf.Lerp(CurrentSetting.Intensity, NextSetting.Intensity, ratio);

            foreach (Light light in _lights)
            {
                light.intensity = intensity;
            }

            if (!_useDynamicColors) return;
    
            Color color = Color.Lerp(CurrentSetting.Color, NextSetting.Color, ratio);

            foreach (Light light in _lights)
            {
                light.color = color;
            }
        }
        
        private void InsertFirstSetting()
        {
            if (_lights.Length == 0) return;

            Light light = _lights[0];
            
            _timeSettings.Insert(0, new WindowTimedSetting
            {
                Hours = DayTime.CurrentTime.Hours,
                Color = light.color,
                Intensity = light.intensity,
            });
        }
    }
}