using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {

    public GameObject nameText;
    public GameObject dialogueText;
    public GameObject textBox;
    public GameObject marieKondo;
    public GameObject theCamera;
    public GameObject paperAirplane;
    public GameObject Spotlight;

    public AudioClip[] audioClips;

    public DialogueManager dM;
    public Dialogue dialogue;

    public Transform camPos1;
    public Transform camPos2;

    public GameObject fader;

    // Use this for initialization
    void Start () {
        StartCoroutine(Cutscene1());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Cutscene1() {
        nameText.SetActive(false);
        textBox.SetActive(false);
        dialogueText.SetActive(false);
        marieKondo.SetActive(false);
        //theCamera.SetActive(false);
        paperAirplane.SetActive(false);
        Spotlight.SetActive(false);
        paperAirplane.SetActive(false);
        fader.SetActive(false);

        //START

        paperAirplane.SetActive(true);
        yield return new WaitForSeconds(4);
        paperAirplane.SetActive(false);
        marieKondo.SetActive(true);

        yield return new WaitForSeconds(1);

        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        marieKondo.GetComponent<Animator>().Play("Marie_Shake");

        yield return new WaitForSeconds(2);

        Spotlight.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(audioClips[1]);

        //yield return new WaitForSeconds(1);

        textBox.SetActive(true);
        nameText.SetActive(true);
        dialogueText.SetActive(true);

        dM.StartDialogue(dialogue); //line 1
        GetComponent<AudioSource>().PlayOneShot(audioClips[2]);

        yield return new WaitForSeconds(4);

        dM.DisplayNextSentence(); // line 2
        GetComponent<AudioSource>().PlayOneShot(audioClips[3]);

        yield return new WaitForSeconds(3);

        dM.DisplayNextSentence(); // line 3
        GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
        theCamera.transform.position = camPos2.transform.position;

        yield return new WaitForSeconds(3);

        dM.DisplayNextSentence(); // line 4
        GetComponent<AudioSource>().PlayOneShot(audioClips[5]);
        //theCamera.transform.position = camPos2.transform.position;

        yield return new WaitForSeconds(3);

        textBox.SetActive(false);
        nameText.SetActive(false);
        dialogueText.SetActive(false);
        theCamera.transform.position = camPos1.transform.position;
        fader.SetActive(true);
        fader.GetComponent<Animator>().Play("Fader_FadeOut");

        yield return new WaitForSeconds(3);

        //load next level
        SceneManager.LoadScene("SampleScene");
    }
}
