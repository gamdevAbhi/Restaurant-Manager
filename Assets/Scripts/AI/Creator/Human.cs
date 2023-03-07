using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    [SerializeField] protected internal GridManager gridManager;
    [SerializeField] protected internal GridData currentGrid;
    protected internal List<GridData> path;
    protected internal float waitingTime;

    [SerializeField] private static string humanName;
    [SerializeField] private static uint age;
    [SerializeField] private static bool gender;

    [SerializeField] private Renderer faceRenderer;
    [SerializeField] private Renderer clothRenderer;
    [SerializeField] private Renderer[] handRenderer;
    [SerializeField] private Renderer[] legRenderer;
    [SerializeField] private Renderer[] shoeRenderer;
    [SerializeField] private MeshFilter hairMeshFiler;

    [HideInInspector] public string _humanName;
    [HideInInspector] public uint _age;
    [HideInInspector] public bool _gender;

    protected internal abstract void SetStat(Type type, System.Object newData);

    protected internal void SetInfo(HumanData data)
    {
        humanName = data.name;
        age = data.age;
        gender = data.gender;

        _humanName = humanName;
        _age = age;
        _gender = gender;
    }

    protected internal void SetTex(HumanData data)
    {
        faceRenderer.material.SetTexture("_SkinTex", data.skinTex);
        faceRenderer.material.SetTexture("_EyebrowTex", data.eyebrowTex);
        faceRenderer.material.SetTexture("_EyeTex", data.eyeTex);
        faceRenderer.material.SetTexture("_NoseTex", data.noseTex);
        faceRenderer.material.SetTexture("_MouthTex", data.mouthTex);
        faceRenderer.material.SetTexture("_EarTex", data.earTex);
        faceRenderer.material.SetColor("_MouthColor", data.mouthColor);

        clothRenderer.material.SetTexture("_SkinTex", data.skinTex);
        clothRenderer.material.SetTexture("_ClothTex", data.clothTex);
        if(data.clothTypeTex != null) clothRenderer.material.SetTexture("_ClothTypeTex", data.clothTypeTex);
        if(data.clothPatternTex != null) clothRenderer.material.SetTexture("_ClothPatternTex", data.clothPatternTex);
        clothRenderer.material.SetColor("_ClothColor", data.clothColor);
        if(data.clothTypeTex != null) clothRenderer.material.SetColor("_ClothTypeColor", data.clothTypeColor);
        if(data.clothTypeTex != null) clothRenderer.material.SetInt("_ClothType", 1);
        if(data.clothPatternTex != null) clothRenderer.material.SetInt("_ClothPattern", 1);

        for(int i = 0; i < 2; i++)
        {
            handRenderer[i].material.SetTexture("_SkinTex", data.skinTex);
            if(data.handTex != null) handRenderer[i].material.SetTexture("_HandLegTex", data.handTex);
            if(data.handPatternTex != null) handRenderer[i].material.SetTexture("_HandLegPatternTex", data.handPatternTex);
            handRenderer[i].material.SetColor("_HandLegColor", data.handColor);
            if(data.handTex != null) handRenderer[i].material.SetInt("_HandLeg", 1);
            if(data.handPatternTex != null) handRenderer[i].material.SetInt("_HandLegPattern", 1);

            legRenderer[i].material.SetTexture("_SkinTex", data.skinTex);
            legRenderer[i].material.SetTexture("_HandLegTex", data.legTex);
            if(data.legPatternTex != null) legRenderer[i].material.SetTexture("_HandLegPatternTex", data.legPatternTex);
            legRenderer[i].material.SetColor("_HandLegColor", data.legColor);
            legRenderer[i].material.SetInt("_HandLeg", 1);
            if(data.legPatternTex != null) legRenderer[i].material.SetInt("_HandLegPattern", 1);

            shoeRenderer[i].material.SetTexture("_MainTex", data.shoeTex);
            shoeRenderer[i].material.SetColor("_Color", data.shoeColor);
        }

        hairMeshFiler.mesh = data.hair;
        hairMeshFiler.gameObject.GetComponent<Renderer>().material.SetColor("_Color", data.hairColor);
    }

    protected internal virtual bool MoveTowards(GridData targetGrid, uint speed)
    {
        Vector3 difference = new Vector3(targetGrid.transform.position.x - transform.position.x, 0,
        targetGrid.transform.position.z - transform.position.z);

        Vector3 movement =  difference.normalized * speed * Time.deltaTime / 10;

        if(difference.magnitude <= movement.magnitude || difference == movement)
        {
            transform.position = transform.position + difference;
            return true;
        }
        else
        {
            transform.position += movement;
            return false;
        }
    }

    protected internal bool CheckIfPathAvailable()
    {
        if(path.Count >= 1)
        {
            if(path[0]._occupiedObject != null && path[0]._occupiedObject != gameObject) return false;
        }

        return true;
    }

    private void OnCollisionStay(Collision other)
    {
        GridData grid = other.gameObject.GetComponent<GridData>();

        if(grid != null && grid._occupiedObject == null)
        {
            currentGrid = grid;
        }
    }
}
