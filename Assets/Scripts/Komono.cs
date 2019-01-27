using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Komono : Item
{



    private void Awake()
    {
        //load Text Box
    }

    public override bool CheckSolveConditions()
    {
        if (Input.GetButtonDown("Submit"))
        {
            return true;
        }

        return false;
    }
}
