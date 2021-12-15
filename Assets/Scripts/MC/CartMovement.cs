using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : RigidbodyMovement
{
    [SerializeField] float attackForce = 3f;
    [SerializeField] int damageAmount;
    private CapsuleCollider _collider;
    private bool cartTouchingPlayer = false;
    RigidbodyMovement playerCollidedWith;
    private bool canAttack = true; 

    public override void Attack()
    {

        
        if (canAttack && _pv.IsMine)
        {
            Debug.Log("cart attack");
            m_rigidBody.AddForce(transform.rotation * Vector3.forward * attackForce);
            StartCoroutine(DamageOtherPlayer());
        }
        
    }

    private IEnumerator DamageOtherPlayer()
    {
        bool finishCheck = false;
        float timeElapsed = 0;
        canAttack = false; 
        while (!finishCheck)
        {

            if (timeElapsed >= 1.5f)
            {
                finishCheck = true; 
            }
            if (cartTouchingPlayer)
            {
                playerCollidedWith.TakeDamage(damageAmount);
                break; 
            }
            timeElapsed += 1 * Time.deltaTime; 
            yield return null; 
        }

        canAttack = true;

    } 

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Cart trigger with " + other.name);

        if (other.gameObject.tag == "Player")
        {
            cartTouchingPlayer = true;
            playerCollidedWith = other.gameObject.GetComponent<RigidbodyMovement>(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Cart trigger exit " + other.name);
        if (other.gameObject.tag == "Player")
        {
            playerCollidedWith = null; 
            cartTouchingPlayer = false;
        }
    }
}
