using Basic;
using PlayerScripts;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class Bed : DefaultInteractable
    {
        private Player _player;

        [SerializeField] private Transform _sleepTransform;

        private void Start()
        {
            _player = FindFirstObjectByType<Player>();
        }

        public override void Interact()
        {
            _player.Sleep(_sleepTransform.position,  _sleepTransform.rotation);
        }
    }
}