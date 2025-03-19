using Enums;
using PlayerScripts;
using TaskScripts;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class ShowerWater : MonoBehaviour
    {
        [SerializeField] private float _showerTime = 10f;
        
        private bool _playerIsShowering;
        private float _showeredSeconds;

        private void Start()
        {
            Player.ClosedEyes += () => _showeredSeconds = 0;
        }

        private void Update()
        {
            float delta = Time.deltaTime * (_playerIsShowering ? 1f : -1f);

            _showeredSeconds += delta;
            
            _showeredSeconds = Mathf.Clamp(_showeredSeconds, 0, _showerTime);

            if (_showeredSeconds >= _showerTime)
            {
                TaskSystem.TryCompleteTask(DayTask.Shower);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _playerIsShowering = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _playerIsShowering = false;
        }

        private void OnDisable() => _showeredSeconds = 0f;
    }
}