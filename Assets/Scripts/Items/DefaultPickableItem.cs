using PlayerScripts;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultPickableItem : Pickable
    {
        [SerializeField] private float _dropForce = 1f;
        
        private Rigidbody _rigidbody;
        private Camera _camera;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            
            _camera = Camera.main;
        }

        public override void OnPickedUp()
        {
            base.OnPickedUp();
            
            transform.rotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        public override void OnDropped()
        {
            base.OnDropped();
            
            transform.rotation = Quaternion.identity;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(_camera.transform.forward * _dropForce, ForceMode.Impulse);
        }
    }
}