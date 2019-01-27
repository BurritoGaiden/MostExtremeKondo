using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public PileManager pMan;
    public bool canRestart = false;
    public int sceneIndexToRestart = 0;

    private GameManager gMan;
    private HealthManager hMan;
    private TimerManager tMan;

    public Image Scoreboard;
    public Image ScoreFader;
    public Text scoreReadout;

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
            if (Input.GetButtonDown("StartButton") || Input.GetKeyDown(KeyCode.U))     //check if can restart
            {
                SceneManager.LoadScene(sceneIndexToRestart, LoadSceneMode.Single);
                //Application.LoadLevel(Application.loadedLevel);
            }
        }

        //if (GameOverCall()) {

        //s}
        if (pMan.CheckForSuccess() && runOnce == false)
        {
            DoOnWin();
            Scoreboard.GetComponent<Animator>().Play("Scoreboard_SlideIn");
            ScoreFader.GetComponent<Animator>().Play("ScoreFader_FadeIn");
            StartCoroutine(ReadOutScore());
            runOnce = true;
        }
    }

    public bool runOnce;

    public void DoOnTimeUp()
    {
        Debug.Log("TIME'S UP! LET'S SEE HOW YOU DID!");

        //EVALUATE IF WON OR LOSS
        if (hMan.isAlive())     //checks remaining HP
        {
            if (pMan.CheckPileRequirements() && runOnce == false)           //compare pile requirements
            {
                //WIN
                DoOnWin();
                Scoreboard.GetComponent<Animator>().Play("Scoreboard_SlideIn");
                ScoreFader.GetComponent<Animator>().Play("ScoreFader_FadeIn");
                StartCoroutine(ReadOutScore());
                runOnce = true;
            }

            //LOSS
            if (runOnce == false)
            {
                DoOnLoss();
                Scoreboard.GetComponent<Animator>().Play("Scoreboard_SlideIn");
                ScoreFader.GetComponent<Animator>().Play("ScoreFader_FadeIn");
                StartCoroutine(ReadOutScore());
                runOnce = true;
            }
        }
        if (runOnce == false)
        {
            DoOnLoss();
            Scoreboard.GetComponent<Animator>().Play("Scoreboard_SlideIn");
            ScoreFader.GetComponent<Animator>().Play("ScoreFader_FadeIn");
            StartCoroutine(ReadOutScore());
            runOnce = true;
        }
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

    public IEnumerator ReadOutScore() {
        string readout = "";
        readout += ". ";
        scoreReadout.text = readout;
        yield return new WaitForSeconds(3);
        readout += ". ";
        scoreReadout.text = readout;
        yield return new WaitForSeconds(1);
        readout += ". ";
        scoreReadout.text = readout;
        yield return new WaitForSeconds(1);
        readout += "\n\n YOUR SCORE: X";
        scoreReadout.text = readout;
        yield return new WaitForSeconds(1);
        readout += "\n Press Start to Restart!!!";
        scoreReadout.text = readout;
    }

}
