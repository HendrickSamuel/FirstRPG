using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayerScripts;

public class EventSystem : MonoBehaviour
{
    #region COEURS
    public event EventHandler OnUpdateHearts;

    public void TriggerHealthUpdate()
    {
        OnUpdateHearts?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region CLUE
    public event EventHandler<ClueEventArgs> OnClueSet;

    public class ClueEventArgs: EventArgs
    {
        public int clue;

        public ClueEventArgs(int ps)
        {
            clue = ps;
        }
    }

    public void TriggerClue(int clueToSend)
    {
        OnClueSet?.Invoke(this, new ClueEventArgs(clueToSend));
    }
    #endregion

    #region GiveItem
    public event EventHandler<ItemEventArgs> OnItemReceive;

    public class ItemEventArgs: EventArgs
    {
        public Item receivedItem;
    }

    public void TriggerGiveItem(Item item)
    {
        OnItemReceive?.Invoke(this, new ItemEventArgs { receivedItem = item });
    }
    #endregion

    #region AfficheMessage
    public event EventHandler<MessageEventArgs> OnUIMessage;

    public class MessageEventArgs : EventArgs
    {
        public string message;
        public bool activate;
    }

    public void TriggerUIMessage(string sendMessange,bool active)
    {
        OnUIMessage?.Invoke(this, new MessageEventArgs { message = sendMessange, activate = active });
    }
    #endregion
}

