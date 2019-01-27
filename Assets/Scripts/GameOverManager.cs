using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PileManager pMan;
    public bool canRestart = false;
    public int sceneIndexToRestart = 0;

    private GameManager gMan;
    private HealthManager hMan;
    private TimerManager tMan;


    void Start()
    {
        HealthManager.HPExpired += DoOnLoss;
        TimerManager.TimeExpired += DoOnTimeUp;
        PileManager.pileExhausted += DoOnLoss;

        gMan = GetComponent<GameManager>();
        hMan = GetComponent<HealthManager>();
        tMan = GetComponent<TimerManager>();

    }

    private void Update()
    {
        if (canRestart)
        {
            if (Input.GetButtonDown("StartButton"))     //check if can restart
            {
                SceneManager.LoadScene(sceneIndexToRestart, LoadSceneMode.Single);
            }
        }
    }

    public void DoOnTimeUp()
    {
        Debug.Log("TIME'S UP! LET'S SEE HOW YOU DID!");

        //EVALUATE IF WON OR LOSS
        if (hMan.isAlive())     //checks remaining HP
        {
            if (pMan.CheckPileRequirements())           //compare pile requirements
            {
                //WIN
                DoOnWin();
            }
            //LOSS
            DoOnLoss();
        }
        DoOnLoss();
    }

    public void DoOnWin()
    {
        tMan.enabled = false;               //Pause Timer, probably disable Timer script
        pMan.SetCurrentItemActive(false);   //Disable Folding, either by disabling the Item GameObject or script.

        canRestart = true;

        Debug.Log("YOU WIN! THANK YOU FOR PLAYING, PRESS 'START' to CONTINUE");
    }

    public void DoOnLoss()
    {
        tMan.enabled = false;               //Pause Timer, probably disable Timer script
        pMan.SetCurrentItemActive(false);   //Disable Folding, either by disabling the Item GameObject or script.

        canRestart = true;

        Debug.Log("I'M SORRY :'( YOU DIED OF UNHAPPINESS. PLEASE TRY AGaIN. PRESS 'START' to CONTINUE");
    }

}
