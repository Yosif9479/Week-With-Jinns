using UnityEngine;

namespace Interfaces
{
	public interface IPickable
	{
		public AnimatorOverrideController AnimatorOverride();
		public void OnPickedUp();
		public void OnDropped();
		public bool CanBePickedUp();
	}
}