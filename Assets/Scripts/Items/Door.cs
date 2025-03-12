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
        
        private Quaternion _closedRotation;
        private Quaternion _openRotation;
        
        private AudioSource _audioSource;
        private Quaternion _targetRotation;
        private bool _isRotating;

        private const float RotationFactor = 90f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            InitOpenAndClosedRotation();
        }

        private void InitOpenAndClosedRotation()
        {
            _closedRotation = transform.localRotation;
            Vector3 openEulers = transform.localEulerAngles + Vector3.up * RotationFactor;
            _openRotation = Quaternion.Euler(openEulers); 
    
            if (_openByDefault) 
            {
                transform.localRotation = _openRotation;
                _targetRotation = _openRotation;
            }
        }

        private void Update()
        {
            if (_isRotating) Rotate();
        }
        
        public void Interact()
        {
            if (_isRotating) return;
    
            bool isOpen = Quaternion.Angle(transform.localRotation, _openRotation) < 1f;
    
            _targetRotation = isOpen ? _closedRotation : _openRotation;

            _isRotating = true;

            _audioSource.PlayOneShot(isOpen ? _closeSound : _openSound);
        }

        private void Rotate()
        {
            float step = RotationFactor / _rotationTimeSeconds * Time.deltaTime;
            
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetRotation, step);

            if (transform.localRotation == _targetRotation) _isRotating = false;
        }
    }
}
