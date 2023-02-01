using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : Employee
{
    [SerializeField] private uint level;
    [SerializeField] private bool doRandom;

    private void Update()
    {
        if(doRandom)
        {
            EmployeeData data = new EmployeeData();

            data.gender = IdentityCreator.GetGender(70, 30);
            data.employeeName = IdentityCreator.GetName(data.gender);
            data.age = IdentityCreator.GetAge();

            data = EmployeeCreator.CreateStat(level, this, data);
            SetStat(data);
            
            doRandom = false;
        }
    }
}
