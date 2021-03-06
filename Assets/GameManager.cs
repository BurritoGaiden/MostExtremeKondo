﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioSource jetSetSource;
    public AudioSource crowdSource;

    public GameObject goodBlip;
    public GameObject theCamera;

    public GameObject conLeft;
    public GameObject conRight;

    public GameObject textBox;
    public GameObject textText;
    public GameObject nameText;

    public PileManager pMan;

    public void Start()
    {
        Shirt.ClothingStepped += StepProgress;
        Pants.ClothingStepped += StepProgress;
        Socks.ClothingStepped += StepProgress;
        Book.ClothingStepped += StepProgress;  
    }


    //    private void onlevel
    //      currentObject = GameObject.Find("CurrentObject");
    //    hand = GameObject.Find("Hand");
    //}

    private void OnEnable()
    {
        currentObject = GameObject.Find("CurrentObject");
        hand = GameObject.Find("Hand");
    }

    private void Awake()
    {
        currentObject = GameObject.Find("CurrentObject");
        hand = GameObject.Find("Hand");
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
        //if(currentObject != null)
        currentObject.GetComponent<Animator>().Play("Object_FlyOut");
        //GameObject.Find("Hand").GetComponent<Animator>().Play("Hand_Slap");
        GiveRandomPitch(1f, 2.5f);
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        jetSetSource.pitch = 1;
        jetSetSource.PlayOneShot(audioClips[7]);
        //crowdSource.PlayOneShot(audioClips[12]);
        goodBlip.GetComponent<Animator>().Play("Good_Blip_Flash");
        thisAnim = ObjectAnimState.ObjOut;
        currentWaitTime = waitTime;

        textBox.SetActive(false);
        textText.GetComponent<Text>().text = "";
    }

    public void ThrowAwayItem()
    {
        currentObject.GetComponent<Animator>().Play("Object_KickOut");
        hand.GetComponent<Animator>().Play("Hand_Punch");
        GiveRandomPitch(1f, 2.5f);
        GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
        thisAnim = ObjectAnimState.ObjOut;
        currentWaitTime = waitTime;

        textBox.SetActive(false);
        textText.GetComponent<Text>().text = "";
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

        //if(currentObject.)
        //bring up cons for the first actionconLeft = 
        if (pMan.currentItem.backStory != "") {
            textBox.SetActive(true);
            textText.GetComponent<Text>().text = pMan.currentItem.backStory;
            nameText.GetComponent<Text>().text = "";
        }
    }

    public void GiveRandomPitch(float min, float max) {
        GetComponent<AudioSource>().pitch = Random.Range(min, max);
    }

    //public void GetAllReferences() {
    //        public GameObject theCamera;
   //hand = GameObject.Find("Hand");
    ///    theCamera
    //}

    public void StepProgress() {
   // GetAllReferences();

        int rRand = Random.Range(0, 4);
        print(rRand);
        if (rRand == 0) {
            //GameObject.Find("Hand").GetComponent<Animator>().Play("Hand_Fold");
            hand.GetComponent<Animator>().Play("Hand_Fold");
            theCamera.GetComponent<Animator>().Play("Camera_LeftMovement");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
            jetSetSource.pitch = .6f;
            jetSetSource.PlayOneShot(audioClips[7]);
        }
        else if (rRand == 1) {
            //GameObject.Find("Hand").GetComponent<Animator>().Play("Hand_Fold1");
            hand.GetComponent<Animator>().Play("Hand_Fold1");
            theCamera.GetComponent<Animator>().Play("Camera_UpMovement");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
            jetSetSource.pitch = .7f;
            jetSetSource.PlayOneShot(audioClips[7]);
        }
        else if (rRand == 2) {
            //GameObject.Find("Hand").GetComponent<Animator>().Play("Hand_Fold2");
            hand.GetComponent<Animator>().Play("Hand_Fold2");
            theCamera.GetComponent<Animator>().Play("Camera_DownMovement");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[5]);
            jetSetSource.pitch = .9f;
            jetSetSource.PlayOneShot(audioClips[7]);
        }
        else if (rRand == 3) {
            //GameObject.Find("Hand").GetComponent<Animator>().Play("Hand_Fold3");
            hand.GetComponent<Animator>().Play("Hand_Fold3");
            theCamera.GetComponent<Animator>().Play("Camera_RightMovement");
            GiveRandomPitch(1f, 2.5f);
            GetComponent<AudioSource>().PlayOneShot(audioClips[6]);
            jetSetSource.pitch = .8f;
            jetSetSource.PlayOneShot(audioClips[7]);
            //currentWaitTime = waitTime;
        }

        animeVignette.GetComponent<Animator>().Play("Anime_Vignette_Off");
        animeVignette.GetComponent<Animator>().Play("Anime_Vignette_Flashing");
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
