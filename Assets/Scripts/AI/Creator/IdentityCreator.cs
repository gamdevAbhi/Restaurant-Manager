using System;
using System.Collections.Generic;
using UnityEngine;

public static class IdentityCreator
{
    private static string maleFirstNameDir = "Names/MaleFirstNames";
    private static string femaleFirstNameDir = "Names/FemaleFirstNames";
    private static string surnamesDir = "Names/Surnames";

    private static string[] firstNamesMale;
    private static string[] firstNamesFemale;
    private static string[] surnames;

    private static Dictionary<string, string> humanTexPath = new Dictionary<string, string>() 
    {
        {"Head", "Head/"}, {"Body", "Body/"}, {"Hand", "Hand/"}, {"Leg", "Leg/"}, {"Shoe", "Shoe/"},
        {"Pattern", "Pattern/"}, {"Skin", "Skin/"}
    };

    public static string GetName(bool gender) 
    {
        if(firstNamesMale == null) firstNamesMale = AssetToNames(maleFirstNameDir);
        if(firstNamesFemale == null) firstNamesFemale = AssetToNames(femaleFirstNameDir);
        if(surnames == null) surnames = AssetToNames(surnamesDir);

        if(gender)
        {
            return GetRandomLine(firstNamesMale) + " " + GetRandomLine(surnames);
        }
        else
        {
            return GetRandomLine(firstNamesFemale) + " " + GetRandomLine(surnames);
        }
    }

    private static string[] AssetToNames(string path)
    {
        TextAsset asset = Resources.Load<TextAsset>(path);

        if(asset == null) return new string[0];

        string[] output = asset.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Resources.UnloadAsset(asset);
        return output;
    }

    private static string GetRandomLine(string[] words)
    {
        System.Random rand = new System.Random();

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

        if(value <= youngAge) return (uint)rand.Next(17, 28);
        else if(value <= midAge) return (uint)rand.Next(28, 40);
        else return (uint)rand.Next(40, 60);
    }
}
