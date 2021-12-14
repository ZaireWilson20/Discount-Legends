using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO; 

public class PlayerManager : MonoBehaviour
{
    PhotonView _pv;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>(); 

    }
    // Start is called before the first frame update
    void Start()
    {
        if (_pv.IsMine)
        {
            CreatePlayerController(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePlayerController()
    {
        Debug.Log("Instantiate Player Controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
