using UnityEngine;

public class Table : Item
{
    [SerializeField] protected internal Chair[] chair;

    protected internal override void Update()
    {
        base.Update();

        CheckControlItem();
    }

    protected internal override void Initialize()
    {
        TableStat stat = item.stat as TableStat;

        chair = new Chair[stat.GetCapacitySize(stat.capacity)];

        CheckControlItem();
    }

    protected internal int ReturnFreeIndex()
    {
        for(int i = 0; i < chair.Length; i++)
        {
            if(chair[i] == null) return i;
        }

        return -1;
    }

    protected internal void CheckControlItem()
    {
        for(int i = 0; i < chair.Length; i++)
        {
            if(chair[i] == null) continue;
            if(chair[i].GetComponent<Chair>().table != this) chair[i] = null;
        }
    }
}
