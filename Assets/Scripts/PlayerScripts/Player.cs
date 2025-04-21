using Basic;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using VContainer;

namespace PlayerScripts
{
	[RequireComponent(typeof(CharacterController), typeof(Animator))]
	public class Player : MonoBehaviour
	{
		public static event UnityAction Slept;
		public static event UnityAction ClosedEyes;
		
		private bool _isStunned;
		private Animator _animator;
		private CharacterController _character;

		[SerializeField] private MovementSetting _movementSettingSettings;
		[SerializeField] private CameraControllerSetting _cameraSettings;
		[SerializeField] private InteractionSetting _interactionSetting;

		[Space] 
		
		public Vector3 SpawnPosition;
		public Quaternion SpawnRotation;
		[SerializeField] private Transform _itemHolder;

		[Inject] private IPlayerMovement _movement;
		[Inject] private IPlayerCameraController _cameraController;
		[Inject] public IInteractor Interactor;

		private const int AllowedSleepHour = 9;
		
		private void Awake()
		{
			_character = GetComponent<CharacterController>();
			_animator = GetComponent<Animator>();
			
			_movement.Initialize(_movementSettingSettings, _character);
			_cameraController.Initialize(_cameraSettings, transform);
			Interactor.Initialize(_interactionSetting, _itemHolder);
		}

		private void Start()
		{
			transform.SetPositionAndRotation(SpawnPosition, SpawnRotation);
		}
		
		private void Update()
		{
			if (_isStunned) return;
			
			_movement.ApplyMovement();
			_cameraController.HandleInput();
		}

		public void Sleep(Vector3 position, Quaternion rotation)
		{
			if (DayTime.CurrentTime.Hours < AllowedSleepHour) return;
			
			transform.SetPositionAndRotation(position, rotation);
			_animator.SetTrigger("Sleep");
			Slept?.Invoke();
			Stun();
		}

		public void Stun(float duration = 0)
		{
			_isStunned = true;

			if (duration > 0)
			{
				Invoke(nameof(StopStun), duration);
			}
		}

		private void StopStun()
		{
			_isStunned = false;
		}

		// Called from animation
		private void OnClosedEyes()
		{
			ClosedEyes?.Invoke();
			StopStun();
			transform.SetPositionAndRotation(SpawnPosition, SpawnRotation);
		}
	}
}