using System.Collections.Generic;
using System.Linq;
using Enums;
using PlayerScripts;
using UnityEngine;

namespace TaskScripts
{
    public class JinnSpawner : MonoBehaviour
    {
        [Header("Jinns")]
        [SerializeField] private GameObject _bathroomJinn;
        [SerializeField] private GameObject _kitchenJinn;
        [SerializeField] private GameObject _nightJinn;

        [Header("Tasks")]
        [SerializeField] private DayTask[] _bathroomTasks;
        [SerializeField] private DayTask[] _kitchenTasks;
        [SerializeField] private DayTask[] _nightTasks;

        private const int NightJinnSpawnHour = 21;
        private List<DayTask> _failedTasks;
        
        private void Start()
        {
            Player.ClosedEyes += HandleJinns;
        }

        private void HandleJinns()
        {
            _failedTasks = TaskSystem.FailedTasks;
            
            HandleBathroomJinn();
            HandleKitchenJinn();
            HandleNightJinn();
        }

        private void HandleBathroomJinn()
        {
            if (!_failedTasks.Any(x => _bathroomTasks.Contains(x))) return;
            
            _bathroomJinn.SetActive(true);
        }

        private void HandleKitchenJinn()
        {
            if (!_failedTasks.Any(x => _kitchenTasks.Contains(x))) return;
            
            _kitchenJinn.SetActive(true);
        }

        private void HandleNightJinn()
        {
            if (!_failedTasks.Any(x => _nightTasks.Contains(x))) return;
            
            _nightJinn.SetActive(true);
        }
    }
}