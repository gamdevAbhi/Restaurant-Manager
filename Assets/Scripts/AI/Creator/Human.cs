using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    [SerializeField] protected internal GridManager gridManager;
    [SerializeField] protected internal GridData currentGrid;
    protected internal List<GridData> path;

    [SerializeField] private static string employeeName;
    [SerializeField] private static uint age;
    [SerializeField] private static bool gender;

    [HideInInspector] public string _employeeName;
    [HideInInspector] public uint _age;
    [HideInInspector] public bool _gender;

    protected internal virtual void SetStat(EmployeeData data)
    {
        employeeName = data.employeeName;
        age = data.age;
        gender = data.gender;

        _employeeName = employeeName;
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
}
