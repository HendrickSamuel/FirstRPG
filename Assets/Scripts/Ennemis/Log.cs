using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Ennemi
{
    protected Rigidbody2D rb;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    protected Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAwake", true);
        setState(EnemyState.idle);
        homePosition = transform;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        float checkDistance = Vector3.Distance(target.position, transform.position);
        if (checkDistance <= chaseRadius && checkDistance > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                anim.SetBool("isAwake", true);
                Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - (Vector2)transform.position);
                rb.MovePosition(temp);
                setState(EnemyState.walk);
            }
        }
        else 
        if(checkDistance > chaseRadius)
        {
            anim.SetBool("isAwake", false);
            setState(EnemyState.idle);

        }
    }

    protected void SetAnim(Vector2 deplacement)
    {
        anim.SetFloat("MoveX", deplacement.x);
        anim.SetFloat("MoveY", deplacement.y);
    }

    protected void ChangeAnim(Vector2 deplacement)
    {
        if(Mathf.Abs(deplacement.x) > Mathf.Abs(deplacement.y))
        {
            if (deplacement.x > 0)
                SetAnim(new Vector2(1, 0));
            else
            if (deplacement.x < 0)
                SetAnim(new Vector2(-1, 0));
        }
        else
        if(Mathf.Abs(deplacement.x) < Mathf.Abs(deplacement.y))
        {
            if (deplacement.y > 0)
                SetAnim(new Vector2(0, 1));
            else
           if (deplacement.y < 0)
                SetAnim(new Vector2(0, -1));
        }
    }
}
