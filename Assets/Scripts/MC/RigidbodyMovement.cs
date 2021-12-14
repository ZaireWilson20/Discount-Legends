using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
     private PlayerInput playerInputActions;
     Vector3 move;
     [SerializeField]
     private Rigidbody m_rigidBody;
     private float m_MovementSmoothing;
     private Vector3 m_Velocity = Vector3.zero;
     private Vector3 direction;
     public float speed = 10f;
     void Awake(){
        playerInputActions = new PlayerInput();
        playerInputActions.Movement.Enable();
        m_MovementSmoothing = 0.1f;
        // playerInputActions.Movement.Jump.performed += Jump;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {

     Vector2 inputDir = playerInputActions.Movement.Move.ReadValue<Vector2>();
     direction = new Vector3(inputDir.x , 0f, inputDir.y).normalized;
     

    }

    void FixedUpdate()
    {
        move = direction  * speed;
        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, m_rigidBody.velocity.y, move.z * speed  * Time.fixedDeltaTime);
        m_rigidBody.velocity = Vector3.SmoothDamp(m_rigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

      
    }
}
