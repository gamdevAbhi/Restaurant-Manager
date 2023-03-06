using System;
using UnityEngine;

[System.Serializable]
public class ChairStat : Stat
{
    public Capacity capacity;
    public Type script = typeof(Chair);
}
