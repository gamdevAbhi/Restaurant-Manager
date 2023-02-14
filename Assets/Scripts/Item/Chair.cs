using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Item
{
    [SerializeField] protected internal Human user;

    protected internal override void Initialize()
    {
        controlGridItem = new Vector2[1] {new Vector2(gridManager._worldToGrid.x * 1f, 0f)};
        controlItem = new Item[1] {null};
    }

    protected internal override void CheckControlItem()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition += new Vector3(controlGridItem[0].x, 0f, controlGridItem[0].y);

        GridData grid = gridManager.GetClosestGridFromWorld(obj.transform.position);

        if(grid != null)
        {
            try
            {
                if(grid._occupiedObject.GetComponent<Item>().itemType == ItemObject.ItemType.table)
                {
                    controlItem[0] = grid._occupiedObject.GetComponent<Item>();
                    Table table = controlItem[0] as Table;

                    foreach(Chair chair in table.controlItem)
                    {
                        if(chair == this) return;
                    }

                    int index = grid._occupiedObject.GetComponent<Table>().ReturnFreeIndex();
                    grid._occupiedObject.GetComponent<Item>().controlItem[index] = this;
                }
            }
            catch
            {
                controlItem[0] = null;
            }
        }

        Destroy(obj);
    }
}
