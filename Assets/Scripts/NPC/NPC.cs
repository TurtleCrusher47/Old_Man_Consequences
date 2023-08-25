using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject choicePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject nextLineButton;
    public float wordSpeed;
    public bool inRange;
    
    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    ResetText();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            nextLineButton.SetActive(true);
        }
    }

    public void ResetText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        nextLineButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index ++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            choicePanel.SetActive(true);

            ResetText();
        }
    }

    public void DisableChoice()
    {
        choicePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inRange = false;
            ResetText();
            DisableChoice();
        }
    }
}
