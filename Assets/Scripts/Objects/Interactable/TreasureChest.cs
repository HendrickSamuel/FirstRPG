using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureChest : Interactable
{
    public Item content;
    public bool isOpen = false;
    private bool isDeactivated = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDeactivated)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && playerInRange && !isOpen)
        {
            if (!isOpen)
            {
                Debug.Log("open");
                OpenChest();
            }
            else
            {
                Debug.Log("already open");
                ChestIsOpen();
            }
        }
    }

    public void OpenChest()
    {
        _events.TriggerGiveItem(content);
        _events.TriggerClue(0);
        anim.SetTrigger("open");
        isOpen = true;
    }

    public void ChestIsOpen()
    {
        isDeactivated = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            playerInRange = true;
            _events.TriggerClue(2);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            playerInRange = false;
            _events.TriggerClue(0);
        }
    }

}
