using UnityEngine;

public abstract class HumanData 
{
    public string name;
    public uint age;
    public bool gender;

    public Texture skinTex;
    public Texture eyebrowTex;
    public Texture eyeTex;
    public Texture noseTex;
    public Texture mouthTex;
    public Texture earTex;

    public Color mouthColor = Color.white;

    public Texture clothTex;
    public Texture clothTypeTex;
    public Texture clothPatternTex;
    
    public Color clothColor;
    public Color clothTypeColor;

    public Texture handTex;
    public Texture handPatternTex;

    public Color handColor;
    public Color handPatternColor;

    public Texture legTex;
    public Texture legPatternTex;

    public Color legColor;
    public Color legPatternColor;
    
    public Texture shoeTex;
    public Color shoeColor;

    public Mesh hair;
    public Color hairColor;
}