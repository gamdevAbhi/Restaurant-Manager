using UnityEngine;

[CreateAssetMenu(fileName = "Ear", menuName = "ScriptableObject/HumanTex/Ear", order = 0)]
public class Ear : ScriptableObject 
{
    public string texName = "";
    public Texture texture;
}