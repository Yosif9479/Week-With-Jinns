using System;
using System.Collections;
using Basic;
using PlayerScripts;
using UnityEngine;
using TMPro;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class Clock : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        private AudioSource _audioSource;
        private bool _isStopped;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Start()
        {
            Player.ClosedEyes += OnPlayerClosedEyes;
        }

        private void OnPlayerClosedEyes()
        {
            _audioSource.Play();
        }

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