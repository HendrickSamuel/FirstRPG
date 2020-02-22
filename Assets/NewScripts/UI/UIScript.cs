using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI text;
    [SerializeField]
    private EventSystem _event;
    private bool isActive = false;

    private void Start()
    {
        _event.OnUIMessage += _event_OnUIMessage;
    }

    private void _event_OnUIMessage(object sender, EventSystem.MessageEventArgs e)
    {
        if (e.activate == false || isActive)
            DisableMessage();
        else
          ShowMessage(e.message);
    }

    public void ShowMessage(string newText)
    {
        textBox.SetActive(true);
        text.text = newText;
        isActive = true;
    }

    public void DisableMessage()
    {
        textBox.SetActive(false);
        isActive = false;
    }

    public void Clean()
    {
        DisableMessage();
    }
}
