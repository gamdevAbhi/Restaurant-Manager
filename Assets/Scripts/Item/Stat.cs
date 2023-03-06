using UnityEngine;

[System.Serializable]
public abstract class Stat : ScriptableObject 
{
    public enum Capacity {Zero, One, Two, Four};

    public int GetCapacitySize(Capacity capacity)
    {
        if(capacity == Stat.Capacity.One) return 1;
        else if(capacity == Stat.Capacity.Two) return 2;
        else if(capacity == Stat.Capacity.Four) return 4;
        else return 0;
    }
}
