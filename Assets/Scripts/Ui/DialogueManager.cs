using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject box;

    private Queue<string> sentences;

    [SerializeField]
    private KeyCode continueDialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        box.SetActive(true);
        Debug.Log("Starting Conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
            DisplayNextSentence();
        
    }

    public void DisplayNextSentence()
    {

            if (sentences.Count == 0)
            {
                EndDialogue();
                box.SetActive(false);
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
