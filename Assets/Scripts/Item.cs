using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public delegate void SolveEvent();
    public static event SolveEvent ItemSolved;

    private float thankYouPressDuration = 0f;

    public bool solved = false;
    public float sparkJoy;
    public float sparkJoyDeviation;
    public Text backStory;
    public float thankYouMinHold = 3f;
    //public GameObject preSolveModel;
    //public GameObject postSolveModel;



    public virtual bool CheckSolveConditions()
    {
        //parameters and/or button inputs that need to be satisfied to solve object, called in Update()
        return false;
    }

    public virtual void Solve()
    {
        solved = true;
        Debug.LogError("SOLVED!");
        ItemSolved();
        //actions to execute on successful solve of item
    }

    public void ThrownAway()
    {

    }

    public void Thanked()
    {

    }

    public void CheckThankYou()     //INCOMPLETE
    {
        if (Input.GetButton("ThankYou"))
        {
            thankYouPressDuration += Time.deltaTime;
            Debug.Log("Thank You button pressed");

            if (thankYouPressDuration >= thankYouMinHold)
            {
                Thanked();
            }
        }

        else
        {
            thankYouPressDuration = 0f;
        }

    }

    void Update()
    {
        CheckThankYou();

        if (CheckSolveConditions())
        {
            Solve();
        }
    }
}
