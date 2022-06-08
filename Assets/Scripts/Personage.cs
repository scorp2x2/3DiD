using UnityEngine;
using Assets.Scripts.Inventory;
using System;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class Personage : ScriptableObject
    {
        public Inventory.Inventory Inventory;
        public GameObject Prefab;


        public State BaseState;


        public GameObject Shell;
        public string Name;
      
    }

    public class PersonageSave
    {
        public Personage Personage;
        public State State;

        public int Mp;
        public int Hp;
        public bool isDeath;

        public PersonageSave(Personage personage)
        {
            this.Personage = personage;
            State = personage.BaseState;
            Mp = Personage.BaseState.MaxMp;
            Hp = Personage.BaseState.MaxHp;
        }

        public ShellState ShellState
        {
            get
            {
                return new ShellState()
                {
                    Damage = State.Damage,
                    Time = 3,
                    Speed = 2,
                    Prefab = Personage.Shell
                };
            }
        }

        public State UpdateState()
        {

            State state = null;
            if (Personage.Inventory == null)
            {
                //state = BaseState;
                State = Personage.BaseState;
                return Personage.BaseState;
            }
            else
                state = UpdateState(Personage.Inventory?.Helmet, Personage.Inventory?.Chestpiece, Personage.Inventory?.Shoes, Personage.Inventory?.Trousers, Personage.Inventory?.Weapon);


            Hp = (Hp * state.MaxHp) / State.MaxHp;
            Mp = (Mp * state.MaxMp) / State.MaxMp;

            State = state;

            return State;
        }

        public State UpdateState(Helmet Helmet, Chestpiece Chestpiece, Shoes Shoes, Trousers Trousers, Weapon Weapon)
        {
            State state = new State();

            state.MaxHp = Personage.BaseState.MaxHp + (Helmet == null ? 0 : Helmet.HpCount)
                                    + (Chestpiece == null ? 0 : Chestpiece.HpCount)
                                    + (Shoes == null ? 0 : Shoes.HpCount)
                                    + (Trousers == null ? 0 : Trousers.HpCount);
            state.MaxMp = Personage.BaseState.MaxHp + (Helmet == null ? 0 : Helmet.MpCount)
                                    + (Chestpiece == null ? 0 : Chestpiece.MpCount)
                                    + (Shoes == null ? 0 : Shoes.MpCount)
                                    + (Trousers == null ? 0 : Trousers.MpCount);
            state.Damage = Personage.BaseState.Damage + (Helmet == null ? 0 : Helmet.Damage)
                                    + (Chestpiece == null ? 0 : Chestpiece.Damage)
                                    + (Shoes == null ? 0 : Shoes.Damage)
                                    + (Trousers == null ? 0 : Trousers.Damage)
                                    + (Weapon == null ? 0 : Weapon.Damage);

            float damagespeed = (Helmet == null ? 0 : Helmet.DamageSpeed)
                                    + (Chestpiece == null ? 0 : Chestpiece.DamageSpeed)
                                    + (Shoes == null ? 0 : Shoes.DamageSpeed)
                                    + (Trousers == null ? 0 : Trousers.DamageSpeed)
                                    + (Weapon == null ? 0 : Weapon.DamageSpeed);

            state.DamageSpeed = Personage.BaseState.DamageSpeed / ((100 + damagespeed) * 0.01f);
            state.Armor = Personage.BaseState.Armor + (Helmet == null ? 0 : Helmet.ArmorCount)
                                    + (Chestpiece == null ? 0 : Chestpiece.ArmorCount)
                                    + (Shoes == null ? 0 : Shoes.ArmorCount)
                                    + (Trousers == null ? 0 : Trousers.ArmorCount);

            return state;
        }

        public bool Damage(int damage)
        {
            var resist = (0.052f * (float)State.Armor) / (0.9f + 0.048f * (float)State.Armor);
            var dam = resist * damage;
            Debug.Log(Personage.Name + " Получил " + dam);
            Hp -= (int)dam;

            if (Hp <= 0)
            {
                Hp = 0;
                Death();
                return false;
            }

            return true;
        }

        public void Death()
        {
            Debug.Log(Personage.Name + " death");
            isDeath = true;
            //FindObjectOfType<AnimationController>().Death();
        }

        public State CheckState(InventoryItem inventoryItem)
        {
            var Inventory = Personage.Inventory;
            if (inventoryItem.inventoryItemData is Helmet)
                return UpdateState(inventoryItem.inventoryItemData as Helmet, Personage.Inventory.Chestpiece, Personage.Inventory.Shoes, Personage.Inventory.Trousers, Personage.Inventory.Weapon);
            else if (inventoryItem.inventoryItemData is Shoes)
                return UpdateState(Inventory.Helmet, Inventory.Chestpiece, inventoryItem.inventoryItemData as Shoes, Inventory.Trousers, Inventory.Weapon);
            else if (inventoryItem.inventoryItemData is Trousers)
                return UpdateState(Inventory.Helmet, Inventory.Chestpiece, Inventory.Shoes, inventoryItem.inventoryItemData as Trousers, Inventory.Weapon);
            else if (inventoryItem.inventoryItemData is Chestpiece)
                return UpdateState(Inventory.Helmet, inventoryItem.inventoryItemData as Chestpiece, Inventory.Shoes, Inventory.Trousers, Inventory.Weapon);
            else if (inventoryItem.inventoryItemData is Weapon)
                return UpdateState(Inventory.Helmet, Inventory.Chestpiece, Inventory.Shoes, Inventory.Trousers, inventoryItem.inventoryItemData as Inventory.Weapon);
            else
                new Exception("Неверный тип брони");

            return null;
        }
    }
}
