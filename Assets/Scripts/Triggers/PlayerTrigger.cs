using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
	[RequireComponent(typeof(Collider))]
	public class PlayerTrigger : MonoBehaviour
	{
		public event UnityAction Triggered; 
		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			Triggered?.Invoke();
		}
	}
}