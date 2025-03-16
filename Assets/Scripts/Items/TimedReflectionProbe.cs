using Basic;
using System.Linq;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ReflectionProbe))]
    public class TimedReflectionProbe : MonoBehaviour
    {
        [SerializeField] private int[] _hours;
        private ReflectionProbe _reflectionProbe;
        private int _lastRenderHour;
        
        private void Awake()
        {
            _reflectionProbe = GetComponent<ReflectionProbe>();
            
            _reflectionProbe.RenderProbe();
        }

        private void FixedUpdate()
        {
            TryRender();
        }

        private void TryRender()
        {
            if (_lastRenderHour == DayTime.CurrentTime.Hours) return;
            if (_hours.Contains(DayTime.CurrentTime.Hours)) _reflectionProbe.RenderProbe();
            _lastRenderHour = DayTime.CurrentTime.Hours;
        }
    }
}