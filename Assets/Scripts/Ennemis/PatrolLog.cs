using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance = 0.2f;

    void Update()
    {
        
    }

    protected override void CheckDistance()
    {
        float checkDistance = Vector3.Distance(target.position, transform.position);
        if (checkDistance <= chaseRadius && checkDistance > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                
                Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - (Vector2)transform.position);
                rb.MovePosition(temp);
              
            }
        }
        else
        if (checkDistance > chaseRadius)
        {
            if(Vector2.Distance((Vector2)transform.position, (Vector2)path[currentPoint].position) > roundingDistance)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - (Vector2)transform.position);
                rb.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length -1 )
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
            currentGoal = path[currentPoint];
    }
}
