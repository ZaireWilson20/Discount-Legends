using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Photon.Pun; 
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(PhotonView))]
public class DL_PlayerManager : MonoBehaviourPunCallbacks
{

    PhotonView _pV;
    int Index = 0;
    public string playerType; 
    private void Awake()
    {
        _pV = GetComponent<PhotonView>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (_pV.IsMine)
        {
            //playerType = 
            CreateController(); 
        }

    }

    void CreateController()
    {
        playerType = GameObject.Find("RoomManager").GetComponent<RoomManager>().gmananger.GetComponent<GManager>().playType;
        Debug.Log("Instantiated Player Controller");
        Transform spawnPoint = SpawnManager.instance.GetSpawnPoint(IndexCount()); 
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerType), spawnPoint.position, spawnPoint.rotation);
    }

    int IndexCount()
    { // finds what order the player is in the List
        Player [] listofPlayers = PhotonNetwork.PlayerList;
        int count = 0;
        for (int i = 0 ; i < listofPlayers.Length; i++ ) {
    
            if(listofPlayers[i] == PhotonNetwork.LocalPlayer){
                return i;
            }
        }

        return 0;
    
    }

}
