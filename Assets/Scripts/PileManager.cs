﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileManager : MonoBehaviour
{
    public GameManager gMan;
    public HealthManager hMan;
    public Item[] allItems;

    private Item currentItem;
    private int incrementer = 0;

    private void Start()
    {
        Item.ItemSolved += DoOnSolve;
        Item.ItemThrownAway += DoOnThrowAway;

        allItems = GetComponentsInChildren<Item>(true);
        currentItem = allItems[0];
        currentItem.gameObject.SetActive(true);

        Debug.Log("Item Pile Size: " + allItems.Length);
        Debug.Log("The Starting Item is: " + currentItem);

        gMan.BringInItem();
    }

    void DoOnSolve()
    {
        gMan.StoreItem();   //animation
        hMan.DealDamage(currentItem.sparkJoy);  //deal damage to health manager
        
            
        incrementer++;

        LoadItem(incrementer);
    }

    void DoOnThrowAway()
    {
        gMan.ThrowAwayItem();
        hMan.DealDamage(currentItem.sparkJoy);

        incrementer++;
        LoadItem(incrementer);
    }

    public void LoadItem(int i)
    {
        currentItem.gameObject.SetActive(false);
        currentItem = allItems[i];
        currentItem.gameObject.SetActive(true);
        Debug.Log("The New Current Item is " + currentItem);
    }
}