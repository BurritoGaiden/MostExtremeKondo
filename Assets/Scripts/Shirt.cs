using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shirt : Clothing
{
    public delegate void SolveEvent();
    public static event SolveEvent ShirtStepped;
    public static event SolveEvent StoreShirt;

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

    public void ActivateModel (int i)
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

            if (Input.GetAxis("Horizontal") > .9f)   //folds left side first
            {
                step++;
                Debug.Log("Success, move to step " + step);
                ShirtStepped();
                //load transition cloud puff
                ActivateModel(1);    //load next model, LEFT ORIENTATION
                //load success SFX puff
                leftFoldedFirst = true;
            }

            else if (Input.GetAxis("HorizontalTurn") < -.9f) //folds right side first
            {
                step++;
                Debug.Log("Success, move to step " + step);
                ShirtStepped();

                //load transition cloud puff
                ActivateModel(2);//load next model, RIGHT ORIENTATION
                //load success SFX puff
                leftFoldedFirst = false;
            }

            else if (Input.GetAxis("Horizontal") < -.9f ||
                     Input.GetAxis("Vertical") > .9f ||
                     Input.GetAxis("Vertical") < -.9f ||
                     Input.GetAxis("HorizontalTurn") > .9f ||
                     Input.GetAxis("VerticalTurn") > .9f ||
                     Input.GetAxis("VerticalTurn") < -.9f)  //USER INPUTS WRONG 
            {
                step = 0;   //return to beginning
                Debug.Log("Player Mistake, return to step " + step);
                

                //load transition cloud puff
                ActivateModel(step);    //load first model
                //load fail SFX buzz
            }

            StartCoroutine(InputDelay());
        }

        if (step == 1 && leftFoldedFirst && !delay)
        {

            if (Input.GetAxis("HorizontalTurn") < -.9f) //folds right side
            {
                step++;
                Debug.Log("Success, move to step " + step);
                ShirtStepped();
                //load transition cloud puff
                ActivateModel(3);    //load next model, RIGHT ORIENTATION
                //load success SFX puff
            }

            //USER INPUTS WRONG 
            else if (Input.GetAxis("Horizontal") < -.9f ||      //LEFT-OUT
                     Input.GetAxis("Horizontal") > .9f ||       //LEFT-IN
                     Input.GetAxis("Vertical") > .9f ||         //LEFT-UP
                     Input.GetAxis("Vertical") < -.9f ||        //LEFT-DOWN
                     Input.GetAxis("HorizontalTurn") > .9f ||   //RIGHT-OUT
                     Input.GetAxis("VerticalTurn") > .9f ||     //RIGHT-UP
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

        if (step == 1 && !leftFoldedFirst && !delay)
        {
            if (Input.GetAxis("Horizontal") > .9f)   //folds left side  LEFT-IN
            {
                step++;
                Debug.Log("Success, move to step " + step);
                ShirtStepped();
                //load transition cloud puff
                ActivateModel(4);    //load next model, LEFT ORIENTATION
                //load success SFX puff

            }

            //USER INPUTS WRONG 
            else if (Input.GetAxis("Horizontal") < -.9f ||      //LEFT-OUT
                     Input.GetAxis("HorizontalTurn") < -.9f ||  //RIGHT-IN
                     Input.GetAxis("Vertical") > .9f ||         //LEFT-UP
                     Input.GetAxis("Vertical") < -.9f ||        //LEFT-DOWN
                     Input.GetAxis("HorizontalTurn") > .9f ||   //RIGHT-OUT
                     Input.GetAxis("VerticalTurn") > .9f ||     //RIGHT-UP
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
                ShirtStepped();
                //load transition cloud puff
                ActivateModel(5);    //load next model, RIGHT ORIENTATION
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
            if (Input.GetButtonDown("Submit"))
            {
                //StoreShirt();   //is this necessary?
                return true;
            }
            
        }

        return false;

    }
}
