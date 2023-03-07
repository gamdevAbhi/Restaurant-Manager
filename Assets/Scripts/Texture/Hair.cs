using UnityEngine;

[CreateAssetMenu(fileName = "Hair", menuName = "ScriptableObject/HumanTex/Hair", order = 0)]
public class Hair : ScriptableObject 
{
    public string hairName;
    public Mesh mesh;
    public Color[] color;
}
