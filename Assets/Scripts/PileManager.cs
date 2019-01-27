using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PileManager : MonoBehaviour
{
    public delegate void PileExpireEvent();
    public static event PileExpireEvent pileExhausted;

    public GameManager gMan;
    public HealthManager hMan;
    public TimerManager tMan;
    public Item[] allItems;

    public Item currentItem;
    private int incrementer = 0;
    private List<Item> storedItems;
    private List<Item> thrownItems;

    public int successfullySortedObjects;
    public int sortedObjectSuccessThreshold;
    public int remainingUnsortedObjects;

    public Text remainingObjectText;
    public Text requiredObjectText;


    private void Start()
    {
        //subscribe to relevant items
        Item.ItemSolved += DoOnSolve;
        Item.ItemThrownAway += DoOnThrowAway;
        HealthManager.HPExpired += DoOnHealthFail;

        //get all items, get the current item, turn the current item on
        allItems = GetComponentsInChildren<Item>(true);
        currentItem = allItems[0];
        currentItem.gameObject.SetActive(true);

        Debug.Log("Item Pile Size: " + allItems.Length);
        Debug.Log("The Starting Item is: " + currentItem);

        //bring in the item
        gMan.BringInItem();

        remainingUnsortedObjects = allItems.Length;
        remainingObjectText.text = remainingUnsortedObjects.ToString();
        requiredObjectText.text = sortedObjectSuccessThreshold.ToString();
    }

    //this is called when the current item is solved
    void DoOnSolve()
    {
        gMan.StoreItem();   //animation
        hMan.DealDamage(currentItem.sparkJoy);  //deal damage to health manager

        //storedItems.Add(currentItem);       //this adds item to all stored items at the end.

        remainingUnsortedObjects--;
        sortedObjectSuccessThreshold--;
        remainingObjectText.text = remainingUnsortedObjects.ToString();
        requiredObjectText.text = sortedObjectSuccessThreshold.ToString();

        incrementer++;


        //CHECK IF THERE'S ANOTHER ITEM
        if (allItems[incrementer] != null)
        {
            LoadItem(incrementer);
        }

        else
        {
            pileExhausted();
        }
        
    }

    //this is called when the current item is thrown away
    void DoOnThrowAway()
    {
        gMan.ThrowAwayItem();
        hMan.DealDamage(currentItem.sparkJoy);

        //thrownItems.Add(currentItem);       //this tracks all thrown away Items

        remainingUnsortedObjects--;
        remainingObjectText.text = remainingUnsortedObjects.ToString();

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

    private void Update()
    {
        //CheckForSuccess();    
    }

    public bool CheckForSuccess() {
        if (sortedObjectSuccessThreshold <= 0) {
            print("success!");
            return true;
        }
        return false;
    }

    public void DoOnSuccess() {

    }

    public void DoOnHealthFail() {
        //if(hMan.he)
        print("ran out of health");
    }

    public void SetCurrentItemActive(bool b)
    {
        currentItem.enabled = b;
    }

    public bool CheckPileRequirements()
    {
        return sortedObjectSuccessThreshold <= 0;
    }
}