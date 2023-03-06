using UnityEngine;

[CreateAssetMenu(fileName = "Mouth", menuName = "Restaurant Manager/Texture/Mouth", order = 0)]
public class Mouth : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;
}