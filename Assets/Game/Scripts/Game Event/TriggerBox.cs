using UnityEngine;
using UnityEngine.Events;

namespace AgateAIHorror.Core
{
    public class TriggerBox : MonoBehaviour
    {
        [SerializeField] private bool _autoActive;
        [Tooltip("Tag of object that's gonna be the trigger")]
        [SerializeField] private string _tag;
        [SerializeField] private bool _isOneTime;

        public UnityEvent OnTrigger;
        private bool _isActive;

        private void Awake()
        {
            _isActive = _autoActive;
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_tag) && _isActive)
            {
                OnTrigger?.Invoke();
                if(_isOneTime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
