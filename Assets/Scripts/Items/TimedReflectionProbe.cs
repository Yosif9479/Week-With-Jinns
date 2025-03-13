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

        private void Awake()
        {
            _reflectionProbe = GetComponent<ReflectionProbe>();
        }

        private void Start()
        {
            _reflectionProbe.RenderProbe();
            
            InvokeRepeating(nameof(TryRender), 0f,  60.5f);
        }

        private void TryRender()
        {
            if (_hours.Contains(DayTime.CurrentTime.Hours)) _reflectionProbe.RenderProbe();
        }
    }
}