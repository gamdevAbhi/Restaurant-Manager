using System.Collections.Generic;
using UnityEngine;

public static class AppearanceCreator
{
    private static TextureManagerHolder textureManagerHolder;

    private static void Initialize()
    {
        textureManagerHolder = Resources.Load<TextureManagerHolder>("TextureManagerHolder");
    }

    private static string GetFullAssetPath(TextureManager manager, int index)
    {
        return manager.path + "/" + manager.tetxuresName[index];
    }

    private static string GetRandomTexture(TextureManager bothTexture, TextureManager genderTexture)
    {
        string path;

        if(bothTexture != null && genderTexture != null)
        {
            if(bothTexture.tetxuresName.Count == 0 && genderTexture.tetxuresName.Count == 0) return "";

            int random = Random.Range(0, 100);

            if((random < 50 && genderTexture.tetxuresName.Count > 0) || bothTexture.tetxuresName.Count == 0)
            {
                path = GetFullAssetPath(genderTexture, Random.Range(0, genderTexture.tetxuresName.Count));
            }
            else
            {
                path = GetFullAssetPath(bothTexture, Random.Range(0, bothTexture.tetxuresName.Count));
            }
        }
        else if(bothTexture == null && genderTexture == null)
        {
            return "";
        }
        else
        {
            if(bothTexture == null)
            {
                path = GetFullAssetPath(genderTexture, Random.Range(0, genderTexture.tetxuresName.Count));
            }
            else
            {
                path = GetFullAssetPath(bothTexture, Random.Range(0, bothTexture.tetxuresName.Count));
            }
        }

        return path;
    }

    private static HumanData GetHumanTexture(HumanData data, TextureManagerPath bothTex, TextureManagerPath genderTex)
    {
        string eyebrowPath = GetRandomTexture(bothTex.eyebrowManager, genderTex.eyebrowManager);
        Eyebrow eyebrow = Resources.Load<Eyebrow>(eyebrowPath);

        string eyePath = GetRandomTexture(bothTex.eyeManager, genderTex.eyeManager);
        Eye eye = Resources.Load<Eye>(eyePath);

        string nosePath = GetRandomTexture(bothTex.noseManager, genderTex.noseManager);
        Nose nose = Resources.Load<Nose>(nosePath);

        string mouthPath = GetRandomTexture(bothTex.mouthManager, genderTex.mouthManager);
        Mouth mouth = Resources.Load<Mouth>(mouthPath);

        string earPath = GetRandomTexture(bothTex.earManager, genderTex.earManager);
        Ear ear = Resources.Load<Ear>(earPath);

        string clothPath = GetRandomTexture(bothTex.clothManager, genderTex.clothManager);
        Cloths cloth = Resources.Load<Cloths>(clothPath);
        Instance clothType = (cloth.clothType.Length > 0)? cloth.clothType[Random.Range(0, cloth.clothType.Length)] : null;

        string handPath = GetRandomTexture(bothTex.handManager, genderTex.handManager);
        Hand hand = Resources.Load<Hand>(handPath);

        string legPath = GetRandomTexture(bothTex.legManager, genderTex.legManager);
        Leg leg = Resources.Load<Leg>(legPath);

        string shoePath = GetRandomTexture(bothTex.shoeManager, genderTex.shoeManager);
        Shoe shoe = Resources.Load<Shoe>(shoePath);

        string hairPath = GetRandomTexture(bothTex.hairManager, genderTex.hairManager);
        Hair hair = Resources.Load<Hair>(hairPath);

        data.eyebrowTex = eyebrow.texture;
        data.eyeTex = eye.texture;
        data.noseTex = nose.texture;
        data.mouthTex = mouth.texture;
        if(mouth.color.Length > 0) data.mouthColor = mouth.color[Random.Range(0, mouth.color.Length)];
        else data.mouthColor = Color.white;
        data.earTex = ear.texture;
        
        data.hair = hair.mesh;
        data.hairColor = hair.color[Random.Range(0, hair.color.Length)];
        
        data.clothTex = cloth.texture;
        data.clothColor = cloth.color[Random.Range(0, cloth.color.Length)];
        int shouldClothType = Random.Range(0, 100);

        if(shouldClothType > 50 && clothType != null)
        {
            data.clothTypeTex = clothType.texture;
            data.clothTypeColor = clothType.color[Random.Range(0, clothType.color.Length)];
        }
        if(clothType != null)
        {
            if((clothType.patternAllowed == true && shouldClothType > 90 && cloth.clothPattern.Length > 0) || (shouldClothType < 10 && cloth.clothPattern.Length > 0))
            {
                data.clothPatternTex = cloth.clothPattern[Random.Range(0, cloth.clothPattern.Length)];
            }
        }

        if(cloth.extendedHand == true)
        {
            data.handTex = hand.texture;
            data.handColor = hand.color[Random.Range(0, hand.color.Length)];
            if(Random.Range(0, 100) > 50) data.handPatternTex = hand.handPattern[Random.Range(0, hand.handPattern.Length)];
            data.handPatternColor = Color.white;
        }

        data.legTex = leg.texture;
        data.legColor = leg.color[Random.Range(0, leg.color.Length)];

        if(Random.Range(0, 100) > 50 && leg.legPattern.Length > 0) 
        {
            LegPattern LegPattern = leg.legPattern[Random.Range(0, leg.legPattern.Length)];
            data.legPatternTex = LegPattern.texture;
            data.legPatternColor = LegPattern.color[Random.Range(0, LegPattern.color.Length)];
        }

        data.shoeTex = shoe.texture;
        data.shoeColor = shoe.color[Random.Range(0, shoe.color.Length)];

        return data;
    }

    public static HumanData GetRandomAppearance(HumanData data)
    {
        if(textureManagerHolder == null) Initialize();
        
        int index = Random.Range(0, textureManagerHolder.skinManager.tetxuresName.Count);
        data.skinTex = Resources.Load<Skin>(GetFullAssetPath(textureManagerHolder.skinManager, index)).texture;

        if(data.gender == true)
        {
            data = GetHumanTexture(data, textureManagerHolder.bothTex, textureManagerHolder.maleTex);
        }
        else
        {
            data = GetHumanTexture(data, textureManagerHolder.bothTex, textureManagerHolder.femaleTex);
        }

        return data;
    }
}
