using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Job : MonoBehaviour
{
    [SerializeField] protected internal uint work;
    [SerializeField] protected internal GridData effectedGrid;
    [SerializeField] protected internal Employee employee;
    [SerializeField] protected internal System.Type employeeType;
    [SerializeField] protected internal DirtManager dirtManager;

    protected internal virtual void SetVar(GridData grid, DirtManager manager)
    {
        effectedGrid = grid;
        dirtManager = manager;
    }
    
    protected internal virtual bool SetEmployee(Employee employee)
    {
        if(this.employee == null && employee.GetType() == employeeType)
        {
            this.employee = employee;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected internal virtual void RemoveEmployee(Employee employee)
    {
        if(this.employee == employee && employee.GetType() == employeeType) this.employee = null;
    }

    protected internal virtual void DoWork(uint value)
    {
        work = ((int)work - (int)value >= 0)? work - value : 0;

        if(work == 0) Destroy(this.gameObject);
    }
}
