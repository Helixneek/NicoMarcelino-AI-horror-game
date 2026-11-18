using AgateAIHorror.Interactable;
using System.Collections.Generic;
using UnityEngine;

namespace AgateAIHorror.Player
{
    public class InventoryManager : MonoBehaviour
    {
        private List<ItemData> _items = new();
        public List<ItemData> Items => _items;

        public void AddItem(ItemData item)
        {
            _items.Add(item);
        }

        public bool CheckItem(string id)
        {
            bool doesItemExist = Items.Exists(itemData => string.Equals(itemData.ID, id));
            return doesItemExist;
        }

        public void RemoveItem(ItemData item)
        {
            Items.Remove(item);
        }
    }
}
