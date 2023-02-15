using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Item
{
    [SerializeField] protected internal Table table;
    [SerializeField] protected internal Customer[] customer;

    protected internal override void Update()
    {
        base.Update();

        CheckControlItem();
    }

    protected internal override void Initialize()
    {
        ChairStat stat = item.stat as ChairStat;

        table = null;

        customer = new Customer[stat.GetCapacitySize(stat.capacity)];

        CheckControlItem();
    }

    protected internal void CheckControlItem()
    {
        table = null;
        Table currentTable = null;

        foreach(GridData _grid in occupiedGrid)
        {
            if(_grid == null) return;

            GameObject obj = new GameObject();
            obj.transform.parent = transform;
            obj.transform.position = _grid.transform.position;

            obj.transform.localPosition += new Vector3(gridManager._worldToGrid.x * 1f, 0f, 0f);

            GridData grid = gridManager.GetClosestGridFromWorld(obj.transform.position);
            Destroy(obj);

            if(grid == null) return;
            if(grid._occupiedObject == null) return;
            if(grid._occupiedObject.GetComponents<Item>().Length == 0) return;

            if(grid._occupiedObject.GetComponent<Item>().itemType == ItemObject.ItemType.Table)
            {
                if(currentTable != null && currentTable != grid._occupiedObject.GetComponent<Table>()) return;

                currentTable = grid._occupiedObject.GetComponent<Table>();
            }
            else return;
        }

        foreach(Chair chair in currentTable.chair)
        {
            if(chair == this)
            {
                table = currentTable;
                return;
            }
        }
        
        int index = currentTable.ReturnFreeIndex();

        if(index > -1)
        {
            currentTable.chair[index] = this;
            table = currentTable;
        }
    }

    protected internal bool ChairIsFree()
    {
        foreach(Customer cus in customer)
        {
            if(cus == null) return true;
        }

        return false;
    }

    protected internal int ChairFreeIndex()
    {
        if(ChairIsFree() == false) return -1;
        else
        {
            for(int i = 0; i < customer.Length; i++)
            {
                if(customer[i] == null) return i;
            }

            return -1;
        }
    }
}
