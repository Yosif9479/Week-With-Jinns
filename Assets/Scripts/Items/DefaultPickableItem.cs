using PlayerScripts;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultPickableItem : Pickable
    {
        [SerializeField] private float _dropForce = 1f;
        [SerializeField] private bool _useCustomRotation;
        [SerializeField] private Vector3 _customRotation;
        [SerializeField] private bool _useCustomPosition;
        [SerializeField] private Vector3 _customPosition;
        
        private Rigidbody _rigidbody;
        private Camera _camera;
        private RigidbodyInterpolation _interpolation;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            
            _camera = Camera.main;
            
            _interpolation = _rigidbody.interpolation;
        }

        public override void OnPickedUp()
        {
            base.OnPickedUp();
            
            Quaternion rotation = _useCustomRotation ? Quaternion.Euler(_customRotation) : Quaternion.identity;
            Vector3 position = _useCustomPosition ? _customPosition : Vector3.zero;
            
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            transform.localRotation = rotation;
            transform.localPosition = position;

            _rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        public override void OnDropped()
        {
            base.OnDropped();
            
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(_camera.transform.forward * _dropForce, ForceMode.Impulse);
            _rigidbody.interpolation = _interpolation;
        }
    }
}