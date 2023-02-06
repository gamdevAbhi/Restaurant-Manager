using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    [SerializeField] protected internal Vector2[] controlGridItem;
    [SerializeField] protected internal Vector2[] controlGridHuman;
    [SerializeField] protected internal Item[] controlItem;
    [SerializeField] protected internal Human[] controlHuman;
    [SerializeField] protected internal GridManager gridManager;

    protected internal abstract void Initialize();
    protected internal abstract void CheckControlItem();
    //protected internal abstract void CheckControlHuman();
}
