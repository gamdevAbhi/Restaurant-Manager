using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathfinder : Pathfinder
{
    [SerializeField] private GridData start;
    [SerializeField] private GridData destination;
    [SerializeField] private Color color;
    [SerializeField] private bool doSearch = false;

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(doSearch)
        {
            List<GridData> path = FindPath(start, destination);

            foreach(GridData grid in path)
            {
                grid.GetComponent<Renderer>().material.SetColor("_Color", color);
            }

            doSearch = false;
            start = null;
            destination = null;
        }

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

                if(obstacle._walkable == true)
                {
                    obstacle.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    obstacle._walkable = false;
                }
                else
                {
                    obstacle.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    obstacle._walkable = true;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            List<GridData> grids = gridManager._gridList;

            foreach(GridData grid in grids)
            {
                if(grid._walkable == true)
                {
                    grid.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            doSearch = true;
        }
    }
}
