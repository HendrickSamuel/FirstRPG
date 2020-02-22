using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

public class knockback : MonoBehaviour
{
    [SerializeField] float thrust = 0;
    [SerializeField] string knockbackTag = "";
    [SerializeField] float knockTime = 0.2f;
    [SerializeField] float damage = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(knockbackTag))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 difference = (Vector2)(enemyRb.transform.position - transform.position);
                difference = difference.normalized * thrust;
                enemyRb.AddForce(difference, ForceMode2D.Impulse);
                               
                if(other.CompareTag("Player"))
                {
                    if(other.GetComponent<PlayerScript>()._actualState != PlayerScripts.PlayerState.stagger)
                    {
                        other.GetComponent<PlayerScript>().Knock(knockTime, damage);
                    }

                }

            }

        }
    }

    
}
