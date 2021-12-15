using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : RigidbodyMovement
{
    [SerializeField] float attackForce = 3f; 

    public override void Attack()
    {
        Debug.Log("cart attack");
        m_rigidBody.AddForce(transform.rotation*Vector3.forward * attackForce);
        base.Attack();
    }
}
