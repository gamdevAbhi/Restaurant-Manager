using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] private uint stainValue;
    [SerializeField] private GridData effectedGrid;
    [SerializeField] private Cleaner cleaner;
    [SerializeField] private Texture[] textures;
    [SerializeField] private DirtManager dirtManager;

    public GridData _effectedGrid
    {
        get { return effectedGrid; }
    }

    private void Awake()
    {
        System.Random rand = new System.Random();
        stainValue = (uint)rand.Next(10, 400);

        int index = (stainValue < 100)? 0 : (stainValue > 100 && stainValue < 250)? 1 : 2;
        GetComponent<Renderer>().material.SetTexture("_MainTex", textures[index]);
    }

    public void Update()
    {
        int index = (stainValue < 100)? 0 : (stainValue > 100 && stainValue < 250)? 1 : 2;

        if(GetComponent<Renderer>().material.GetTexture("_MainTex") != textures[index])
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex", textures[index]);
        }
    }
    
    protected internal void SetVar(GridData grid, DirtManager manager)
    {
        effectedGrid = grid;
        dirtManager = manager;
    }
    
    protected internal bool SetCleaner(Cleaner cleaner)
    {
        if(this.cleaner == null)
        {
            this.cleaner = cleaner;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected internal void RemoveStain(uint value)
    {
        stainValue = ((int)stainValue - (int)value >= 0)? stainValue - value : 0;

        if(stainValue == 0) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        dirtManager.RemoveDirt(this);
    }
}
