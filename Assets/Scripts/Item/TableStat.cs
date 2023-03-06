using System;
using UnityEngine;

[System.Serializable]
public class TableStat : Stat
{
    public Capacity capacity;
    public Type script = typeof(Table);
}
