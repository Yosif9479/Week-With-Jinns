using Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
	[RequireComponent(typeof(AudioSource))]
	public class FallingItem : MonoBehaviour
	{
		[SerializeField] private PlayerTrigger _trigger;
		[SerializeField] private Transform _fallTransform;
		[SerializeField] private float _fallChance = 100;
		
		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			_trigger.Triggered += Fall;
		}

		private void OnDisable()
		{
			_trigger.Triggered -= Fall;
		}

		private void Fall()
		{
			if (transform.position == _fallTransform.position) return;

			if (!RandomChance()) return;
			
			transform.position = _fallTransform.position;
			transform.rotation = _fallTransform.rotation;
			
			_audioSource.Play();
		}

		private bool RandomChance()
		{
			int value = Random.Range(0, 100);

			return value <= _fallChance;
		}
	}
}