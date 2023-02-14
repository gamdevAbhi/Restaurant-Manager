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
    [SerializeField] private static HumanTex headTex;
    [SerializeField] private static HumanTex skinTex;
    [SerializeField] private static HumanTex handTex;
    [SerializeField] private static HumanTex legTex;
    [SerializeField] private static HumanTex bodyTex;

    [HideInInspector] public string _humanName;
    [HideInInspector] public uint _age;
    [HideInInspector] public bool _gender;
    [HideInInspector] public string _headTex;
    [HideInInspector] public string _skinTex;
    [HideInInspector] public string _handTex;
    [HideInInspector] public string _legTex;
    [HideInInspector] public string _bodyTex;

    protected internal abstract void SetStat(Type type, System.Object newData);

    protected internal void SetInfo(string name, uint age_, bool gender_)
    {
        humanName = name;
        age = age_;
        gender = gender_;

        _humanName = humanName;
        _age = age;
        _gender = gender;
    }

    protected internal void SetTex(HumanTex skin, HumanTex head, HumanTex hand, HumanTex body, HumanTex leg)
    {
        headTex = head;
        skinTex = skin;
        handTex = hand;
        bodyTex = body;
        legTex = leg;

        //_headTex = headTex.texName;
        _skinTex = skinTex.texName;
        //_handTex = handTex.texName;
        //_bodyTex = bodyTex.texName;
        //_legTex = legTex.texName;

        ApplyTex();
    }

    protected internal void ApplyTex()
    {
        foreach(Transform trans in transform)
        {
            Renderer render = trans.GetComponent<Renderer>();
            render.material.SetTexture("_SkinTex", skinTex.texture);
        }
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
