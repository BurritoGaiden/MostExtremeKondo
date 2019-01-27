using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;

    private void Start()
    {
        //TriggerDialogue();
    }

    //public void TriggerDialogue() {
     //   FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
