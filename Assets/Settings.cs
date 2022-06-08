using Assets.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public List<InventoryItemData> inventoryItemDatas;
    // Start is called before the first frame update
    void Awake()
    {
        inventoryItemDatas= Resources.LoadAll<InventoryItemData>("Items").Select(a => a as InventoryItemData).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
