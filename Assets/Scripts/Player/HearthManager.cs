using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite halfHearth;
    public Sprite emptyHearth;
    public FloatValue hearthContainers;
    public FloatValue playerCurrentHealth;

    [SerializeField]
    internal EventSystem _events;

    void Start()
    {
        InitHearths();
        _events.OnUpdateHearts += Testing_OnUpdateHearts;
    }
    
    private void Testing_OnUpdateHearts(object sender, EventArgs e)
    {
        UpdateHearts();
    }

    public void InitHearths()
    {
        for(int i = 0; i < hearthContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHearth;
        }

        UpdateHearts();
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.runTimeValue / 2;
        for (int i = 0; i < hearthContainers.runTimeValue; i++)
        {
            if( i <= tempHealth-1)
            {
                hearts[i].sprite = fullHearth;
            }
            else
            if(i < tempHealth)
            {
                hearts[i].sprite = halfHearth;
            }
            else
            {
                hearts[i].sprite = emptyHearth;
            }
        }
    }
}
