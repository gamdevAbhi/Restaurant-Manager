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
    public Vector2[] occupiedGridDistance;
    
    public Vector2[] controlGridItem;
    public int controlItem;
}

[System.Serializable]
public class ObjectTransform
{
    public float height;
    public Vector3 rotation;
    public Vector3 scale;
}
