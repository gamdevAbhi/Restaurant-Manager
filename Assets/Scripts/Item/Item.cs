using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public ItemObject.ItemType itemType;
    public GridManager gridManager;
    public ItemManager itemManager;
    public List<GridData> occupiedGrid;
    public bool rotate;

    private void Awake()
    {
        transform.parent = itemManager.transform;

        if(item != null)
        {
            SetItem();
        }
    }

    private void Update()
    {
        if(rotate)
        {
            transform.eulerAngles += new Vector3(0f, 90f, 0);
            rotate = false; 
            SetOccupiedGrid();
            gameObject.GetComponent<ItemScript>().CheckControlItem();
        }
    }

    private void SetItem()
    {
        gameObject.name = item.itemName;
        itemType = item.itemType;
        GameObject emptyObject = new GameObject();

        transform.position = new Vector3(transform.position.x, item.objectTransform.height, transform.position.z);
        transform.localScale = item.objectTransform.scale;
        transform.eulerAngles = item.objectTransform.rotation;

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>() as MeshFilter;
        meshFilter.mesh = item.itemMesh;

        Bounds bounds = meshFilter.sharedMesh.bounds;

        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = new Vector3(bounds.size.x * 0.9f, bounds.size.y * 0.9f, bounds.size.z);

        Renderer renderer = gameObject.AddComponent<MeshRenderer>() as MeshRenderer;
        renderer.material = item.material;
        renderer.material.SetColor("_Color", item.textureColor);

        foreach(Vector2 grid in item.occupiedGridDistance)
        {
            Instantiate(emptyObject, transform.position + new Vector3(grid.x * gridManager._worldToGrid.x, 
            0f, grid.y * gridManager._worldToGrid.y), Quaternion.identity, transform);
        }

        SetOccupiedGrid();
        
        if(item.itemScriptType != "")
        {
            System.Type type = System.Type.GetType(item.itemScriptType);
            ItemScript itemScript = gameObject.AddComponent(type) as ItemScript;
            itemScript.gridManager = gridManager;
            itemScript.Initialize();
            itemScript.CheckControlItem();
        }

        Destroy(emptyObject);
    }

    private void SetOccupiedGrid()
    {
        foreach(GridData grid in occupiedGrid)
        {
            grid._occupiedObject = null;
        }

        occupiedGrid = new List<GridData>();

        foreach(Transform trans in transform)
        {
            GridData gridData = gridManager.GetClosestGridFromWorld(trans.position);

            if(gridData != null)
            {
                if(occupiedGrid.Contains(gridData) == false)
                {
                    occupiedGrid.Add(gridData);
                    gridData._occupiedObject = gameObject;
                }
            }
        }
    }
}
