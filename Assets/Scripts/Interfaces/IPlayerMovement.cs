using Models;
using UnityEngine;

namespace Interfaces
{
	public interface IPlayerMovement
	{
		public void Initialize(MovementSetting settings, CharacterController characterController);
		public void ApplyMovement();
	}
}