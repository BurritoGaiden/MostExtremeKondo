using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileManager : MonoBehaviour
{
    public GameManager gMan;
    public Item[] allItems;

    private Item currentItem;
    private int incrementer = 0;

    private void Start()
    {
        Item.ItemSolved += DoOnSolve; 

        allItems = GetComponentsInChildren<Item>(true);
        currentItem = allItems[0];
        currentItem.gameObject.SetActive(true);

        Debug.Log("Item Pile Size: " + allItems.Length);
        Debug.Log("The Starting Item is: " + currentItem);

        gMan.BringInItem();
    }

    void DoOnSolve()
    {
        gMan.StoreItem();
        LoadItem(incrementer);
            
        incrementer++;
    }

    public void LoadItem(int i)
    {
        currentItem.gameObject.SetActive(false);
        currentItem = allItems[i];
        currentItem.gameObject.SetActive(true);
        Debug.Log("The New Current Item is " + currentItem);
    }
}