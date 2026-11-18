using AgateAIHorror.Player;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace AgateAIHorror.Interactable
{
    public class Item : MonoBehaviour, IInteractable, IPickable
    {
        [SerializeField] private ItemData _itemData;

        public string Name => _itemData.Name;
        public UnityEvent OnItemPicked;

        [ContextMenu("Pick up Item")]
        public void Interact(PlayerCharacter player)
        {
            Pickup(player);
        }

        public void Pickup(PlayerCharacter player)
        {
            ItemData newData = new(_itemData.ID, _itemData.Name);
            player.Inventory.AddItem(newData);

            OnItemPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
