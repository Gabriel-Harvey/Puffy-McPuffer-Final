using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Collectables questScore;
    public GameObject crates;
    // Start is called before the first frame update
    void Start()
    {

        questScore = crates.GetComponent<Collectables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (questScore.questScore >= 1)
        {
            print("Woohoo");
        }
    }
}
