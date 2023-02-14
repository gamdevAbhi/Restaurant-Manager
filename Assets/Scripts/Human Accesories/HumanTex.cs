using UnityEngine;

[CreateAssetMenu(fileName = "HumanTex", menuName = "Restaurant Manager/HumanTex", order = 0)]
public class HumanTex : ScriptableObject
{
    public string texName = "";
    public Texture texture;
    public Color color;
    public enum TexType {Male, Female, Both, Half, Full, UShape, FreeShape};
    public TexType type = TexType.Both;
}
