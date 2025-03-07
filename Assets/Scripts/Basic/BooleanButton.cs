using Interfaces;
using UnityEngine;

namespace Basic
{
	public class BooleanButton : MonoBehaviour, IInteractable
	{
		[SerializeField] private GameObject _target;
		
		public void Interact()
		{
			_target.SetActive(!_target.activeInHierarchy);
		}
	}
}