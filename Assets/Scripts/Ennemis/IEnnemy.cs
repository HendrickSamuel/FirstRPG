using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnnemy
{
    void Knock(Rigidbody2D rb,float time,float damage);
    bool ApplyDamage(float damage);
}
