using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shirt : Clothing
{
    public delegate void SolveEvent();
    public static event SolveEvent ClothingStepped;

    private int step = 0;
    private bool leftFoldedFirst;
    private bool delay = false;

    public GameObject[] models;
    public float delayTime = .1f;
    public AudioClip successSFX;
    public AudioClip mistakeSFX;

    public bool[] values;

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


    float yaw;
    Vector3 currentRotation;
    Vector3 rotationSmoothVelocity;
    public float rotationSmoothTime = .12f;

    public float timeSpentExamining;
    public float diff0;

    //Check for if the player has performed an input this frame
    //return true if so, false if not
    public override bool CheckSolveConditions()
    {
        //rotate the object
        //if (Input.GetKey(KeyCode.T))
        //    yaw += 1 * 3;
        //if (Input.GetKey(KeyCode.Y))
        //    yaw += -1 * 3;
        //currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        //transform.eulerAngles = currentRotation;

        //examine
        //get the amount of values on this object
        //for each *1* we eclipse when examining, show a value in the text
        if (Input.GetKey(KeyCode.I))
        {
            timeSpentExamining += Time.deltaTime;
        }

        if (timeSpentExamining >= 1) {
            diff0++;
            timeSpentExamining = 0;
        }

        //telling which values we've seen
        //for (int i = 0; i < diff0; i++) {
        //    print(values[i]);
        //}


        //if the delay has elapsed
        if (!delay)
        {
            //if we're on step 0
            if (step == 0)
            {
                if (Input.GetAxis("Horizontal") > .9f || Input.GetKeyDown(KeyCode.U))   //folds left side first
                {
                    step++;
                    ClothingStepped();
                    //load transition cloud puff
                    ActivateModel(1);    //load next model, LEFT ORIENTATION
                    //load success SFX puff
                    leftFoldedFirst = true;
                    StartCoroutine(InputDelay());
                }

                else if (Input.GetAxis("HorizontalTurn") < -.9f || Input.GetKeyDown(KeyCode.U)) //folds right side first
                {
                    step++;
                    ClothingStepped();
                    //load transition cloud puff
                    ActivateModel(2);//load next model, RIGHT ORIENTATION
                                     //load success SFX puff
                    leftFoldedFirst = false;
                    StartCoroutine(InputDelay());
                }

                else if (Input.GetAxis("Horizontal") < -.9f ||
                         Input.GetAxis("Vertical") > .9f ||
                         Input.GetAxis("Vertical") < -.9f ||
                         Input.GetAxis("HorizontalTurn") > .9f ||
                         Input.GetAxis("VerticalTurn") > .9f ||
                         Input.GetAxis("VerticalTurn") < -.9f)  //USER INPUTS WRONG 
                {
                    step = 0;   //return to beginning
                                //Debug.Log("Player Mistake, return to step " + step);


                    //load transition cloud puff
                    ActivateModel(step);    //load first model
                                            //load fail SFX buzz
                    StartCoroutine(InputDelay());
                }
            }
            else if (step == 1)
            {
                if (leftFoldedFirst)
                {
                    if (Input.GetAxis("HorizontalTurn") < -.9f || Input.GetKeyDown(KeyCode.U)) //folds right side
                    {
                        step++;
                        //Debug.Log("Success, move to step " + step);
                        ClothingStepped();
                        //load transition cloud puff
                        ActivateModel(3);    //load next model, RIGHT ORIENTATION
                                             //load success SFX puff
                        StartCoroutine(InputDelay());
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
                                    //Debug.Log("Player Mistake, return to step " + step);
                                    //load transition cloud puff
                        ActivateModel(step);    //load first model
                                                //load fail SFX buzz
                        StartCoroutine(InputDelay());
                    }
                }
                else if (!leftFoldedFirst)
                {
                    if (Input.GetAxis("Horizontal") > .9f || Input.GetKeyDown(KeyCode.U))   //folds left side  LEFT-IN
                    {
                        step++;
                        //Debug.Log("Success, move to step " + step);
                        ClothingStepped();
                        //load transition cloud puff
                        ActivateModel(4);    //load next model, LEFT ORIENTATION
                                             //load success SFX puff
                        StartCoroutine(InputDelay());
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
                                    //Debug.Log("Player Mistake, return to step " + step);
                                    //load transition cloud puff
                        ActivateModel(step);    //load first model
                                                //load fail SFX buzz
                        StartCoroutine(InputDelay());
                    }
                }
            }
            else if (step == 2) {
                //Debug.Log(Input.GetAxis("Vertical"));
                if ((Input.GetAxis("Vertical") < -.9f && Input.GetAxis("VerticalTurn") < -.9f) || Input.GetKeyDown(KeyCode.U))   //DOUBLE-DOWN
                {
                    step++;
                    //Debug.Log("DOUBLE DOWN");
                    ClothingStepped();
                    //load transition cloud puff
                    ActivateModel(5);    //load next model, RIGHT ORIENTATION
                                         //load success SFX puff
                    StartCoroutine(InputDelay());
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
                                //Debug.Log("Player Mistake, return to step " + step);
                                //load transition cloud puff
                    ActivateModel(step);    //load first model
                                            //load fail SFX buzz
                    StartCoroutine(InputDelay());
                }              
            }
            else if (step == 3)
            {
                if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.U))
                {
                    //StoreShirt();   //is this necessary?
                    return true;
                }
            }
        }
        else {
            print("still in delay");
        }
        return false;
    }
}
