using Models;
using UnityEngine;

namespace Interfaces
{
	public interface IPlayerCameraController
	{
		public void Initialize(CameraControllerSetting settings, Transform transform);
		public void HandleInput();
	}
}