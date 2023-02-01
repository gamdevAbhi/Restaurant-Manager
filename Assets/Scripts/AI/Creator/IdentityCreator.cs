using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class IdentityCreator
{
    private static string maleFirstNameDir = "Text/MaleFirstNames";
    private static string femaleFirstNameDir = "Text/FemaleFirstNames";
    private static string surnamesDir = "Text/Surnames";

    public static string GetName(bool gender)
    {
        if(gender)
        {
            return GetRandomLine(Resources.Load<TextAsset>(maleFirstNameDir)) + " " + GetRandomLine(Resources.Load<TextAsset>(surnamesDir));
        }
        else
        {
            return GetRandomLine(Resources.Load<TextAsset>(femaleFirstNameDir)) + " " + GetRandomLine(Resources.Load<TextAsset>(surnamesDir));
        }
    }

    private static string GetRandomLine(TextAsset asset)
    {
        System.Random rand = new System.Random();

        string[] words = asset.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        int line = rand.Next(0, words.Length);

        return words[line];
    }

    public static bool GetGender(int maleChance, int femaleChance)
    {
        int totalChance = maleChance + femaleChance;

        if(totalChance <= 0) return true;
        else
        {
            System.Random rand = new System.Random();
            int value = rand.Next(0, totalChance);

            return (value <= totalChance - Math.Min(maleChance, femaleChance))? 
            (maleChance > femaleChance)? true : false : 
            (maleChance > femaleChance)? false : true;
        }
    }

    public static uint GetAge()
    {
        int youngAge = 60;
        int midAge = youngAge + 30;
        int oldAge = midAge + 10;

        System.Random rand = new System.Random();
        int value = rand.Next(0, 100);

        if(value <= youngAge) return (uint)rand.Next(20, 28);
        else if(value <= midAge) return (uint)rand.Next(28, 40);
        else return (uint)rand.Next(40, 60);
    }
}
