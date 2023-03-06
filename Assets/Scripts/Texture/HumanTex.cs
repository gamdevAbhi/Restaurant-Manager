using UnityEngine;

[CreateAssetMenu(fileName = "HumanTex", menuName = "Restaurant Manager/HumanTex", order = 0)]
public class HumanTex : ScriptableObject {
    public enum TexType {};
    public string texName;
    public Texture texture;
    public TexType type;
}