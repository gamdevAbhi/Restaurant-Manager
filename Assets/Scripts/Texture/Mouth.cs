using UnityEngine;

[CreateAssetMenu(fileName = "Mouth", menuName = "ScriptableObject/HumanTex/Mouth", order = 0)]
public class Mouth : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;
}