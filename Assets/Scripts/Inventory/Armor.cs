using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class Armor : InventoryItemData
    {
        public int ArmorCount;
        public int MpCount;
        public int HpCount;
        public int Damage;
        public float DamageSpeed;
    }
}
