using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Ennemi : MonoBehaviour, IEnnemy
{
    [SerializeField] protected EnemyState currentState;
    [SerializeField] protected FloatValue maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected string enemyName;
    [SerializeField] protected int baseAttack;
    [SerializeField] protected float moveSpeed;
    public GameObject deathEffect;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void setState(EnemyState newstate)
    {
        if(currentState != newstate)
            currentState = newstate;
    }

    public void Knock(Rigidbody2D myRigid, float KnockTime, float damageTaken)
    {
        if(ApplyDamage(damageTaken))
            StartCoroutine(knockCo(myRigid, KnockTime));
    }

    public bool ApplyDamage(float value)
    {
        health -= value;
        if (health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
            return false;
        }
        return true;
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    private IEnumerator knockCo(Rigidbody2D myRigid, float KnockTime)
    {
        if (myRigid != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myRigid.velocity = Vector2.zero;
            setState(EnemyState.idle);
        }
    }

    
}
