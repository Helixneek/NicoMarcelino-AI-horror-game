using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _name;

        public string Name => _name;

        public void Interact()
        {
            
        }
    }
}
