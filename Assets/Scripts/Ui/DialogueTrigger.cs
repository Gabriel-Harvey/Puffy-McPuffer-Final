using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject portrait;
    public bool portraitCheck;

    public void TriggerDialogue()
    {
        portraitCheck = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        portraitCheck = false;
    }
}
