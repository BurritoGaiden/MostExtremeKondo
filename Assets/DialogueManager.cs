using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public Queue<string> sentences;



    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting convo with kondo");

        sentences.Clear(); //clear the list of sentences

        //load the queue with all of the sentences of the current dialogue
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        //Display the first sentence in the queue
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        //if there are no more sentences, just print a console message
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        //if there are sentences left, print the sentence into the console and then take it out of the queue
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    //prints a message
    void EndDialogue() {
        Debug.Log("End of conversation.");
    }
}


