using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Item
{
    protected internal override void Initialize()
    {
        controlGridItem = new Vector2[0] {};
        controlItem = new Item[4] {null, null, null, null};
    }

    protected internal int ReturnFreeIndex()
    {
        for(int i = 0; i < controlItem.Length; i++)
        {
            if(controlItem[i] == null) return i;
        }

        return -1;
    }

    protected internal override void CheckControlItem()
    {
        for(int i = 0; i < controlItem.Length; i++)
        {
            if(controlItem[i] == null) continue;
            if(controlItem[i].GetComponent<Chair>().controlItem[0] != GetComponent<Item>()) controlItem[i] = null;
        }
    }
}
