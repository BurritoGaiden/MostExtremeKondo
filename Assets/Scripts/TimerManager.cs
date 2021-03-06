﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float totalTime;
    public Text timerText;

    public delegate void TimeExpireEvent();
    public static event TimeExpireEvent TimeExpired;

    public float timeLeft;
    private bool stopTime = false;

    public AudioSource musicSource;

    private void Start()
    {
        timeLeft = totalTime;
    }

    private void Update()
    {
        if (timeLeft > 30)
        {
            musicSource.pitch = 1;
        }
        else if (timeLeft > 10)
        {
            musicSource.pitch = 1.5f;
        }
        else if (timeLeft > 0) {
            musicSource.pitch = 2f;
        }

        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;

            //Casting the float into a 0:00 format string
            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            timerText.text = niceTime;
        }

        else if (timeLeft < 0 && !stopTime)
        {
            Debug.Log("TIME HAS RUN OUT");
            TimeExpired();

            stopTime = true;
            //go to evaluation screen
        }
    }

}
