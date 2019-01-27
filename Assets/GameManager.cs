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

    public AudioClip[] audioClips;
    public AudioSource musicSource;
    public GameObject slapEffect;

    public void Start()
    {
        Shirt.ClothingStepped += StepProgress;
        Pants.ClothingStepped += StepProgress;
        Socks.ClothingStepped += StepProgress;
        Book.ClothingStepped += StepProgress;
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
        hand.GetComponent<Animator>().Play("Hand_Slap");
        GiveRandomPitch(1f, 2.5f);
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        thisAnim = ObjectAnimState.ObjOut;
        currentWaitTime = waitTime;
    }

    public void ThrowAwayItem()
    {
        currentObject.GetComponent<Animator>().Play("Object_KickOut");
        hand.GetComponent<Animator>().Play("Hand_Punch");
        GiveRandomPitch(1f, 2.5f);
        GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
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
        GiveRandomPitch(1f, 2.5f);
        GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
        thisAnim = ObjectAnimState.ObjIn;
        currentWaitTime = waitTime;
    }

    public void GiveRandomPitch(float min, float max) {
        GetComponent<AudioSource>().pitch = Random.Range(min, max);
    }

    public void StepProgress() {
        int rRand = Random.Range(0, 4);
        print(rRand);
        if (rRand == 0) { 
            hand.GetComponent<Animator>().Play("Hand_Fold");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
        }
        else if (rRand == 1) {
            hand.GetComponent<Animator>().Play("Hand_Fold1");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
        }
        else if (rRand == 2) {
            hand.GetComponent<Animator>().Play("Hand_Fold2");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[5]);
        }
        else if (rRand == 3) {
            hand.GetComponent<Animator>().Play("Hand_Fold3");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[6]);
            //currentWaitTime = waitTime;
        }

        slapEffect.GetComponent<Animator>().Play("Slap_Effect_Blip");
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
