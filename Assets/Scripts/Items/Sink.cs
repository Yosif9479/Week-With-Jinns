using Enums;
using Interfaces;
using PlayerScripts;
using TaskScripts;
using UnityEngine;

namespace Items
{
    public class Sink : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _water;
        [SerializeField] private Transform _handleTransform;
        [SerializeField] private float _washTime = 5f;

        private Player _player;
        private Quaternion _handleClosedRotation;
        [SerializeField] private Quaternion _handleOpenRotation;

        private void Start()
        {
            _player = FindFirstObjectByType<Player>();
            _handleClosedRotation = _handleTransform.localRotation;
        }
        
        public void Interact()
        {
            _water.SetActive(!_water.activeInHierarchy);
            
            bool isOpen = _water.activeInHierarchy;

            if (isOpen)
            {
                _player.Stun(_washTime);
                Invoke(nameof(CompleteTask), _washTime);
            }
            
            _handleTransform.localRotation =  isOpen ? _handleOpenRotation : _handleClosedRotation;
        }
        
        private void CompleteTask() => TaskSystem.TryCompleteTask(DayTask.WashHands);
    }
}