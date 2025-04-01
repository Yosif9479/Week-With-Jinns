using Interfaces;
using UnityEngine;

namespace PlayerScripts
{
	public class Pickable : MonoBehaviour, IPickable
	{
		public virtual void OnPickedUp() => Debug.Log($"Picked up {this}");

		public virtual void OnDropped() => Debug.Log($"Dropped {this}");
		
		public virtual bool CanBePickedUp() => true;
	}
}