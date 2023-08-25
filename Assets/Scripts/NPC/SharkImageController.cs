using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkImageController : MonoBehaviour
{
    public GameObject sharkImage;

    public NPC npc;

    void Update()
    {
        if (npc.dialoguePanel.activeInHierarchy || npc.choicePanel.activeInHierarchy)
        {
            sharkImage.SetActive(true);
        }

        else
        {
            sharkImage.SetActive(false);
        }
    }
}
