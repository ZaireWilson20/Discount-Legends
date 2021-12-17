using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun; 


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
    public float camRotateSpeed;
    public bool paused = false; 

     void Awake(){
        playerInputActions = new PlayerInput();
        playerInputActions.Movement.Enable();
        m_MovementSmoothing = 0.1f;
        _pv = GetComponent<PhotonView>(); 
        // playerInputActions.Movement.Jump.performed += Jump;
    }
    void Start()
    {
        playerInputActions.Movement.Attack.performed += _ => Attack(); 
        
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
    // Update is called once per frame

    void Update()
    {
        if (!transform.parent.gameObject.GetComponent<Photon.Pun.PhotonView>().IsMine) { return;  }

     Vector2 inputDir = playerInputActions.Movement.Move.ReadValue<Vector2>();
     direction = (transform.rotation * Vector3.forward * inputDir.y + transform.rotation * Vector3.right * inputDir.x).normalized;
     yaw = Mouse.current.delta.ReadValue().x * Time.deltaTime * camRotateSpeed;
        //prevCamPosX = Mouse.current.position.ReadValue().x;
     transform.Rotate(Vector3.up * yaw);


    }

    void FixedUpdate()
    {
        if (!transform.parent.gameObject.GetComponent<Photon.Pun.PhotonView>().IsMine) { return; }
        move = direction  * speed;
        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, m_rigidBody.velocity.y, move.z * speed  * Time.fixedDeltaTime);
        m_rigidBody.velocity = Vector3.SmoothDamp(m_rigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

      
    }

    public virtual void Attack()
    {
        if (!_pv.IsMine) { return; }
    }

    public void TakeDamage(int dmgAmount)
    {
        _pv.RPC("RPC_TakeDamage", RpcTarget.All, dmgAmount);
    }

    [PunRPC]
    public void RPC_TakeDamage(int dmgAmount)
    {

        //m_rigidBody.AddForce(transform.rotation * Vector3.forward * 100f);
        if (!_pv.IsMine) { return;  }
        Debug.Log("Took " + dmgAmount + " damage");
    }
}
