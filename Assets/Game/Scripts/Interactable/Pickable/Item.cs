using System;
using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class Item : MonoBehaviour, IInteractable, IPickable
    {
        [SerializeField] private string _itemName;

        public String Name => _itemName;

        public void Interact()
        {
            Pickup();
        }

        public void Pickup()
        {
            throw new NotImplementedException();
        }
    }
}
