using Interfaces;
using UnityEngine;

namespace ItemScripts
{
	public class Pickable : MonoBehaviour, IPickable
	{
		[SerializeField] private AnimatorOverrideController _animatorOverride;
		public AnimatorOverrideController AnimatorOverride() => _animatorOverride;

		public virtual void OnPickedUp() => Debug.Log($"Picked up {this}");

		public virtual void OnDropped() => Debug.Log($"Dropped {this}");
		
		public virtual bool CanBePickedUp() => true;
	}
}