using UnityEngine;

[CreateAssetMenu(fileName = "Cloths", menuName = "Restaurant Manager/Texture/Cloth", order = 0)]
public class Cloths : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;
    public enum Gender {Male, Female, Both}

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