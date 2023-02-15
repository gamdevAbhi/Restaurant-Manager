using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemObject item;
    public ItemObject.ItemType itemType;
    public GridManager gridManager;
    public ItemManager itemManager;
    public GridData[] occupiedGrid;
    public bool rotate;

    protected internal abstract void Initialize();

    protected internal virtual void Awake()
    {
        transform.parent = itemManager.transform;

        if(item != null)
        {
            SetItem();
        }
    }

    protected internal virtual void Update()
    {
        CheckRotation();
    }

    private void CheckRotation()
    {
        if(rotate)
        {
            transform.eulerAngles += new Vector3(0f, 90f, 0);
            rotate = false; 
        }
    }

    private void SetItem()
    {
        gameObject.name = item.itemName;
        itemType = item.itemType;

        transform.position = new Vector3(transform.position.x, item.objectTransform.height, transform.position.z);
        transform.localScale = item.objectTransform.scale;
        transform.eulerAngles = item.objectTransform.rotation;

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = item.itemMesh;

        Bounds bounds = meshFilter.sharedMesh.bounds;

        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = new Vector3(bounds.size.x * 0.9f, bounds.size.y * 0.9f, bounds.size.z);

        Renderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = item.material;
        renderer.material.SetColor("_Color", item.textureColor);

        Initialize();
    }

    protected internal bool CheckElement(GridData grid)
    {
        for (int i = 0; i < occupiedGrid.Length; i++)
        {
            if(occupiedGrid[i] == grid) return true;
        }
        
        return false;
    }

    private int FindElement(GridData grid)
    {
        for (int i = 0; i < occupiedGrid.Length; i++)
        {
            if(occupiedGrid[i] == grid) return i;
        }
        
        return -1;
    }

    private void OnCollisionStay(Collision other) 
    {
        try
        {
            GridData grid = other.gameObject.GetComponent<GridData>();

            if(CheckElement(grid) == false)
            {
                int index = FindElement(null);
                if(index == -1) return; 

                occupiedGrid[index] = grid;
            }
        }
        catch {}
    }

    private void OnCollisionExit(Collision other) 
    {
        try
        {
            GridData grid = other.gameObject.GetComponent<GridData>();

             if(CheckElement(grid) == true)
            {
                int index = FindElement(grid); 
                
                occupiedGrid[index] = null;
            }
        }
        catch {}
    }
}
