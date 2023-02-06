using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] private List<GridData> gridList;
    [SerializeField] private Vector2 worldToGrid = new Vector2(1, 1);
    [HideInInspector] [SerializeField] private bool shouldUpdate = false;

    public List<GridData> _gridList
    {
        get { return new List<GridData>(gridList); }
    }

    public Vector2 _worldToGrid
    {
        get { return worldToGrid; }
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

    protected internal GridData GetClosestGridFromWorld(Vector3 worldPos)
    {
        float lowestMagnitude = float.MaxValue;
        int index = -1;

        for(int i = 0; i < gridList.Count; i++)
        {
            float magnitude = (gridList[i].transform.position - worldPos).magnitude;

            if(lowestMagnitude > magnitude)
            {
                index = i;
                lowestMagnitude = magnitude;
            }
        }

        if(index > -1) return gridList[index];
        else return null;
    }

    protected internal List<GridData> GetWalkableGrid()
    {
        List<GridData> walkableGrid = new List<GridData>();

        foreach(GridData grid in gridList)
        {
            if(grid._occupiedObject == null)
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
