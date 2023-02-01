using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] private List<GridData> gridList;
    [HideInInspector] [SerializeField] private bool shouldUpdate = false;

    public List<GridData> _gridList
    {
        get { return gridList; }
    }

    private void Update()
    {
        #if UNITY_EDITOR

        if(shouldUpdate)
        {
            InitializeNeighbours();
            shouldUpdate = false;
        }

        List<GridData> tempGrid = new List<GridData>();

        foreach(GridData grid in gridList)
        {
            if(grid != null)
            {
                tempGrid.Add(grid);
            }
        }

        if(tempGrid.Count != gridList.Count)
        {
            gridList = tempGrid;

            InitializeNeighbours();
        }

        #endif
    }

    private void AddGrid(GridData grid)
    {
        if(gridList.Contains(grid) == false)
        {
            gridList.Add(grid);
        }
    }

    protected internal List<GridData> GetWalkableGrid()
    {
        List<GridData> walkableGrid = new List<GridData>();

        foreach(GridData grid in gridList)
        {
            if(grid._walkable == true)
            {
                walkableGrid.Add(grid);
            }
        }

        return walkableGrid;
    }

    protected void InitializeNeighbours()
    {
        foreach(GridData grid in gridList)
        {
            grid.GetComponent<GridData>().SetNeighbours(GetNeighbours(grid._gridPos));
            grid.Initialize();
            grid.Save();
        }
    }

    private List<GridData> GetNeighbours(Vector2 gridPos)
    {
        List<GridData> data = new List<GridData>();

        List<Vector2> pos = new List<Vector2>() {new Vector2(gridPos.x - 1, gridPos.y), new Vector2(gridPos.x + 1, gridPos.y),
        new Vector2(gridPos.x, gridPos.y - 1), new Vector2(gridPos.x, gridPos.y + 1)};

        foreach(Vector2 _pos in pos)
        {
            int value = GridExist(_pos);

            if(value > -1)
            {
                data.Add(transform.GetChild(value).GetComponent<GridData>());
            }
        }

        return data;
    }

    private int GridExist(Vector2 gridPos)
    {
        foreach(GridData grid in gridList)
        {
            if(grid._gridPos == gridPos)
                return grid.transform.GetSiblingIndex();
        }

        return -1;
    }
}
