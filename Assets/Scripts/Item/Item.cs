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
            transform.GetChild(0).eulerAngles += new Vector3(0f, 90f, 0);
            rotate = false; 
            SetOccupiedGrid();
        }
    }

    private void SetItem()
    {
        gameObject.name = item.itemName;
        itemType = item.itemType;
        GameObject emptyObject = new GameObject();

        GameObject mesh = Instantiate(emptyObject, transform.position, Quaternion.identity, transform);
        mesh.name = "Mesh";
        mesh.transform.localPosition = item.objectTransform.position;
        mesh.transform.localScale = item.objectTransform.scale;
        mesh.transform.localEulerAngles = item.objectTransform.rotation;

        MeshFilter meshFilter = mesh.AddComponent<MeshFilter>() as MeshFilter;
        meshFilter.mesh = item.itemMesh;

        Bounds bounds = meshFilter.sharedMesh.bounds;

        BoxCollider collider = mesh.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = new Vector3(bounds.size.x * 0.9f, bounds.size.y * 0.9f, bounds.size.z);

        Renderer renderer = mesh.AddComponent<MeshRenderer>() as MeshRenderer;
        renderer.material = item.material;
        renderer.material.SetColor("_Color", item.textureColor);

        Rigidbody rigid = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;

        foreach(Vector3 grid in item.occupiedPos)
        {
            Instantiate(emptyObject, transform.position + grid, Quaternion.identity, mesh.transform);
        }

        SetOccupiedGrid();

        Destroy(emptyObject);
    }

    private void SetOccupiedGrid()
    {
        foreach(GridData grid in occupiedGrid)
        {
            grid._walkable = true;
        }

        occupiedGrid = new List<GridData>();

        foreach(Transform trans in transform.GetChild(0))
        {
            GridData gridData = gridManager.GetClosestGridFromWorld(trans.position);

            if(gridData != null)
            {
                if(occupiedGrid.Contains(gridData) == false)
                {
                    occupiedGrid.Add(gridData);
                    gridData._walkable = false;
                }
            }
        }
    }
}
