using UnityEngine;

[CreateAssetMenu(fileName = "Hand", menuName = "Restaurant Manager/Texture/Hand", order = 0)]
public class Hand : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
    public Color[] color;

    public Texture[] handPattern;
}