using Interfaces;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _openOnStart;
        
        [Header("Audio")]
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;
        
        private Vector3 _closedRotation;
        private Vector3 _openRotation;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _closedRotation = transform.localEulerAngles;
            _openRotation = transform.localEulerAngles + Vector3.up * 90;
            
            if (_closedRotation.y >= 360) _closedRotation.y -= 360;
            if (_openRotation.y >= 360) _openRotation.y -= 360;
            
            if (_openOnStart) transform.localEulerAngles = _openRotation;
        }
        
        public void Interact()
        {
            bool isOpen = Mathf.Approximately(transform.localEulerAngles.y, _openRotation.y);
            
            Vector3 rotation = isOpen ? _closedRotation : _openRotation;
            
            transform.localEulerAngles = rotation;

            _audioSource.PlayOneShot(isOpen ? _openSound : _closeSound);
        }
    }
}
