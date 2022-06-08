using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class Weapon : InventoryItemData
    {
        public Sprite MaxIcon;

        public int Damage;
        public float DamageSpeed;
    }
}
