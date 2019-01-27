using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting convo with kondo");

        nameText.text = dialogue.characterName;

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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

    //prints a message
    void EndDialogue() {
        Debug.Log("End of conversation.");
    }
}


