using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class GridData : MonoBehaviour
{
    [SerializeField] private string gridName;
    [SerializeField] private Vector2 worldPos;
    [SerializeField] private Vector2 gridPos;
    [SerializeField] private List<GridData> neighbours;
    [SerializeField] private bool walkable = true;

    public bool _walkable
    {
        get {return walkable; }
        set {walkable = value; }
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
        ChangeName();
        SetPos();
        #endif
    }

    protected internal void ChangeName()
    {
        this.gameObject.name = gridName + "_" + transform.GetSiblingIndex();
    }

    protected internal void SetPos()
    {
        worldPos = new Vector2(transform.position.x, transform.position.z);
        gridPos = new Vector2(worldPos.x / 1.0f, worldPos.y / 1.0f);
    }

    protected internal void SetNeighbours(List<GridData> _neighbours)
    {
        neighbours = _neighbours;
    }

    protected internal void Save()
    {
        PrefabUtility.RecordPrefabInstancePropertyModifications(this);
    }
}
