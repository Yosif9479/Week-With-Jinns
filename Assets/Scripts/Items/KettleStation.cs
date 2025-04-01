using Interfaces;
using UnityEngine;

namespace Items
{
    public class KettleStation : MonoBehaviour,ICanBeUsedOn
    {
        [SerializeField] private Transform _kettleStationTransform;
        
        private void TeleportKettle(GameObject usedOn)
        {
            ElectricKettle electricKettle = usedOn.GetComponent<ElectricKettle>();
            
            if (electricKettle == null) return;
            
            electricKettle.transform.SetParent(_kettleStationTransform);
            electricKettle.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        public void UseWith(GameObject item = null)
        {
            if (item == null) return;
            
            TeleportKettle(item);
        }
        
    }
}