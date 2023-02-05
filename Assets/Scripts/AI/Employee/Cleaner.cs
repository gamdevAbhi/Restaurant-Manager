using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : Employee
{
    [SerializeField] private DirtManager dirtManager;
    [SerializeField] private Dirt job;

    private void Awake()
    {
        currentGrid = gridManager.GetClosestGridFromWorld(transform.position);
        transform.position = new Vector3(currentGrid.transform.position.x, transform.position.y, currentGrid.transform.position.z);

        EmployeeData data = new EmployeeData();

        data.gender = IdentityCreator.GetGender(70, 30);
        data.employeeName = IdentityCreator.GetName(data.gender);
        data.age = IdentityCreator.GetAge();

        data = EmployeeCreator.CreateStat(1, this, data);
        SetStat(data);
    }

    private void Update()
    {
        SetJob();

        if(job != null)
        {
            if(job._effectedGrid == currentGrid)
            {
                DoJob();
            }
            else
            {
                bool doneGridMove = MoveTowards(path[0]);
                if(doneGridMove)
                {
                    currentGrid = path[0];
                    path.Remove(path[0]);
                }
            }
        }
    }

    private void DoJob()
    {
        float deltaTime = Time.deltaTime;
        time = (time - deltaTime >= 0)? time - deltaTime : 0;

        if(time == 0)
        {
            int value = Random.Range((int)((float)_cleaning * 0.75f), (int)_cleaning + 1);

            job.RemoveStain((uint)value);
            time = 0.25f;
        }
    }

    private void SetJob()
    {
        if(dirtManager._dirtList.Count > 0 && job == null)
        {
            for(int i = 0; i < dirtManager._dirtList.Count; i++)
            {
                List<GridData> newPath = Pathfinder.FindPath(gridManager.GetWalkableGrid(), currentGrid, dirtManager._dirtList[i]._effectedGrid);

                if(newPath.Count > 0 || currentGrid == dirtManager._dirtList[i]._effectedGrid)
                {
                    if( dirtManager._dirtList[i].SetCleaner(this))
                    {
                        job = dirtManager._dirtList[i];
                        path = newPath;
                        break;
                    }
                }
            }
        }
    }
}
