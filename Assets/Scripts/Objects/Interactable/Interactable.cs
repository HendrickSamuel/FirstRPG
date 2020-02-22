using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected bool playerInRange;
    [SerializeField] protected EventSystem _events;

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _events.TriggerClue(1);
            playerInRange = true;
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _events.TriggerClue(0);
            playerInRange = false;
        }
    }
}
