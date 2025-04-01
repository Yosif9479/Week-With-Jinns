using Interfaces;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class ElectricKettle : DefaultPickableItem, IUsable
    {
        public bool IsBoiled;
        private bool _isFull;
        private bool _isBoiling;
        private AudioSource _audioSource;
        
        [SerializeField] private float _boilingTime = 5f;
        
        public override bool CanBePickedUp() => !_isBoiling;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void HandleSink(GameObject usedOn)
        {
            Sink sink = usedOn.GetComponent<Sink>();
            
            if (sink == null) return;

            if (!sink.IsOpen) return;
            
            _isFull = true;
        }
        private void HandleStation(GameObject usedOn)
        {
            KettleStation kettle = usedOn.GetComponent<KettleStation>();
            
            if (kettle == null) return;
            
            _isBoiling = true;
            _audioSource.Play();

            Invoke(nameof(Boil), _boilingTime);
        }

        private void Boil()
        {
            IsBoiled = true;
            _isBoiling = false;
        }

        private void IsFull(GameObject usedOn)
        {
            Cup cup = usedOn.GetComponent<Cup>();
            
            if (cup == null) return;
            
            _isFull = false;
        }
        


        public void Use(GameObject usedOn = null)
        {
            if (usedOn == null) return;
            
            if (!_isFull) HandleSink(usedOn);
            if (!IsBoiled && _isFull) HandleStation(usedOn);
            
            IsFull(usedOn);
            
        }
    }
}