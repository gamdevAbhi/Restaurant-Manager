using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Job
{
    [SerializeField] private Texture[] textures;

    private void Awake()
    {
        employeeType = typeof(Cleaner);

        System.Random rand = new System.Random();
        work = (uint)rand.Next(10, 400);

        int index = (work < 100)? 0 : (work > 100 && work < 250)? 1 : 2;
        GetComponent<Renderer>().material.SetTexture("_MainTex", textures[index]);
    }

    public void Update()
    {
        int index = (work < 100)? 0 : (work > 100 && work < 250)? 1 : 2;

        if(GetComponent<Renderer>().material.GetTexture("_MainTex") != textures[index])
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex", textures[index]);
        }
    }

    private void OnDestroy()
    {
        dirtManager.RemoveDirt(this);
    }
}
