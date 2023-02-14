using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class HumanTexRegister : MonoBehaviour
{
    [SerializeField] private Register[] registers;
    [SerializeField] private bool shouldSave = false;
    [SerializeField] private bool shouldDelete = false;

    private void Update()
    {
        #if UNITY_EDITOR
        if(shouldSave) SaveAllRegister();
        if(shouldDelete) DeleteAllRegister();
        #endif
    }

    private void SaveAllRegister()
    {
        DeleteAllRegister();

        foreach(Register reg in registers)
        {
            string path = Application.dataPath + "/Resources/" + reg.folderName;

            if(Directory.Exists(path))
            {
                ChangeRegisterInfo(path, reg);
            }
        }

        shouldSave = false;
    }

    private void DeleteAllRegister()
    {
        foreach(Register reg in registers)
        {
            string path = Application.dataPath + "/Resources/" + reg.folderName;

            if(Directory.Exists(path) == false) continue;

            foreach(HumanTex.TexType type in reg.supprotedType)
            {
                if(File.Exists(path + "/" + type.ToString() + ".txt")) 
                {
                    File.Delete(path + "/" + type.ToString() + ".txt");
                    File.Delete(path + "/" + type.ToString() + ".txt" + ".meta");
                };
            }
        }

        shouldDelete = false;
    }

    private void ChangeRegisterInfo(string path, Register reg)
    {
        string[] files = Directory.GetFiles(path);

        foreach(string file in files)
        {
            if(file.Contains(".asset") == false || file.Contains(".meta") == true) continue;

            HumanTex hTex = AssetDatabase.LoadAssetAtPath<HumanTex>("Assets/Resources/" + 
            reg.folderName + "/" + Path.GetFileName(file));

            foreach(HumanTex.TexType type in reg.supprotedType)
            {
                if(hTex.type == type)
                {
                    using(FileStream fileStream = new FileStream(path + "/" + type.ToString() + ".txt", 
                    FileMode.Append, FileAccess.Write))
                    using(StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.WriteLine(hTex.name);
                    }
                }
            }
            
        }
    }

    [System.Serializable]
    public class Register
    {
        public string folderName;
        public HumanTex.TexType[] supprotedType;
    }
}


