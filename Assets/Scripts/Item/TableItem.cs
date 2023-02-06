using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItem : ItemScript
{
    protected internal override void Initialize()
    {
        controlGridItem = new Vector2[0] {};
        controlGridHuman = new Vector2[0] {};
        controlItem = new Item[4] {null, null, null, null};
        controlHuman = new Human[0] {};
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
        
    }
}
