using System;
using System.Collections;
using Basic;
using UnityEngine;
using TMPro;

namespace Items
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        private bool _isStopped;

        private void Update()
        {
            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            TimeSpan time = DayTime.CurrentTime;
                
            _text.text = $"{time.Hours:00}:{time.Minutes:00}";
        }
    }
}