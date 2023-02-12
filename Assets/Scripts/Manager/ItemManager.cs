using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemList[] itemList;

    private void Awake()
    {
        string[] itemType = System.Enum.GetNames(typeof(ItemObject.ItemType));

        itemList = new ItemList[itemType.Length];

        for(int i = 0; i < itemList.Length; i++)
        {
            itemList[i] = new ItemList(System.Enum.Parse<ItemObject.ItemType>(itemType[i]));
        }
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            Item item = child.GetComponent<Item>();

            List<Item> thisItemList = itemList[FindItemTypeToItems(item.itemType)].items;

            if(thisItemList.Contains(item) == false)
            {
                thisItemList.Add(item);
            }

            itemList[FindItemTypeToItems(item.itemType)].items = thisItemList;
        }
    }

    private int FindItemTypeToItems(ItemObject.ItemType itemType)
    {
        for(int i = 0; i < itemList.Length; i++)
        {
            if(itemList[i].itemType == itemType) return i;
        }

        return -1;
    }

    protected internal List<Item> FindItem(ItemObject.ItemType itemType)
    {
        return itemList[FindItemTypeToItems(itemType)].items;
    }
}

[System.Serializable]
public class ItemList
{
    public ItemObject.ItemType itemType;
    public List<Item> items;

    public ItemList(ItemObject.ItemType itemType)
    {
        this.itemType = itemType;
        this.items = new List<Item>();
    }
}
