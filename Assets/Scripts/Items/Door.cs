using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _openByDefault;
        [SerializeField] private float _rotationTimeSeconds = 1f;
        
        [Header("Audio")]
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;
        
        private Vector3 _closedRotation;
        private Vector3 _openRotation;
        
        private AudioSource _audioSource;
        private Vector3 _targetRotation;
        private bool _isRotating;

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
            
            if (_openByDefault) transform.localEulerAngles = _openRotation;
        }

        private void FixedUpdate()
        {
            if (_isRotating) Rotate();
        }
        
        public void Interact()
        {
            if (_isRotating) return;
            
            bool isOpen = Mathf.Approximately(transform.localEulerAngles.y, _openRotation.y);
            
            Vector3 rotation = isOpen ? _closedRotation : _openRotation;
            
            _targetRotation = rotation;

            _isRotating = true;

            _audioSource.PlayOneShot(isOpen ? _openSound : _closeSound);
        }

        private void Rotate()
        {
            Quaternion rotation = Quaternion.Euler(_targetRotation);
            
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotation, 90 / _rotationTimeSeconds * Time.fixedDeltaTime);

            if (Mathf.Approximately(transform.localEulerAngles.y, _targetRotation.y)) _isRotating = false;
        }
    }
}
