using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class PlayerListObject : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _playerName;
    Player _player; 


    public void SetUp(Player player)
    {
        _player = player;
        _playerName.text = player.NickName;
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }


}
