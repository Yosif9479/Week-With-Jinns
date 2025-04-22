using System.Collections;
using Basic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class Skibidi : DefaultInteractable
    {
        [SerializeField] private float _flushDurationSeconds;
        [SerializeField] private float _flushRefillSeconds;
        [SerializeField] private GameObject _water;
        
        private AudioSource _audioSource;
        private bool _isFlushing;
        private bool _isRefilling;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            if (_isFlushing || _isRefilling) return;
            
            _water.SetActive(true);
            _audioSource.Play();
            _isFlushing = true;
            
            StartCoroutine(StopFlushing());
        }

        private IEnumerator StopFlushing()
        {
            yield return new WaitForSeconds(_flushDurationSeconds);
            _water.SetActive(false);
            _isFlushing = false;
            _isRefilling = true;
            StartCoroutine(StopRefilling());
        }

        private IEnumerator StopRefilling()
        {
            yield return new WaitForSeconds(_flushRefillSeconds);
            _isRefilling = false;
            _audioSource.Stop();
        }
    }
}