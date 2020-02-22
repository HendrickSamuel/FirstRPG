using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    #region VARIABLES
    [Header("Variables Porte")]
    [SerializeField]private DoorType doorType;
    [SerializeField] private bool isOpen = false;
    public Inventory playerInventory;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D triggerCollider;
    public BoxCollider2D doorCollider;


    #endregion

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                TryOpen();
            }
        }
    }

    public void TryOpen()
    {
        switch (doorType)
        {
            case DoorType.key:
                if(playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
                break;
        }

    }

    public void Open()
    {
        spriteRenderer.enabled = false;
        isOpen = true;
        doorCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    public void Close()
    {

    }
}
