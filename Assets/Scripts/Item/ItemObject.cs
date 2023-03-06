using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Restaurant Manager/ItemObject", order = 0)]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public enum ItemType { Chair, Table};
    public ItemType itemType = ItemType.Chair;
    public Mesh itemMesh;
    public Material material;
    public Color textureColor = Color.white;
    [Range(1,5)] public int starQuality = 1;
    public Stat stat = null;

    public ObjectTransform objectTransform;

    [Conditional("UNITY_EDITOR")]
    private void OnValidate() 
    {
        if(stat == null) 
        {
            DeleteAsset();
            stat = ScriptableObject.CreateInstance<ChairStat>();
            MakeAsset();
        }
        else if(itemType == ItemType.Chair && stat.GetType() != typeof(ChairStat))
        {
            DeleteAsset();
            stat = ScriptableObject.CreateInstance<ChairStat>();
            MakeAsset();
        }
        else if(itemType == ItemType.Table && stat.GetType() != typeof(TableStat))
        {
            DeleteAsset();
            stat = ScriptableObject.CreateInstance<TableStat>();
            MakeAsset();
        }
    }
    
    private void DeleteAsset()
    {
        AssetDatabase.DeleteAsset(Path.GetDirectoryName(AssetDatabase.GetAssetPath(this)) + "/" + 
        Path.GetFileName(AssetDatabase.GetAssetPath(this)).Split('.')[0] + "_" + "stat" + ".Asset");
    }

    private void MakeAsset()
    {
        AssetDatabase.CreateAsset(stat, Path.GetDirectoryName(AssetDatabase.GetAssetPath(this)) + "/" + 
        Path.GetFileName(AssetDatabase.GetAssetPath(this)).Split('.')[0] + "_" + "stat" + ".asset");

    }
}

[System.Serializable]
public class ObjectTransform
{
    public float height;
    public Vector3 rotation;
    public Vector3 scale;
}