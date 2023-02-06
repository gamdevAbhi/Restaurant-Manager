using System;
using System.Collections.Generic;

public static class Pathfinder
{
    public static List<GridData> FindPath(List<GridData> gridList, GridData start, GridData destination)
    {
        gridList.Add(start);
        
        Dictionary<GridData, Node> node = new Dictionary<GridData, Node>();
        List<Node> closedNode = new List<Node>();
        List<Node> openNode = new List<Node>();
        List<GridData> path = new List<GridData>();

        if(gridList.Contains(start) == false || gridList.Contains(destination) == false)
        {
            return path;
        }

        foreach(GridData data in gridList)
        {
            node.Add(data, new Node(data));
        }

        node[start].g = 0;
        node[start].h = Math.Abs(start._gridPos.x - destination._gridPos.x) + Math.Abs(start._gridPos.y - destination._gridPos.y);
        node[start].f = node[start].g + node[start].h;

        openNode.Add(node[start]);

        while(openNode.Count > 0)
        {
            Node currentNode = openNode[0];

            for(int i = 0; i < openNode.Count; i++)
            {
                if(currentNode.f > openNode[i].f)
                {
                    currentNode = openNode[i];
                }
            }

            if(currentNode.grid == destination)
            {
                path.Add(currentNode.grid);
                Node currGrid = currentNode;

                while(currGrid.parentNode != null)
                {
                    path.Add(currGrid.parentNode.grid);
                    currGrid = currGrid.parentNode;
                }

                path.Reverse();

                return path;
            }

            openNode.Remove(currentNode);
            closedNode.Add(currentNode);

            foreach(GridData grid in currentNode.grid._neighbours)
            {
                if(gridList.Contains(grid) == false) continue;

                if(closedNode.Contains(node[grid])) continue;

                float tempG = currentNode.g + Math.Abs(currentNode.grid._gridPos.x - grid._gridPos.x) + 
                Math.Abs(currentNode.grid._gridPos.y - grid._gridPos.y);

                if(tempG < node[grid].g)
                {
                    node[grid].parentNode = currentNode;
                    node[grid].g = tempG;
                    node[grid].h = Math.Abs(destination._gridPos.x - grid._gridPos.x) + 
                    Math.Abs(destination._gridPos.y - grid._gridPos.y);
                    node[grid].f = node[grid].g + node[grid].h;

                    if(openNode.Contains(node[grid]) == false)
                    {
                        openNode.Add(node[grid]);
                    }
                }
            }
        }

        return path;
    }

    public class Node
    {
        public GridData grid;
        public Node parentNode;
        public float g;
        public float h;
        public float f;

        public Node(GridData grid)
        {
            this.grid = grid;
            this.g = int.MaxValue;
            this.f = this.g + this.h;
            this.parentNode = null;
        }
    }
}

