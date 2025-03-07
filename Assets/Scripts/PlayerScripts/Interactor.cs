using System;
using System.Linq;
using Models;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class Interactor : IInteractor
    {
        private readonly PlayerInput.PlayerActions _playerInput;
        private InteractionSetting _settings;
        private Transform _itemHolder;
        private Camera _camera;
        
        public event UnityAction Interacted;
        public event UnityAction ItemUsed;
        public event UnityAction ItemDropped;

        public Interactor(PlayerInput input)
        {
            _playerInput = input.Player;
            
            _playerInput.Enable();
        }
        
        public void Initialize(InteractionSetting settings, Transform itemHolder)
        {
            _settings = settings;
            _itemHolder = itemHolder;

            _playerInput.Interact.started += Interact;
            _playerInput.Use.started += UseItem;
            _playerInput.Drop.started += DropItem;

            _camera = Camera.main;
            
            IsValid();
        }

        private void Interact(InputAction.CallbackContext _)
        {
            if (!IsValid()) return;

            Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            
            bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, _settings.MaxDistance);

            if (!hit) return;

            IPickable pickable = hitInfo.collider.GetComponent<IPickable>();

            if (pickable != null)
            {
                DropItem(_);
                hitInfo.collider.gameObject.transform.SetParent(_itemHolder);
                pickable.OnPickedUp();
                Interacted?.Invoke();
            }
            
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
                Interacted?.Invoke();
            }
        }

        private void UseItem(InputAction.CallbackContext _)
        {
            IUsable usable = _itemHolder.GetComponentsInChildren<IUsable>().FirstOrDefault();
            
            if (usable == null) return;
            
            usable.Use();
            
            ItemUsed?.Invoke();
        }

        private void DropItem(InputAction.CallbackContext _)
        {
            Transform[] heldItems = _itemHolder.GetComponentsInChildren<Transform>();
            
            if (heldItems == null) return;

            foreach (Transform item in heldItems)
            {
                IPickable pickable = item.GetComponent<IPickable>();

                if (pickable == null) continue;
                
                item.SetParent(null);
                
                pickable.OnDropped();

                ItemDropped?.Invoke();

                break;
            }
        }

        private bool IsValid()
        {
            try
            {
                if (_settings == null) throw new NullReferenceException("Interactor settings is null");
                if (_itemHolder == null) throw new NullReferenceException("ItemHolder is null");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }

            return true;
        }
    }
}