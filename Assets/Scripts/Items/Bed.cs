using Interfaces;
using PlayerScripts;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class Bed : MonoBehaviour, IInteractable
    {
        private Player _player;

        [SerializeField] private Transform _sleepTransform;

        private void Start()
        {
            _player = FindFirstObjectByType<Player>();
        }

        public void Interact()
        {
            _player.Sleep(_sleepTransform.position,  _sleepTransform.rotation);
        }
    }
}