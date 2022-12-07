using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogue : MonoBehaviour
{

    public GameObject questObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        questObject.SetActive(false);
    }
}
