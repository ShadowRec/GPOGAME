using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    public float HitBoxRange = 1;
    public Vector3 HitBoxSize = new Vector3((float)1.5, (float)1.5, (float)1.5);
    public float DelayBeforeAttack = 1;
    public float DelayAfterAttack = 1;

    public Attack(float delay1, float delay2)
    {
        DelayBeforeAttack = delay2;
        DelayAfterAttack = delay1;
    }
}
