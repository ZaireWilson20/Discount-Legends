using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCharacter : Character
{
    [SerializeField] private const float attackForce = 1000f;
     protected override void Attack()
    {
        if(canAttack && _pv.IsMine && !stunned) {
            _rigidBody.AddForce(transform.rotation * Vector3.forward * attackForce);
            base.Attack();
        }
    }

    
}
