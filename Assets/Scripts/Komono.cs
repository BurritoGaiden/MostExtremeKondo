using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Komono : Item
{
    public bool canSubmit;
    public float waitTime;
    private float waitThreshold = 3f;


    float yaw;
    Vector3 currentRotation;
    Vector3 rotationSmoothVelocity;
    public float rotationSmoothTime = .12f;

    private void Awake()
    {
        //load Text Box
    }

    public override bool CheckSolveConditions()
    {
        //rotate the object
        if ((Input.GetButton("Submit") || Input.GetKey(KeyCode.I)) && waitTime < waitThreshold)
        {
            waitTime += Time.deltaTime;
            yaw += 1 * 3;
        }
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;



        if (Input.GetKey(KeyCode.I))
        {
           
        }

        if (waitTime >= waitThreshold)
        {
            return true;
            canSubmit = true;
        }



        if (canSubmit)
        {
            if (Input.GetButtonDown("Submit"))
            {
                return true;
            }
        }

        return false;
    }
}
