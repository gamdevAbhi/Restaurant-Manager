using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Employee : MonoBehaviour
{
    // FIXME: make the identity static

    [SerializeField] private string employeeName;
    [SerializeField] private uint age;
    [SerializeField] private bool gender;
    
    [SerializeField] private uint speed;
    [SerializeField] private uint cleaning;
    [SerializeField] private uint cooking;
    [SerializeField] private uint service;

    protected internal virtual void SetStat(EmployeeData data)
    {
        employeeName = data.employeeName;
        age = data.age;
        gender = data.gender;

        speed = data.speed;
        cleaning = data.cleaning;
        cooking = data.cooking;
        service = data.service;
    }
}
