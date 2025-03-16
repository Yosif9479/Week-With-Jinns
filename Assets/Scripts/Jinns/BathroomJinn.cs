using System.Collections.Generic;
using UnityEngine;

namespace Jinns
{
    public class BathroomJinn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objectsToActivate;
        private void Start()
        {
            foreach (GameObject @object in _objectsToActivate)
            {
                @object.SetActive(true);
            }
        }
    }
}