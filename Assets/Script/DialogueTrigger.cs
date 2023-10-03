using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogScript;
    private bool playerDitected;

    // direc triger with palyer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {// if detected show indicator
            playerDitected = true;
            dialogScript.ToggleIndicator(playerDitected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {// if not ditected hide indicator
            playerDitected = false;
            dialogScript.ToggleIndicator(playerDitected);
            dialogScript.EndDialogue();
        }

    }


    // while detected if we interect show dialogues
    private void Update()
    {
        if (playerDitected && Input.GetKeyDown(KeyCode.E))
        {
            dialogScript.StartDialogue();
        }
    }
}
