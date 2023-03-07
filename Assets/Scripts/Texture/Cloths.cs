using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "ScriptableObject/HumanTex/Cloth", order = 0)]
public class Cloths : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;
    public enum Gender {Male, Female, Both}
    public bool extendedHand = true;

    public Instance[] clothType;
    public Texture[] clothPattern;
}

[System.Serializable]
public class Instance
{
    public Texture texture;
    public Color[] color;
    public bool patternAllowed = false;
}