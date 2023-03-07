using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObject/HumanTex/Skin", order = 0)]
public class Skin : ScriptableObject
{
    public string texName = "";
    public Texture texture;
}