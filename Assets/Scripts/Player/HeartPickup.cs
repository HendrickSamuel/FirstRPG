using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float healingAmount = 1f;
    [SerializeField] private EventSystem evenement;
    private bool hasBeenUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!hasBeenUsed && other.CompareTag("Player") && !other.isTrigger)
        {
            hasBeenUsed = true;
            playerHealth.runTimeValue += healingAmount;
            playerHealth.runTimeValue = Mathf.Clamp(playerHealth.runTimeValue, 0, heartContainers.runTimeValue * 2);
            evenement.TriggerHealthUpdate();


            Destroy(this.gameObject, 0.1f);

        }
    }
}
