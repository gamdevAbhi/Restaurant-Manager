using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
    [SerializeField] private GameObject dirt;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private List<Dirt> dirtList;
    [SerializeField] private bool makeDirt;

    public List<Dirt> _dirtList
    {
        get { return new List<Dirt>(dirtList); }
    }

    private void Update()
    {
        if(makeDirt)
        {
            List<GridData> dirtableGrid = GetDirtableGrid();

            System.Random rand = new System.Random();

            int index = rand.Next(0, dirtableGrid.Count);

            Vector3 pos = dirtableGrid[index].transform.position;
            pos.y = 0.1251f;

            GameObject newDirtObject = Instantiate(dirt, pos, Quaternion.identity, transform);
            newDirtObject.transform.eulerAngles = new Vector3(90f, 0f, 0f);

            Dirt newDirt = newDirtObject.GetComponent<Dirt>();
            newDirt.SetVar(dirtableGrid[index], this);
            dirtList.Add(newDirt);
            
            makeDirt = false;
        }
    }

    protected internal void RemoveDirt(Dirt newDirt)
    {
        dirtList.Remove(newDirt);
    }

    private List<GridData> GetDirtableGrid()
    {
        List<GridData> dirtableGrid = gridManager._gridList;
        
        foreach (Dirt dirts in dirtList)
        {
            dirtableGrid.Remove(dirts._effectedGrid);
        }

        List<GridData> freeDirtableGrid = new List<GridData>();

        foreach (GridData grid in dirtableGrid)
        {
            if(grid.GetType() == typeof(StorableGrid) && grid._walkable == true)
            {
                freeDirtableGrid.Add(grid);
            }
        }

        return freeDirtableGrid;
    }
}
