using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class ItemController : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    [SerializeField]
    private GameObject trigger;

    void Awake(){
        PV = GetComponent<PhotonView>();
    }

    public void UpdateItemStatus(int InstanceID, bool active){
        PV.RPC("UpdateItemEveryone", RpcTarget.All, InstanceID, active);
    }

     [PunRPC]
    private void UpdateItemEveryone(int InstanceID, bool active) {
        if(InstanceID == PV.ViewID) {
            trigger.SetActive(active);
        } else {
            Debug.Log("This player is not the player who pressed the button");
        }
    }
}
