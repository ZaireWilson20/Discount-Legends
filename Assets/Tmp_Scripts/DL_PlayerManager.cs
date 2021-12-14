using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Photon.Pun; 

[RequireComponent(typeof(PhotonView))]
public class DL_PlayerManager : MonoBehaviour
{

    PhotonView _pV;


    private void Awake()
    {
        _pV = GetComponent<PhotonView>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (_pV.IsMine)
        {
            CreateController(); 
        }

    }

    void CreateController()
    {
        Debug.Log("Instantiated Player Controller");
        Transform spawnPoint = SpawnManager.instance.GetSpawnPoint(); 
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TmpPlayerController"), spawnPoint.position, spawnPoint.rotation);
    }

}
