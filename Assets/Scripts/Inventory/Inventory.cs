using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<InventoryItem> InventoryItems;

        public Helmet Helmet;
        public Chestpiece Chestpiece;
        public Trousers Trousers;
        public Shoes Shoes;

        public Weapon Weapon;

        public void AddItem(InventoryItem inventoryItem)
        {

            if (inventoryItem.inventoryItemData is StackItem)

                foreach (var item in InventoryItems)
                {
                    if (item.inventoryItemData == inventoryItem.inventoryItemData)
                    {
                        uint maxCount = (inventoryItem.inventoryItemData as StackItem).maxCount;
                        int sub = (int)maxCount - (int)item.count;
                        if (sub < inventoryItem.count)
                        {
                            item.count = (int)maxCount;
                            inventoryItem.count -= sub;
                        }
                        else
                        {
                            item.count += inventoryItem.count;
                            return;
                        }
                    }
                }

            InventoryItems.Add(inventoryItem);
        }

        public void Remove(InventoryItem inventoryItem)
        {
            InventoryItems.Remove(inventoryItem);
        }

        public void Use(InventoryItem inventoryItem)
        {
            inventoryItem.count--;
            if (inventoryItem.count <= 0)
                Remove(inventoryItem);
        }

        public void Equip(InventoryItem inventoryItem)
        {
            if (inventoryItem.inventoryItemData is Helmet)
            {
                if(Helmet!=null) InventoryItems.Add(new InventoryItem(Helmet, 1));
                Helmet = inventoryItem.inventoryItemData as Helmet;
            }
            else if (inventoryItem.inventoryItemData is Shoes)
            {
                if (Shoes != null) InventoryItems.Add(new InventoryItem(Shoes,1));
                Shoes = inventoryItem.inventoryItemData as Shoes;
            }
            else if (inventoryItem.inventoryItemData is Trousers)
            {
                if (Trousers != null) InventoryItems.Add(new InventoryItem(Trousers, 1));
                Trousers = inventoryItem.inventoryItemData as Trousers;
            }
            else if (inventoryItem.inventoryItemData is Chestpiece)
            {
                if (Chestpiece != null) InventoryItems.Add(new InventoryItem(Chestpiece, 1));
                Chestpiece = inventoryItem.inventoryItemData as Chestpiece;
            }
            else if (inventoryItem.inventoryItemData is Weapon)
            {
                if (Weapon != null) InventoryItems.Add(new InventoryItem(Weapon, 1));
                Weapon = inventoryItem.inventoryItemData as Weapon;
            }
            else
                new Exception("Неверный тип брони");

            InventoryItems.Remove(inventoryItem);
        }

        public void TakeOff(InventoryItem inventoryItem)
        {
            if (inventoryItem.inventoryItemData is Helmet)
                Helmet = null; 
            else if (inventoryItem.inventoryItemData is Shoes)
                Shoes = null;
            else if (inventoryItem.inventoryItemData is Trousers)
                Trousers = null;
            else if (inventoryItem.inventoryItemData is Chestpiece)
                Chestpiece = null;
            else if (inventoryItem.inventoryItemData is Weapon)
                Weapon = null;
            else
                new Exception("Неверный тип брони");
        }
    }
}
