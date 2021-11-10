using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : MonoBehaviour
{
    public GameObject canvas;
    private GameObject portraitImage;
    private DialogueManager dialogue;
    private bool isShowing;

    private void Start()
    {
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
        dialogue = canvas.GetComponent<DialogueManager>();
    }

    public void DisplayDialogue()
    {
        Debug.Log("DisplayDialogue called");
        isShowing = !isShowing;
        canvas.SetActive(isShowing);
        dialogue.StartStory();
    }
}
