using System;
using UnityEngine;
namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class InventoryItemData : ScriptableObject
    {
        public Sprite Sprite;
    }

    [Serializable]
    public class InventoryItem
    {
        
        public InventoryItemData inventoryItemData;
        public int count;


        public InventoryItem(InventoryItemData inventoryItemData)
        {
            this.inventoryItemData = inventoryItemData;
        }

        public InventoryItem(InventoryItemData inventoryItemData, int count)
        {
            this.count = count;
            this.inventoryItemData = inventoryItemData;
        }

        public Sprite Sprite { get { return inventoryItemData.Sprite; } }
    }
}
