using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
public class InteractTrigger : MonoBehaviourPunCallbacks
{
  private PlayerInput playerInputActions;
  
  [SerializeField]
  private GameObject Trigger;

  private ItemController itemEqualizer;
   PhotonView PV;
    
    void Awake(){
        PV = GetComponent<PhotonView>();
        itemEqualizer = GetComponent<ItemController>();
        playerInputActions = new PlayerInput();
        playerInputActions.Interact.Enable();
        playerInputActions.Interact.PickUp.performed += PickUp;
        playerInputActions.Interact.PickUp.canceled += stopPickUp;
         Trigger.SetActive(false);
    }
     public void PickUp(InputAction.CallbackContext context)
    {  
          if (!PV.IsMine){
               return;
          }
           itemEqualizer.UpdateItemStatus(PV.ViewID, true);
        
    }

     public void stopPickUp(InputAction.CallbackContext context)
    {  
         if (!PV.IsMine){
               return;
          }
         itemEqualizer.UpdateItemStatus(PV.ViewID, false);
    }



}
