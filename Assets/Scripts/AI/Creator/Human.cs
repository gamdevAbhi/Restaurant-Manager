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

    [HideInInspector] public string _humanName;
    [HideInInspector] public uint _age;
    [HideInInspector] public bool _gender;

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
        if(path.Count > 1)
        {
            if(path[0]._occupiedObject != null && path[0]._occupiedObject != gameObject) return false;
        }

        return true;
    }
}
