using Interfaces;
using Models;
using UnityEngine;
using VContainer;

namespace PlayerScripts
{
	[RequireComponent(typeof(CharacterController))]
	public class Player : MonoBehaviour
	{
		private CharacterController _character;

		[SerializeField] private MovementSetting _movementSettingSettings;
		[SerializeField] private CameraControllerSetting _cameraSettings;
		[SerializeField] private InteractionSetting _interactionSetting;

		[Space] 
		
		[SerializeField] private Transform _itemHolder;

		[Inject] private IPlayerMovement _movement;
		[Inject] private IPlayerCameraController _cameraController;
		[Inject] private IInteractor _interactor;
		
		private void Awake()
		{
			_character = GetComponent<CharacterController>();
			
			_movement.Initialize(_movementSettingSettings, _character);
			_cameraController.Initialize(_cameraSettings, transform);
			_interactor.Initialize(_interactionSetting, _itemHolder);
		}

		private void Update()
		{
			_movement.ApplyMovement();
			_cameraController.HandleInput();
		}
	}
}