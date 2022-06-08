using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class StackItem : InventoryItemData
    {
        public uint maxCount;
    }
}
