using TMPro;
using UnityEngine;

namespace Ui
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MessageText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        
        public void Init(string message)
        {
            _text = GetComponent<TextMeshProUGUI>();
            
            _text.text = message;
        }
        
        private void SelfDestroy() => Destroy(gameObject);
    }
}