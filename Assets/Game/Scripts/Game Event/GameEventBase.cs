using UnityEngine;
using UnityEngine.Events;

namespace AgateAIHorror.Core
{
    public class GameEventBase : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isOneTime;

        public UnityEvent OnEventTriggered, OnEventFinished;

        public string ID => _id;

        private void Start()
        {
            GameEventManager.Instance.Register(this);
        }

        public virtual void Trigger()
        {
            OnEventTriggered?.Invoke();
        }

        public virtual void Finish()
        {
            OnEventFinished?.Invoke();

            if(_isOneTime)
            {
                GameEventManager.Instance.Unregister(this);
                Destroy(gameObject);
            }
        }
    }
}
