using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text gemText;
    private int itemCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gem")
        {
            Destroy(collision.gameObject);
            Debug.Log(itemCount);
            itemCount++;
            gemText.text = "Gem: " + itemCount;
        }
    }
}
