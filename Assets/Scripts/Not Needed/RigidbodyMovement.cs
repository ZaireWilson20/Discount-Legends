using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun; 
using UnityEngine.Audio;
using System;

public class RigidbodyMovement : MonoBehaviourPunCallbacks
{

    [SerializeField] protected PhotonView _pv; 
     protected PlayerInput playerInputActions;
     Vector3 move;
     [SerializeField]
     protected Rigidbody m_rigidBody;
     private float m_MovementSmoothing;
     private Vector3 m_Velocity = Vector3.zero;
     private Vector3 direction;
     public float speed = 10f;
    public float yaw = 0f;
    public float camRotateSpeed = 1000f;
    public bool paused = false; 
    public bool stunned = false;
    public int healthBar = 100;
    private Animator anim; 

    public AudioSource Hit;
    public AudioSource Stun;


    private Vector2 input_View;
    private Vector2 input_Move;
     void Awake(){
        playerInputActions = new PlayerInput();
        playerInputActions.Movement.Enable();
        m_MovementSmoothing = 0.1f;
        _pv = GetComponent<PhotonView>();
        m_rigidBody = GetComponent<Rigidbody>(); 

        foreach(Transform t in transform)
        {
            if (!t.gameObject.activeSelf) { continue; }
            Animator ani = t.gameObject.GetComponent<Animator>(); 
            if(ani != null)
            {
                anim = ani;
                break; 
            }
        }
        // playerInputActions.Movement.Jump.performed += Jump;
    }
    void Start()
    {
        playerInputActions.Movement.Attack.performed += _ => Attack(); 
        playerInputActions.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnEnable()
    {
        playerInputActions.Movement.Attack.Enable();
    }

    public override void OnDisable()
    {
        playerInputActions.Movement.Attack.Disable(); 
    }
    void Update()
    {
        if (!transform.parent.gameObject.GetComponent<Photon.Pun.PhotonView>().IsMine) { return;  }
        input_Move = playerInputActions.Movement.Move.ReadValue<Vector2>();
      direction = (transform.rotation * Vector3.forward * input_Move.y + transform.rotation * Vector3.right * input_Move.x).normalized;
     yaw = input_View.normalized.x * Time.deltaTime * camRotateSpeed;
        //prevCamPosX = Mouse.current.position.ReadValue().x;
     transform.Rotate(Vector3.up * yaw);
        if(direction.magnitude > 0) {
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Idle", true);
        }

        Debug.Log("I am Looking " + input_View);

    }

    void FixedUpdate()
    {
        if (!transform.parent.gameObject.GetComponent<Photon.Pun.PhotonView>().IsMine) { return; }
        if (!stunned){
        move = direction  * speed;
        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, m_rigidBody.velocity.y, move.z * speed  * Time.fixedDeltaTime);
        m_rigidBody.velocity = Vector3.SmoothDamp(m_rigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
      
    }

    public virtual void Attack()
    {
        if (!_pv.IsMine) { return; }
        // Make sure this doesn't play while stunned and can't attack
        anim.SetTrigger("Attack");
    }

    public void TakeDamage(int dmgAmount)
    {
        _pv.RPC("RPC_TakeDamage", RpcTarget.All, dmgAmount);
    }

    [PunRPC]
    public void RPC_TakeDamage(int dmgAmount)
    {

        //m_rigidBody.AddForce(transform.rotation * Vector3.forward * 100f);
        
        
        if (!_pv.IsMine) {return;  }   

        if(!stunned){ // Necessary to stop playing hit sounds while player is stunned/ stop them from lowering health further
           _pv.RPC("RPC_PlayHit", RpcTarget.All);
            healthBar -= dmgAmount;
        }

        if (healthBar <= 0 && !stunned) {
           _pv.RPC("RPC_PlaySound", RpcTarget.All); //_pv.RPC calls are necessary to play sound for everyone involved
            stunned = true;
            anim.SetBool("Stunned", true); 
            StartCoroutine(Stunned()); // delay movement
        }
        
    }

    [PunRPC]

    public void RPC_PlayHit(){
        Hit.Play();
    }

    [PunRPC]
    public void RPC_PlaySound(){
         Stun.Play();
    }
      private IEnumerator Stunned()
    { 
         yield return new WaitForSeconds(5f);
         healthBar = 100;
         stunned = false;
        anim.SetBool("Stunned", false); 
    }
}
