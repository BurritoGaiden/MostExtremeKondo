using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject currentObject;
    public GameObject currentObjectModel;
    public GameObject hand;
    public ObjectAnimState thisAnim;
    public GameObject animeVignette;
    public float waitTime;
    public float currentWaitTime;

    public Transform[] rotations;
    public Transform from;
    public Transform to;
    public float speed;

    public float remainingTime;

    public void Start()
    {
        Shirt.ShirtStepped += StepProgress;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisAnim == ObjectAnimState.ObjOut) {
            if (currentWaitTime <= 0) {
                BringInItem();
            }
        }      
        else if (thisAnim == ObjectAnimState.ObjIn)
        {
            if (currentWaitTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StoreItem();
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    ThrowAwayItem();
                }
                else if (Input.GetKeyDown(KeyCode.J)) {     //=======I commented this section out -ALEX
                    
                }
            }
        }
        RotateModel();
        DecrementCurrentWaitTime();
    }

    //======ALEX
    public void StoreItem()
    {
        currentObject.GetComponent<Animator>().Play("Object_FlyOut");
        hand.GetComponent<Animator>().SetTrigger("Slap");
        thisAnim = ObjectAnimState.ObjOut;
        currentWaitTime = waitTime;
    }

    public void ThrowAwayItem()
    {
        currentObject.GetComponent<Animator>().Play("Object_KickOut");
        hand.GetComponent<Animator>().SetTrigger("Punch");
        thisAnim = ObjectAnimState.ObjOut;
        currentWaitTime = waitTime;
    }

    public void BringInItem() {
        animeVignette.GetComponent<Animator>().SetTrigger("StopFlash");
        //give object model a rotation to rotate from and to
        from = rotations[Random.Range(0, rotations.Length)];
        currentObjectModel.transform.rotation = from.rotation;
        to = rotations[Random.Range(0, rotations.Length)];

        currentObject.GetComponent<Animator>().Play("Object_FlyIn");
        thisAnim = ObjectAnimState.ObjIn;
        currentWaitTime = waitTime;
    }

    public void StepProgress() {
        int rRand = Random.Range(0, 4);
        print(rRand);
        if (rRand == 0)
            hand.GetComponent<Animator>().SetTrigger("Fold");
        else if (rRand == 1)
            hand.GetComponent<Animator>().SetTrigger("Fold1");
        else if (rRand == 2)
            hand.GetComponent<Animator>().SetTrigger("Fold2");
        else if (rRand == 3)
            hand.GetComponent<Animator>().SetTrigger("Fold3");
        //currentWaitTime = waitTime;

        //give object model a rotation to rotate to
        to = rotations[Random.Range(0, rotations.Length)];
    }

    //======ALEX

    void RotateModel() {
        currentObjectModel.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);
    }

    void DecrementCurrentWaitTime() {
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }
    }
}

public enum ObjectAnimState {ObjOut,ObjIn }
