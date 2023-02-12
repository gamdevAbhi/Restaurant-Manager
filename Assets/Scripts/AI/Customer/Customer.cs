using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Human 
{
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private Item activityItem;
    [SerializeField] private uint money;

    public uint _money
    {
        get { return money; }
    }

    // TODO: ! IT'S FOR TESTING PURPOSE ! TODO:

    private void Awake()
    {
        currentGrid = gridManager.GetClosestGridFromWorld(transform.position);
        currentGrid._occupiedObject = gameObject;

        transform.position = new Vector3(currentGrid.transform.position.x, 
        transform.position.y, currentGrid.transform.position.z);

        CustomerData data = new CustomerData();

        data.gender = IdentityCreator.GetGender(60, 40);
        data.age = IdentityCreator.GetAge();
        data.name = IdentityCreator.GetName(data.gender);

        data = CustomerCreator.CreateCustomer(data);

        SetStat(typeof(CustomerData), data);

        gameObject.name = data.name + " " + "(Customer)";
    }

    private void Update()
    {
        currentGrid._occupiedObject = null;
        currentGrid = gridManager.GetClosestGridFromWorld(transform.position);
        currentGrid._occupiedObject = gameObject;

        FindActivity(null);

        if(activityItem != null)
        {
            if(path.Count > 0 && currentGrid != activityItem.occupiedGrid[0])
            {
                bool isMoved = MoveTowards(path[0], 10);

                if(isMoved)
                {
                    path.Remove(path[0]);
                }
            }
            else if(activityItem.occupiedGrid[0] == currentGrid && path.Count == 0)
            {
                waitingTime = 0f;
            }
            else if(CheckIfPathAvailable() == false)
            {
                if(waitingTime <= 0.5f)
                {
                    waitingTime += Time.deltaTime;
                }
                else if(waitingTime <= 1.5f)
                {
                    FindActivity(activityItem);
                    waitingTime += Time.deltaTime;
                }
            }
        }
        
    }

    private void FindActivity(Item item)
    {
        if(item == null && activityItem == null)
        {
            List<Item> tables = itemManager.FindItem(ItemObject.ItemType.table);

            foreach(Item table in tables)
            {
                Item[] chairs = table.GetComponent<TableItem>().controlItem;

                foreach(Item chair in chairs)
                {
                    if(chair == null)
                    {
                        continue;
                    }
                    
                    if(chair.GetComponent<ChairItem>().controlHuman[0] == null)
                    {
                        GridData grid = chair.occupiedGrid[0];

                        List<GridData> newPath = Pathfinder.FindPath(gridManager.GetWalkableGrid(),
                        currentGrid, grid, true, true);
                        

                        if(newPath.Count > 0)
                        {
                            path = newPath;
                            activityItem = chair;
                            chair.GetComponent<ChairItem>().controlHuman[0] = this;
                            break;
                        }
                    }
                }
            }
        }
        else if(item != null)
        {
            GridData grid = activityItem.occupiedGrid[0];

            List<GridData> newPath = Pathfinder.FindPath(gridManager.GetWalkableGrid(),
            currentGrid, grid, true, true);
                        

            if(newPath.Count > 0)
            {
                path = newPath;
            }
        }
    }

    // TODO: -> END HERE <- TODO:

    protected internal override void SetStat(Type type, System.Object newData)
    {
        CustomerData data  = (CustomerData)Convert.ChangeType(newData, type);

        SetInfo(data.name, data.age, data.gender);

        money = data.money;
    }
}