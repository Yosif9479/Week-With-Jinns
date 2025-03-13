using Interfaces;
using UnityEngine;

namespace Items
{
    public class Sink : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _water;
        [SerializeField] private Transform _handleTransform;

        private Quaternion _handleClosedRotation;
        [SerializeField] private Quaternion _handleOpenRotation;

        private void Start()
        {
            _handleClosedRotation = _handleTransform.localRotation;
        }
        
        public void Interact()
        {
            _water.SetActive(!_water.activeInHierarchy);
            
            bool isOpen = _water.activeInHierarchy;
            
            _handleTransform.localRotation =  isOpen ? _handleOpenRotation : _handleClosedRotation;
        }
    }
}