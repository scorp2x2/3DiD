using Assets.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public Transform contextMenu;
    public Image sprite;
    public Text textCount;
    public Image Border;

    public GameObject buttonRemove;
    public GameObject buttonEqup;
    public GameObject buttonUse;

    InventoryItem InventoryItem;

    public float InitialTouch;
    public bool isChest;
    public bool isEquip;

    public void Initialise(InventoryItem inventoryItem, bool isChest = false)
    {
        if (inventoryItem.inventoryItemData == null) { Clear(); return; }

        Border.enabled = false;
        sprite.enabled = true;
        sprite.sprite = inventoryItem.Sprite;
        this.isChest = isChest;

        InventoryItem = inventoryItem;
        buttonEqup.SetActive(false);
        buttonUse.SetActive(false);
        textCount.text = "";

        if (inventoryItem.inventoryItemData is Armor || inventoryItem.inventoryItemData is Weapon)
            buttonEqup.SetActive(true);
        if (inventoryItem.inventoryItemData is Consumables)
            buttonUse.SetActive(true);
        if (inventoryItem.inventoryItemData is StackItem)
            textCount.text = inventoryItem.count.ToString();
    }

    public void Clear()
    {
        sprite.enabled = false;
        InventoryItem = null;
        textCount.text = "";
        contextMenu.gameObject.SetActive(false);
        Border.enabled = false;

    }
    private void OnMouseUpAsButton()
    {

        if (InventoryItem == null) return;

        if (isChest || isEquip)
        {
            if (Time.time < InitialTouch + 0.5f)
            {
                if (isChest) FindObjectOfType<InventoryController>().AddItemChest(InventoryItem);
                if (isEquip) ButtonTakeOff();
                Clear();
            }
            InitialTouch = Time.time;
            return;
        }

        if (!isChest && !isEquip)
            contextMenu.gameObject.SetActive(true);
    }

    public void ButtonRemove()
    {
        FindObjectOfType<InventoryController>().Remove(InventoryItem);
    }

    public void ButtonUse()
    {
        FindObjectOfType<InventoryController>().Use(InventoryItem);
    }

    public void ButtonEqup()
    {
        FindObjectOfType<InventoryController>().Equip(InventoryItem);
        contextMenu.gameObject.SetActive(false);
    }

    public void ButtonTakeOff()
    {
        FindObjectOfType<InventoryController>().TakeOff(InventoryItem);
        Clear();
    }

    public void OnMouseExit()
    {
        contextMenu.gameObject.SetActive(false);
        FindObjectOfType<InventoryController>().EndCheckState();
        Border.enabled = false;
    }

    public void OnMouseEnter()
    {
        if (InventoryItem == null) return;

        Debug.Log("MouseEnter " + InventoryItem.Sprite.name);
        Border.enabled = true;
        if (InventoryItem.inventoryItemData is Armor || InventoryItem.inventoryItemData is Weapon)
            FindObjectOfType<InventoryController>().CheckState(InventoryItem);
    }

}
