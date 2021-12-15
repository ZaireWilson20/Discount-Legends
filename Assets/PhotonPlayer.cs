using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PhotonPlayer : MonoBehaviour
{
    [SerializeField] GameObject _playerCam;
    private PhotonView _pv;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>(); 
        if (_pv.IsMine)
        {
            _playerCam.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
