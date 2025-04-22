using UnityEngine;

namespace Interfaces
{
	public interface IInteractable
	{
		public AnimatorOverrideController AnimatorOverride();
		public void Interact();
	}
}