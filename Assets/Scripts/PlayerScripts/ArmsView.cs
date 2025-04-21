using Interfaces;
using Items;
using UnityEngine;

namespace PlayerScripts
{
    public class ArmsView : MonoBehaviour
    {
        [SerializeField] private Transform _hand;

        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _maxHandDistance = 3;

        private Camera _camera;
        private Player _player;
        private IInteractor _interactor;
        private Animator _animator;
        private Vector3 _positionTarget;
        private Vector3 _initialTarget;
        private Transform _transformTarget;

        private Vector3 HandTarget => _transformTarget != null
            ? _hand.parent.transform.InverseTransformPoint(_transformTarget.position)
            : _positionTarget;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _animator = GetComponent<Animator>();
            _interactor = _player.Interactor;
            _camera = Camera.main;
        }

        private void Start()
        {
            _initialTarget = _hand.localPosition;
            SetTarget(_initialTarget);
        }

        private void Update()
        {
            MoveHand();
        }

        private void MoveHand()
        {
            bool isAtInitial = Vector3.Distance(HandTarget, _initialTarget) < 0.01f;
            bool isTooFar = Vector3.Distance(_camera.transform.position, _hand.position) > _maxHandDistance;

            if (!isAtInitial && isTooFar)
            {
                SetTarget(_initialTarget);
                _animator.SetTrigger("Idle");
                return;
            }
            
            _hand.localPosition = Vector3.MoveTowards(_hand.localPosition, HandTarget, _movementSpeed * Time.deltaTime);
        }
        
        private void OnInteracted(GameObject item)
        {
            if (item == null) return;

            if (item.GetComponent<Door>() is Door door) HandleDoor(door);
        }

        private void HandleDoor(Door door)
        {
            if (door == null) return;

            SetTarget(door.HandleTransform);

            _animator.SetTrigger("Door");
        }

        private void SetTarget(Transform target)
        {
            _transformTarget = target;
            _positionTarget = _initialTarget;
        }

        private void SetTarget(Vector3 target)
        {
            _positionTarget = target;
            _transformTarget = null;
        }
        
        private void OnEnable() => _interactor.Interacted += OnInteracted;

        private void OnDisable() => _interactor.Interacted -= OnInteracted;
    }
}