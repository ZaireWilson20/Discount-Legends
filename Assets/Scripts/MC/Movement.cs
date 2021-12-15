using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
public class Movement : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform camera;
    public Transform transform;
    public float smoothing = 0.1f;
    public float speed = 4f;
    private float smoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private bool isGrounded = false;
    private Vector3 currVelocity;

    private PlayerInput playerInputActions;
    PhotonView PV;

    void Awake(){
         PV = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        playerInputActions = new PlayerInput();
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Jump.performed += Jump;
        camera = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        if (!PV.IsMine){
            return;
        }
        isGrounded = controller.isGrounded;
        
        if (isGrounded && currVelocity.y < 0) {
            //Set to -2 as a default so that even if the player is a Little bit off the ground, they'll move down.
            currVelocity.y = -2f;
        }
    

        Vector2 inputDir = playerInputActions.Movement.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputDir.x , 0f, inputDir.y).normalized;


        if(direction.magnitude >= 0.1f) {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y; // takes angle of camera direction we want
                    float smoothAngle  = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle, ref smoothVelocity, smoothing);
                    transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        currVelocity.y += gravity * Time.deltaTime;
        controller.Move(currVelocity*Time.deltaTime);
    }


    public void Jump(InputAction.CallbackContext context)
    {   if(context.performed){ // Check for key press not hold
        if (isGrounded){
             currVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Formula for finding velocity needed to reach a certain height
        }
    }
        
    }


// UNCOMMENT FOR DEBUGGING PURPOSES 

    //  void OnDrawGizmos()
    // {
    //      Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    // }

}
