using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Employee : MonoBehaviour
{
    [SerializeField] protected internal GridManager gridManager;
    protected internal GridData currentGrid;
    protected internal List<GridData> path;

    [SerializeField] private static string employeeName;
    [SerializeField] private static uint age;
    [SerializeField] private static bool gender;
    
    [HideInInspector] [SerializeField] private uint speed;
    [HideInInspector] [SerializeField] private uint cleaning;
    [HideInInspector] [SerializeField] private uint cooking;
    [HideInInspector] [SerializeField] private uint service;
    protected internal float time = 0.5f;

    [HideInInspector] public string _employeeName;
    [HideInInspector] public uint _age;
    [HideInInspector] public bool _gender;

    public uint _speed
    {
        get { return speed; }
    }

    public uint _cleaning
    {
        get { return cleaning; }
    }

    public uint _cooking
    {
        get { return cooking; }
    }

    public uint _service
    {
        get { return service; }
    }

    protected internal virtual void SetStat(EmployeeData data)
    {
        employeeName = data.employeeName;
        age = data.age;
        gender = data.gender;

        _employeeName = employeeName;
        _age = age;
        _gender = gender;

        speed = data.speed;
        cleaning = data.cleaning;
        cooking = data.cooking;
        service = data.service;
    }

    protected internal virtual bool MoveTowards(GridData targetGrid)
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
