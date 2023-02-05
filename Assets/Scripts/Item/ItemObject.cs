using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Restaurant Manager/ItemObject", order = 0)]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public enum ItemType { chair, table};
    public ItemType itemType;
    public Mesh itemMesh;
    public Material material;
    public Color textureColor = Color.white;

    public ObjectTransform objectTransform;
    public Vector3[] occupiedPos;
}

[System.Serializable]
public class ObjectTransform
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}
