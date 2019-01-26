using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public float sparkJoy;
    public float sparkJoyDeviation;
    public Text backStory;
    public GameObject preSolveModel;
    public GameObject postSolveModel;

    public virtual bool CheckSolveConditions()
    {
        //parameters and/or button inputs that need to be satisfied to solve object, called in Update()
        return false;
    }

    public virtual void Solve()
    {
        Debug.LogError("SOLVED!");
        //actions to execute on successful solve of item
    }

    void Update()
    {
        if (CheckSolveConditions())
        {
            Solve();
        }
    }
}
