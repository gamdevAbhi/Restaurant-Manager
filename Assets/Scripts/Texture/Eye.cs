using UnityEngine;

[CreateAssetMenu(fileName = "Eye", menuName = "ScriptableObject/HumanTex/Eye", order = 0)]
public class Eye : ScriptableObject
{
    public string texName = "";
    public Texture texture;
}