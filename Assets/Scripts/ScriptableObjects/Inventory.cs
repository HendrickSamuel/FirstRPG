using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;

    public void AddItem(Item newItem)
    {
        if(newItem.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if(!items.Contains(newItem))
            {
                items.Add(newItem);
            }
        }
    }
}
