using Interfaces;
using UnityEngine;

namespace Basic
{
    public abstract class DefaultInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private AnimatorOverrideController _animatorOverride;
        
        public AnimatorOverrideController AnimatorOverride() => _animatorOverride;

        public abstract void Interact();
    }
}