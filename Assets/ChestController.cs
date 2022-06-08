using Assets.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ChestController : MonoBehaviour
{
    bool isEnter;
    PlayerController PlayerController;

    public bool isOpen;
    public List<InventoryItem> inventoryItems;
    Settings Settings;
    public GameObject textInfo;

    private void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();

        Settings = FindObjectOfType<Settings>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = true;
            textInfo.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = false;
            textInfo.SetActive(false);

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEnter)
        {
            if (!isOpen)
                GenerateItems();

            PlayerController.InventoryController.OpenChest(this);
            PlayerController.openInventory = true;
            textInfo.SetActive(false);

        }
    }

    void GenerateItems()
    {
        isOpen = true;
        int count = Random.Range(0, 9) / 2;
        inventoryItems = new List<InventoryItem>();


        for (int i = 0; i < count; i++)
        {
            InventoryItem item = new InventoryItem(Settings.inventoryItemDatas.GetRandom());
            item.count = 1;
            if (item.inventoryItemData is StackItem)
            {
                var stackCount = (item.inventoryItemData as StackItem).maxCount;
                item.count = Random.Range(1, (int)stackCount);
            }

            inventoryItems.Add(item);
        }
    }
}
