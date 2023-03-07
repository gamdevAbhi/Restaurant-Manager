using UnityEngine;

[CreateAssetMenu(fileName = "Shoe", menuName = "ScriptableObject/HumanTex/Shoe", order = 0)]
public class Shoe : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;
}