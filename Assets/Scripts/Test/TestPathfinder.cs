using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathfinder : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridData start;
    [SerializeField] private GridData destination;
    [SerializeField] private Color color;
    
    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0) && start == null)
        {
            if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                start = hit.transform.GetComponent<GridData>();
                start.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }
        if(Input.GetMouseButtonDown(1)  && destination == null)
        {
            if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                destination = hit.transform.GetComponent<GridData>();
                destination.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
        if(Input.GetMouseButtonDown(2))
        {
            if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                GridData obstacle = hit.transform.GetComponent<GridData>();

                // if(obstacle._occupiedObject == true)
                // {
                //     obstacle.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                //     obstacle._occupiedObject = false;
                // }
                // else
                // {
                //     obstacle.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                //     obstacle._occupiedObject = true;
                // }
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            List<GridData> grids = gridManager._gridList;

            // foreach(GridData grid in grids)
            // {
            //     if(grid._occupiedObject == true)
            //     {
            //         grid.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            //     }
            // }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            List<GridData> path = Pathfinder.FindPath(gridManager.GetWalkableGrid(), start, destination);

            foreach(GridData grid in path)
            {
                grid.GetComponent<Renderer>().material.SetColor("_Color", color);
            }

            start = null;
            destination = null;
        }
    }
}
