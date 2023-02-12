using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Employee : Human
{
    [SerializeField] protected internal EmployeeManager employeeManager;
    [HideInInspector] [SerializeField] private uint speed;
    [HideInInspector] [SerializeField] private uint cleaning;
    [HideInInspector] [SerializeField] private uint cooking;
    [HideInInspector] [SerializeField] private uint service;
    protected internal float time = 0.5f;

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

    protected internal override void SetStat(Type type, System.Object newData)
    {
        EmployeeData data  = (EmployeeData)Convert.ChangeType(newData, type);

        SetInfo(data.name, data.age, data.gender);
        
        speed = data.speed;
        cleaning = data.cleaning;
        cooking = data.cooking;
        service = data.service;
    }
}
