using System.Linq;
using Models;
using UnityEngine;

namespace ItemScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class CollisionSound : MonoBehaviour
    {
        [SerializeField] private ThresholdToSound[] _thresholds = {};
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            ThresholdToSound threshold = _thresholds.LastOrDefault(x => x.Threshold <= collision.relativeVelocity.magnitude);

            if (threshold == null) return;
            
            _audioSource.PlayOneShot(threshold.Clip);
        }

        private void Reset()
        {
            var audioSource = GetComponent<AudioSource>();

            if (_thresholds.Length == 0)
            {
                _thresholds = new ThresholdToSound[]
                {
                    new() { Threshold = 1, Clip = audioSource.clip }
                };
            }
        }
    }
}