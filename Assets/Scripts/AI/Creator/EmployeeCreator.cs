using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public static class EmployeeCreator
{
    public static EmployeeData CreateStat(uint level, Employee job, EmployeeData data)
    {
        uint speed = GetStat(level);
        uint cleaning = GetStat(level);
        uint cooking = GetStat(level);
        uint service = GetStat(level);

        if(job.GetType() == typeof(Cleaner))
        {
            speed += (speed * 5) / 100;
            cleaning += (cleaning * 25) / 100;
            cooking -= (cooking * 20) / 100;
            service -= (service * 10) / 100;
        }

        data.speed = speed;
        data.cleaning = cleaning;
        data.cooking = cooking;
        data.service = service;
        
        return data;
    }

    private static uint GetStat(uint level)
    {
        int lowestPoint = (int)level * 7;
        int highestPoint = (int)level * 20;

        Random random = new Random();

        return (uint)random.Next(lowestPoint, highestPoint);
    }
}
