using System;
using System.Collections;
using System.Collections.Generic;

public static class CustomerCreator
{
    public static uint currencyConversion = 2;

    public static CustomerData CreateCustomer(CustomerData data)
    {
        data.money = GetMoney(data.age, data.gender);

        return data;
    }

    private static uint GetMoney(uint age, bool gender)
    {
        Random random = new Random();

        uint money = (uint)((age <= 22)? random.Next(20, 50) : 
        (age <= 25)? random.Next(40, 70) : 
        (age <= 35)? random.Next(60, 100) :
        (age <= 46)? random.Next(70, 120) :
        random.Next(40, 80));

        money = (uint)((age >= 22)? (gender == true)? money * 1.2f : money * 1.1f : money);

        money *= currencyConversion;

        return money;
    }
}
