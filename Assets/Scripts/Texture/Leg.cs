using UnityEngine;

[CreateAssetMenu(fileName = "Leg", menuName = "ScriptableObject/HumanTex/Leg", order = 0)]
public class Leg : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;

    public LegPattern[] legPattern;
}

[System.Serializable]
public class LegPattern
{
    public Texture texture;
    public Color[] color;
}