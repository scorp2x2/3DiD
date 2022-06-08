using Assets.Scripts;
using Assets.Scripts.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject Chest;
    public GameObject prefabItem;

    public ItemController weapon;
    public Text namePerson;
    public Text state;
    public Image iconPerson;
    public ItemController helmet;
    public ItemController Chestpiece;
    public ItemController Trousers;
    public ItemController Shoes;

    public Transform inventoryItems;
    public Transform chestItems;

    public PersonageSave Personage;

    ChestController ChestController;

    // Start is called before the first frame update
    void Start()
    {
        Personage = FindObjectOfType<GameController>().Personage;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Chest.SetActive(false);

        LoadInventory();
    }

    public void PrintState()
    {
        state.text = string.Format("Скорость атаки: {0}\nУрон: {1}\nБроня: {2}\nHp: {3}/{5}\nMp: {4}/{6}",
            Math.Round(Personage.State.DamageSpeed, 2),
            Personage.State.Damage,
            Personage.State.Armor,
            Personage.Hp,
            Personage.Mp,
            Personage.State.MaxHp,
            Personage.State.MaxMp);

    }

    public void LoadInventory()
    {
        if (Personage == null)
            Personage = FindObjectOfType<GameController>().Personage;

        Personage.UpdateState();

        namePerson.text = Personage.Personage.Name;
        PrintState();

        weapon.Initialise(new InventoryItem(Personage.Personage.Inventory.Weapon, 1));
        weapon.sprite.sprite = Personage.Personage.Inventory.Weapon?.MaxIcon;

        helmet.Initialise(new InventoryItem(Personage.Personage.Inventory.Helmet, 1));// sprite = Inventory.Helmet == null ? null : Inventory.Helmet.Sprite;
        Chestpiece.Initialise(new InventoryItem(Personage.Personage.Inventory.Chestpiece, 1));//.sprite = Inventory.Chestpiece == null ? null : Inventory.Chestpiece.Sprite;
        Shoes.Initialise(new InventoryItem(Personage.Personage.Inventory.Shoes, 1));//.sprite = Inventory.Shoes == null ? null : Inventory.Shoes.Sprite;
        Trousers.Initialise(new InventoryItem(Personage.Personage.Inventory.Trousers, 1));//.sprite = Inventory.Trousers == null ? null : Inventory.Trousers.Sprite;

        for (int i = 0; i < inventoryItems.childCount; i++)
        {
            if (i < Personage.Personage.Inventory.InventoryItems.Count)
                inventoryItems.GetChild(i).GetComponent<ItemController>().Initialise(Personage.Personage.Inventory.InventoryItems[i]);
            else
                inventoryItems.GetChild(i).GetComponent<ItemController>().Clear();
        }
    }

    public void OpenChest(ChestController chestController)
    {
        if (Personage == null)
            Personage = FindObjectOfType<GameController>().Personage;

        gameObject.SetActive(true);
        Chest.SetActive(true);

        ChestController = chestController;
        LoadInventory();
        for (int i = 0; i < chestItems.childCount; i++)
        {
            if (i < chestController.inventoryItems.Count)
                chestItems.GetChild(i).GetComponent<ItemController>().Initialise(chestController.inventoryItems[i], true);
            else
                chestItems.GetChild(i).GetComponent<ItemController>().Clear();
        }
    }

    public void EndCheckState()
    {
        PrintState();
    }

    public void TakeOff(InventoryItem inventoryItem)
    {
        Personage.Personage.Inventory.AddItem(inventoryItem);
        Personage.Personage.Inventory.TakeOff(inventoryItem);
        Personage.UpdateState();
        LoadInventory();
    }

    public void CheckState(InventoryItem inventoryItem)
    {
        State newState = Personage.CheckState(inventoryItem);

        state.text = string.Format("Скорость атаки: {0}\nУрон: {1}\nБроня: {2}\nHp: {3}/{5}\nMp: {4}/{6}",
            colorText(Personage.State.DamageSpeed, newState.DamageSpeed),
            colorText(Personage.State.Damage, newState.Damage),
            colorText(Personage.State.Armor, newState.Armor),
            Personage.Hp,
            Personage.Mp,
            colorText(Personage.State.MaxHp, newState.MaxHp),
            colorText(Personage.State.MaxMp, newState.MaxMp));
    }

    public string colorText(float startValue, float newValue)
    {
        string returnText = Math.Round(startValue, 2).ToString();

        if (startValue < newValue)
            returnText += "<Color=Green> +" + Math.Round(newValue - startValue, 2) + "</Color>";
        else if (startValue > newValue)
            returnText += "<Color=Red> -" + Math.Round(startValue - newValue, 2) + "</Color>";

        return returnText;
    }

    public void Equip(InventoryItem inventoryItem)
    {
        Personage.Personage.Inventory.Equip(inventoryItem);
        Personage.UpdateState();
        LoadInventory();
    }

    public void Use(InventoryItem inventoryItem)
    {
        Personage.Personage.Inventory.Use(inventoryItem);
        LoadInventory();
    }

    public void Remove(InventoryItem inventoryItem)
    {
        Personage.Personage.Inventory.Remove(inventoryItem);
        LoadInventory();
    }

    public void AddItemChest(InventoryItem inventoryItem)
    {
        ChestController.inventoryItems.Remove(inventoryItem);
        AddItem(inventoryItem);
    }
    public void AddItem(InventoryItem inventoryItem)
    {
        Personage.Personage.Inventory.AddItem(inventoryItem);
        LoadInventory();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
