using AgateAIHorror.Player;
using UnityEngine;
using UnityEngine.Events;

namespace AgateAIHorror.Interactable
{
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Metadata")]
        [SerializeField] private string _doorName;
        [SerializeField] protected string _keyID;
        [SerializeField] protected Transform _doorTransform;
        [SerializeField] protected bool _isLocked = false;

        [Header("Animation")]
        [SerializeField] protected float _animationDuration = 1f;


        public string Name => _doorName;
        protected bool _isAnimating, _isOpen;
        public bool IsAnimating => _isAnimating;

        public UnityEvent OnDoorOpen, OnDoorClose;

        [ContextMenu("Interact with Door")]
        public void Interact(PlayerCharacter player)
        {
            if(_isLocked)
            {
                bool hasKey = player.Inventory.CheckItem(_keyID);
                if(hasKey)
                {
                    _isLocked = false;
                    Open();
                }
            }
            else
            {
                if (_isOpen)
                {
                    Close();

                }
                else
                {
                    Open();
                }
            }
            
        }

        public virtual void Open()
        {
            _isOpen = true;
            OnDoorOpen?.Invoke();
        }

        public virtual void Close()
        {
            _isOpen = false;
            OnDoorClose?.Invoke();
        }
    }
}
