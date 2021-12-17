using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CartMovement : RigidbodyMovement
{
    [SerializeField] float attackForce = 3f;
    [SerializeField] int damageAmount = 25;
    private CapsuleCollider _collider;
    private bool cartTouchingPlayer = false;
    RigidbodyMovement playerCollidedWith;
    private bool canAttack = true; 

    private int PlayerScore = 0;

    private ScoreBoard ScoreBoard;
    private PlayerRecord Record;
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
            
            playerCollidedWith = other.gameObject.GetComponent<RigidbodyMovement>(); 
          
            cartTouchingPlayer = true;
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

    public void setPlayerScore(int score){
        this.PlayerScore = this.PlayerScore + score;
        updateScoreBoard(PlayerScore);
    }

    public void updateScoreBoard(int PlayerScore) {
        ScoreBoard = GameObject.Find("Scoreboard").GetComponent<ScoreBoard>();
        Record = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        int id = _pv.Owner.ActorNumber;
        ScoreBoard.UpdateScoreboardItem(PlayerScore, id);
        Record.UpdateRecord(PlayerScore, _pv.Owner.NickName);
    }
}
