using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : PowerUp
{
    public FloatValue playerHealth;
    public float healingAmount = 1f;
    [SerializeField] private EventSystem evenement;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.runTimeValue += healingAmount;
            evenement.TriggerHealthUpdate();

            this.gameObject.SetActive(false);

        }
    }
}
