using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //fields
    [SerializeField] private GameObject npc;
    //window
    [SerializeField] private GameObject window;
    //indicator
    [SerializeField] private GameObject indicaor;
    // text component
    [SerializeField] private TMP_Text dialogueText;

    // dialog list
    [SerializeField] private List<string> dialogues;

    // index on dialog
    private int index;

    // charactor index
    private int charIndex;

    // started boolean
    private bool started;

    // writing speed
    [SerializeField] private float writingSpeed = 0.03f;

    // wait for next
    private bool waitForNext;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);

    }
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicaor.SetActive(show);
    }

    //start dialog
    public void StartDialogue()
    {
        if (started)
            return;

        // boolean to incate that we have started
        started = true;
        // show window

        ToggleWindow(true);
        //hide the indicator
        ToggleIndicator(false);
        // start with frist dialogue
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        // start index at zero
        index = i;
        // reset the charactor index
        charIndex = 0;
        // clear the dialog component text
        dialogueText.text = string.Empty;
        // start writing
        StartCoroutine(Writing());
    }

    public void EndDialogue()

    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        // hide window
        ToggleWindow(false);
    }
    // writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];

        //write the charactor
        dialogueText.text += currentDialogue[charIndex];

        // increase the charactor index
        charIndex++;
        // make sure end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            // wait x seconds
            yield return new WaitForSeconds(writingSpeed);
            // reastatr same proces

            StartCoroutine(Writing());
        }
        else
        {
            waitForNext = true;

        }


    }

    private void Update()
    {
        if (!started)
            return;


        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;
            if (index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                // end dialog
                EndDialogue();
                Destroy(npc);
            }

        }
    }
}
