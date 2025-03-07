using System;
using Interfaces;
using Models;
using UnityEngine;

namespace PlayerScripts
{
	public class PlayerCameraController : IPlayerCameraController
	{
		private Camera _camera;
		private Transform _transform;
		private CameraControllerSetting _settings;

		private readonly PlayerInput.PlayerActions _playerInput;

		public PlayerCameraController(PlayerInput input)
		{
			_playerInput = input.Player;
			
			_playerInput.Enable();
		}

		public void Initialize(CameraControllerSetting settings, Transform transform)
		{
			_settings = settings;
			_transform = transform;
			_camera = Camera.main;

			if (!IsValid()) return;
			
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void HandleInput()
		{
			if (!IsValid()) return;
			
			Vector2 input = _playerInput.Look.ReadValue<Vector2>();

			Vector3 cameraDelta = new Vector3(-input.y, 0, 0) * (_settings.Sensitivity * Time.deltaTime);
			Vector3 playerDelta = new Vector3(0, input.x, 0) * (_settings.Sensitivity * Time.deltaTime);

			Vector3 cameraRotation = _camera.transform.localEulerAngles + cameraDelta;
			Vector3 playerRotation = _transform.localEulerAngles + playerDelta;
			
			if (cameraRotation.x > 180) cameraRotation.x -= 360;
			
			cameraRotation.x = Mathf.Clamp(cameraRotation.x, -89f, 89f);
			
			_camera.transform.localRotation = Quaternion.Euler(cameraRotation);
			_transform.rotation = Quaternion.Euler(playerRotation);
		}

		private bool IsValid()
		{
			try
			{
				if (_camera == null) throw new NullReferenceException("Camera is null");
				if (_transform == null) throw new NullReferenceException("Player transform is null");
				if (_settings == null) throw new NullReferenceException("Settings is null");
			}
			catch (Exception exception)
			{
				Debug.LogError(exception.Message);
				return false;
			}

			return true;
		}
	}
}