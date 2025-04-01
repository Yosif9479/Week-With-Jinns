
using Interfaces;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class Cup :  DefaultPickableItem, ICanBeUsedOn, IUsable
    {
        
        private AudioSource _audioSource;
        
        [SerializeField] private GameObject _water;
        
        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            _water.SetActive(false);
        }

        public void UseWith(GameObject item)
        {
            if  (item == null) return;
            
            ElectricKettle kettle = item.GetComponent<ElectricKettle>();
            
            if (kettle == null) return;
            
            if (!kettle.IsBoiled)  return;

            _water.SetActive(true);

        }

        public void Use(GameObject usedOn = null)
        {
            if (_water.activeInHierarchy)
            {
                _audioSource.Play();
                _water.SetActive(false);
            }
        }
    }
}