using System;
using System.Collections.Generic;
using Enums;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using Random = UnityEngine.Random;

namespace TaskScripts
{
    public static class TaskSystem
    {
        public static event UnityAction<DayTask> CompletedTask; 
        
        private const uint TaskAmount = 5;
        public static List<DayTask> DayTasks { get; } = new();
        public static List<DayTask> FailedTasks { get; } = new();

        public static void Start()
        {
            Player.Slept += OnPlayerSlept;
            Player.ClosedEyes += GenerateTasks;
            
            FailedTasks.Clear();
            GenerateTasks();
        }

        private static void OnPlayerSlept()
        {
            FailedTasks.Clear();
            
            FailedTasks.AddRange(DayTasks);
        }

        private static void GenerateTasks()
        {
            DayTasks.Clear();
            
            Array tasks = Enum.GetValues(typeof(DayTask));
            
            long amount = Math.Clamp(TaskAmount, 0, tasks.Length);

            for (var i = 0; i < amount; i++)
            {
                while (true)
                {
                    var task = (DayTask) tasks.GetValue(Random.Range(0, tasks.Length));

                    if (DayTasks.Contains(task)) continue;
                    
                    DayTasks.Add(task);

                    break;
                }
            }
        }

        public static void TryCompleteTask(DayTask task)
        {
            if (!DayTasks.Contains(task)) return;

            DayTasks.Remove(task);
            
            CompletedTask?.Invoke(task);
        }
    }
}