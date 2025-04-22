using UnityEngine;

namespace Interfaces
{
	public interface IUsable
	{
		public AnimatorOverrideController AnimatorOverride();
		public void Use(GameObject usedOn = null);
	}
}