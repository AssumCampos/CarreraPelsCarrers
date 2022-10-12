using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Inventario
 */
namespace Inventario
{
    public class Inventory : Singleton<Inventory>
    {
        public InventoryCell prefab;

        public void AddItem(Item item)
        {
            InventoryCell[] list = GetComponentsInChildren<InventoryCell>();
            foreach (var i in list)
            {
                if (i.description.id == item.description.id)
                {
                    i.amount += item.description.value;
                    if(i.description.type == Type.GARBAGE)
                        i.ChangeIcon();
                    i.totalAmount += item.description.value;
                    return;
                }
            }
        }
    }
}

