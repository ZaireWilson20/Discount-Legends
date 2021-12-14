using System.Collections;
using System.Collections.Generic;
using Photon.Realtime; 
using UnityEngine;
using TMPro; 

public class RoomListObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;
    public RoomInfo _info; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(RoomInfo info) 
    {
        _info = info;
        _roomName.text = info.Name; 
    }
    
    public void OnClick()
    {
        ConnectionManagement.instance.JoinRoom(_info);
    }
}
