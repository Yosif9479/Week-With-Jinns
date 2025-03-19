using Constants;
using Enums;
using Ui;
using UnityEngine;

namespace TaskScripts
{
    public class TaskMessager : MonoBehaviour
    {
        [SerializeField] private RectTransform _parentCanvas;
        [SerializeField] private MessageText _messagePrefab;
        
        private void OnEnable()
        {
            TaskSystem.CompletedTask += OnTaskCompleted;
        }

        private void OnDisable()
        {
            TaskSystem.CompletedTask -= OnTaskCompleted;
        }
        
        private void OnTaskCompleted(DayTask task)
        {
            MessageText message = Instantiate(_messagePrefab, _parentCanvas);
            
            message.Init($"Task completed: {DayTaskNames.TaskToName[task]}");
        }
    }
}