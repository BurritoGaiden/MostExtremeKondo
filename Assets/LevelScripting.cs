using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScripting : MonoBehaviour {

    public GameObject nameText;
    public GameObject dialogueText;
    public GameObject textBox;
    public GameObject marieKondo;
    public GameObject fader;
    public GameObject analogue1;
    public GameObject analogue2;
    public DialogueManager dM;

    public bool waitForAnalogues;

	// Use this for initialization
	void Start () {
        Shirt.ItemSolved += SetAnalogues;
        StartCoroutine(LevelScript());
	}


    IEnumerator LevelScript() {
        fader.GetComponent<Animator>().Play("Fader_FadeIn");
        marieKondo.GetComponent<Animator>().Play("Marie_Gameplay_Offscreen");
        yield return new WaitForSeconds(3);

        nameText.SetActive(true);
        dialogueText.SetActive(true);
        textBox.SetActive(true);

        yield return new WaitForSeconds(1);

        dM.StartDialogue(GetComponent<Dialogue>());

        yield return new WaitForSeconds(4);

        dM.DisplayNextSentence();

        yield return new WaitForSeconds(4);

        analogue1.GetComponent<Animator>().Play("Analogue_UpAnim");
        analogue2.GetComponent<Animator>().Play("Analogue_UpAnim");
        while (waitForAnalogues == false) {
            yield return null;
        }
        dM.DisplayNextSentence();

        yield return new WaitForSeconds(4);

        dM.DisplayNextSentence();
    }

    public void SetAnalogues() {
        waitForAnalogues = true;
    }
}
