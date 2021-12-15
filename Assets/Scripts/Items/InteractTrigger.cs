using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InteractTrigger : MonoBehaviour
{
  private PlayerInput playerInputActions;
  [SerializeField]
  private GameObject Trigger;
    
    void Awake(){
        playerInputActions = new PlayerInput();
        playerInputActions.Interact.Enable();
        playerInputActions.Interact.PickUp.performed += PickUp;
        playerInputActions.Interact.PickUp.canceled += stopPickUp;
         Trigger.SetActive(false);
    }
     public void PickUp(InputAction.CallbackContext context)
    {  
            Trigger.SetActive(true);
        
    }

     public void stopPickUp(InputAction.CallbackContext context)
    {  
         Trigger.SetActive(false);
    }

}
