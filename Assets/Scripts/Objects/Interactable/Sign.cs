using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : Interactable
{
    public string dialog;
    private bool isActive = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(!isActive)
            {
                _events.TriggerClue(0);
                _events.TriggerUIMessage(dialog, true);
                isActive = true;
            }
            else
            {
                _events.TriggerUIMessage(null, false);
                isActive = false;
            }
            // frezze player ?
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
            _events.TriggerClue(0);
            _events.TriggerUIMessage(dialog,false);
        }
    }
}
