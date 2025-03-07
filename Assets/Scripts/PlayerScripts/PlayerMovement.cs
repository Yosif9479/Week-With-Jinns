using System;
using Models;
using Interfaces;
using UnityEngine;

namespace PlayerScripts
{
	public class PlayerMovement : IPlayerMovement
	{
		private MovementSetting _settings;
		private CharacterController _character;
		private readonly PlayerInput.PlayerActions _playerInput;

		public PlayerMovement(PlayerInput input)
		{
			_playerInput = input.Player;
			
			_playerInput.Enable();
		}
		
		public void Initialize(MovementSetting settings, CharacterController characterController)
		{
			_character = characterController;
			_settings = settings;

			IsValid();
		}
		
		public void ApplyMovement()
		{
			if (!IsValid()) return;

			Vector2 input = _playerInput.Move.ReadValue<Vector2>();
			
			Vector3 forward = _character.transform.forward;
			Vector3 right = _character.transform.right;
			
			Vector3 movement = (forward * input.y + right * input.x).normalized;

			movement *= _settings.Speed;
			
			_character.SimpleMove(movement);
		}

		private bool IsValid()
		{
			try
			{
				if (_character == null) throw new NullReferenceException("Character controller is null");
				if (_settings == null) throw new NullReferenceException("Movement settings is null");
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