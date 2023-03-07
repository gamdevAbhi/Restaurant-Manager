using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "TextureManager", menuName = "ScriptableObject/HumanTex/TextureManager", order = 0)]
public class TextureManager : ScriptableObject
{
    public List<string> tetxuresName;
    public string path;
    public bool update;

    [Conditional("UNITY_EDITOR")]
    private void OnValidate() 
    {
        if(update == false) return;

        string currentDirectory = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
        FileInfo[] allFiles = new DirectoryInfo(currentDirectory).GetFiles("*.asset");
        tetxuresName = new List<string>();

        for(int i = 0; i < allFiles.Length; i++)
        {
            if(this.name == allFiles[i].Name.Split(".")[0]) continue;
            else tetxuresName.Add(allFiles[i].Name.Split(".")[0]);
        }

        update = false;
    }
}