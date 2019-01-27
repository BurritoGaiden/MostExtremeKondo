﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pants : Clothing
{
    private int step = 0;
    private bool leftFoldedFirst;
    private bool delay = false;

    public GameObject[] models;
    public float delayTime = .1f;
    public AudioClip successSFX;
    public AudioClip mistakeSFX;

    private void Start()
    {
        ActivateModel(step);
    }

    public IEnumerator InputDelay()
    {
        delay = true;
        yield return new WaitForSecondsRealtime(delayTime);
        delay = false;
    }

    public void ActivateModel(int i)
    {
        DeactivateAllModels();
        models[i].SetActive(true);
    }

    public void DeactivateAllModels()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }
    }

    public override bool CheckSolveConditions()
    {
        if (step == 0 && !delay)
        {

            if (Input.GetAxis("Horizontal") > .9f)   //folds left side first        LEFT-IN
            {
                step++;
                Debug.Log("Success, move to step " + step);
                //load transition cloud puff
                ActivateModel(step);    //load next model, LEFT ORIENTATION
                //load success SFX puff

            }

            else if (Input.GetAxis("HorizontalTurn") < -.9f) //folds right side first   RIGHT-IN
            {
                step++;
                Debug.Log("Success, move to step " + step);

                //load transition cloud puff
                ActivateModel(step);//load next model, RIGHT ORIENTATION
                //load success SFX puff

            }

            //USER INPUTS WRONG 
            else if (Input.GetAxis("Horizontal") < -.9f ||          //LEFT-OUT
                     Input.GetAxis("Vertical") > .9f ||             //LEFT-UP
                     Input.GetAxis("Vertical") < -.9f ||            //LEFT-DOWN
                     Input.GetAxis("HorizontalTurn") > .9f ||       //RIGHT-OUT
                     Input.GetAxis("VerticalTurn") > .9f ||         //RIGHT-UP
                     Input.GetAxis("VerticalTurn") < -.9f)          //RIGHT-DOWN
            {
                step = 0;   //return to beginning
                Debug.Log("Player Mistake, return to step " + step);

                //load transition cloud puff
                ActivateModel(step);    //load first model
                //load fail SFX buzz
            }

            StartCoroutine(InputDelay());
        }

        if (step == 1 && !delay)
        {

            if (Input.GetAxis("VerticalTurn") > .9f && Input.GetAxis("Vertical") > .9f) //DOUBLE-UP
            {
                step++;
                Debug.Log("Success, move to step " + step);
                //load transition cloud puff
                ActivateModel(step);    //load next model, RIGHT ORIENTATION
                //load success SFX puff
            }

            //USER INPUTS WRONG 
            else if (Input.GetAxis("Horizontal") < -.9f ||      //LEFT-OUT
                     Input.GetAxis("Horizontal") > .9f ||       //LEFT-IN
                     //Input.GetAxis("Vertical") > .9f ||         //LEFT-UP
                     Input.GetAxis("Vertical") < -.9f ||        //LEFT-DOWN
                     Input.GetAxis("HorizontalTurn") > .9f ||   //RIGHT-OUT
                     Input.GetAxis("HorizontalTurn") < -.9f ||  //RIGHT-IN
                     //Input.GetAxis("VerticalTurn") > .9f ||     //RIGHT-UP
                     Input.GetAxis("VerticalTurn") < -.9f)      //RIGHT-DOWN
            {
                step = 0;   //return to beginning
                Debug.Log("Player Mistake, return to step " + step);
                //load transition cloud puff
                ActivateModel(step);    //load first model
                //load fail SFX buzz
            }

            StartCoroutine(InputDelay());
        }

        if (step == 2 && !delay)
        {
            //Debug.Log(Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") < -.9f && Input.GetAxis("VerticalTurn") < -.9f)   //DOUBLE-DOWN
            {
                step++;
                Debug.Log("DOUBLE DOWN");
                //load transition cloud puff
                ActivateModel(step);    //load next model, RIGHT ORIENTATION
                //load success SFX puff
            }

            //USER INPUTS WRONG 
            else if (Input.GetAxis("Horizontal") < -.9f ||      //LEFT-OUT
                     Input.GetAxis("Horizontal") > .9f ||       //LEFT-IN
                     Input.GetAxis("Vertical") > .9f ||         //LEFT-UP
                     //Input.GetAxis("Vertical") < -.9f ||        //LEFT-DOWN
                     Input.GetAxis("HorizontalTurn") > .9f ||   //RIGHT-OUT
                     Input.GetAxis("HorizontalTurn") < -.9f ||  //RIGHT-IN
                     Input.GetAxis("VerticalTurn") > .9f)       //RIGHT-UP
                     //Input.GetAxis("VerticalTurn") < -.9f)      //RIGHT-DOWN
            {
                step = 0;   //return to beginning
                Debug.Log("Player Mistake, return to step " + step);
                //load transition cloud puff
                ActivateModel(step);    //load first model
                //load fail SFX buzz
            }

            StartCoroutine(InputDelay());
        }

        if (step == 3 && !delay)
        {
            return true;
        }

        return false;

    }


}