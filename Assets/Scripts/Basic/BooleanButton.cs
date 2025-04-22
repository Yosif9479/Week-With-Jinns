using UnityEngine;

namespace Basic
{
	public class BooleanButton : DefaultInteractable
	{
		[SerializeField] private GameObject _target;
		
		public override void Interact()
		{
			_target.SetActive(!_target.activeInHierarchy);
		}
	}
}