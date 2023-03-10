using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class GridData : MonoBehaviour
{
    private static string gridName;
    [SerializeField] private Vector2 gridPos;
    [SerializeField] private List<GridData> neighbours;
    [SerializeField] private GameObject occupiedObject;

    public GridData(string name)
    {
        gridName = name;
    }
    
    public GameObject _occupiedObject
    {
        get { return occupiedObject; }
    }
    
    public Vector2 _gridPos
    {
        get { return gridPos; }
    }

    public List<GridData> _neighbours
    {
        get { return neighbours; }
    }

    private void Awake()
    {
        #if UNITY_EDITOR
        if(Application.isPlaying == false)
        {
            if(GetComponents(typeof(GridData)).Length > 1)
            {
                DestroyImmediate(this);
            }

            transform.parent.gameObject.SendMessage("AddGrid", this);
            Initialize();
        }
        #endif
    }

    protected internal virtual void Initialize()
    {
        #if UNITY_EDITOR
        SetPos();
        ChangeName();
        #endif
    }

    private void ChangeName()
    {
        this.gameObject.name = gridName + " " + "(" + gridPos.x + ", " + gridPos.y + ")";
    }

    private void SetPos()
    {
        Vector2 worldPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 worldToGrid = transform.parent.GetComponent<GridManager>()._worldToGrid;
        gridPos = new Vector2(worldPos.x / worldToGrid.x, worldPos.y / worldToGrid.y);
    }

    protected internal void SetNeighbours(List<GridData> _neighbours)
    {
        neighbours = _neighbours;
    }

    protected internal void Save()
    {
        PrefabUtility.RecordPrefabInstancePropertyModifications(this);
    }

    private void OnCollisionStay(Collision other) 
    {
        if(occupiedObject == null)
        {
            occupiedObject = other.gameObject;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(occupiedObject == other.gameObject)
        {
            occupiedObject = null;
        }
    }
}
