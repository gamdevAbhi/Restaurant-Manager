using UnityEngine;

[CreateAssetMenu(fileName = "TextureManagerHolder", menuName = "Restaurant Manager/Texture/Holder", order = 0)]
public class TextureManagerHolder : ScriptableObject
{
    public TextureManager skinManager;
    public TextureManagerPath maleTex;
    public TextureManagerPath femaleTex;
    public TextureManagerPath bothTex;

    [System.Serializable]
    public class TextureManagerPath
    {
        public TextureManager eyebrowManager;
        public TextureManager eyeManager;
        public TextureManager noseManager;
        public TextureManager mouthManager;
        public TextureManager earManager;
        public TextureManager clothManager;
        public TextureManager handManager;
        public TextureManager legManager;
        public TextureManager shoeManager;
    }
}
