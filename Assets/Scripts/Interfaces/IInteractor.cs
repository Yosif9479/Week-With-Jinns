using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IInteractor
    {
        public event UnityAction<GameObject> Interacted;
        public event UnityAction<GameObject> ItemUsed;
        public event UnityAction<GameObject> ItemDropped;

        public void Initialize(InteractionSetting settings, Transform itemHolder);
    }
}