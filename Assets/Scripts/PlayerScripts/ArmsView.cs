using System.Linq;
using Interfaces;
using Items;
using ItemScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class ArmsView : MonoBehaviour
    {
        private static readonly int Pickup = Animator.StringToHash("Pickup");
        private static readonly int Interact = Animator.StringToHash("Interact");
        private static readonly int Use = Animator.StringToHash("Use");
        private static readonly int Drop = Animator.StringToHash("Drop");
        
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

        #region MONO
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
        
        private void OnEnable()
        {
            _interactor.Interacted += OnInteracted;
            _interactor.ItemUsed += OnItemUsed;
            _interactor.ItemDropped += OnItemDropped;
        }

        private void OnDisable()
        {
            _interactor.Interacted -= OnInteracted;
            _interactor.ItemUsed -= OnItemUsed;
            _interactor.ItemDropped -= OnItemDropped;
        }

        #endregion

        private void MoveHand()
        {
            bool isAtInitial = Vector3.Distance(HandTarget, _initialTarget) < 0.01f;
            bool isTooFar = Vector3.Distance(_player.transform.position, _hand.position) > _maxHandDistance;
            Vector3 targetWorldPosition = _hand.parent.TransformPoint(HandTarget);

            Vector3 toTarget = targetWorldPosition - _player.transform.position;
            toTarget.y = 0;

            Vector3 forward = _player.transform.forward;
            forward.y = 0;

            float handAngle = Vector3.Angle(forward, toTarget);
            
            if ((Mathf.Abs(handAngle) > 90 || isTooFar) && !isAtInitial)
            {
                SetTarget(_initialTarget);
                return;
            }
            
            _hand.localPosition = Vector3.MoveTowards(_hand.localPosition, HandTarget, _movementSpeed * Time.deltaTime);
        }
        
        private void OnInteracted(GameObject item)
        {
            if (item == null) return;

            if (item.GetComponent<Door>() is Door door) HandleDoor(door);

            if (item.GetComponent<IPickable>() is IPickable pickable)
            {
                _animator.runtimeAnimatorController = pickable.AnimatorOverride();
                _animator.SetTrigger(Pickup);
            }
            else if (item.GetComponent<IInteractable>() is IInteractable interactable)
            {
                _animator.runtimeAnimatorController = interactable.AnimatorOverride();
                _animator.SetTrigger(Interact);
            }
        }
        
        private void OnItemUsed(GameObject item)
        {
            if (item.GetComponent<IUsable>() is not IUsable usable) return;
            
            _animator.runtimeAnimatorController = usable.AnimatorOverride();
            
            _animator.SetTrigger(Use);
        }

        private void OnItemDropped(GameObject item)
        {
            if (item.GetComponent<Pickable>() is not IPickable pickable) return;
            
            _animator.runtimeAnimatorController = pickable.AnimatorOverride();
            
            _animator.SetTrigger(Drop);
        }

        private void HandleDoor(Door door)
        {
            if (door == null) return;

            Transform closestHandle = door.Handles.OrderBy(x => Vector3.Distance(x.position, _hand.position)).FirstOrDefault();

            if (closestHandle == null) return;
            
            SetTarget(closestHandle);
        }
        
        #region UTILS
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
        #endregion
    }
}